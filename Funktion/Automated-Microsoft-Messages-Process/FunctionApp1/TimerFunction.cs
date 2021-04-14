// Copyright (c) PlanB. GmbH. All rights reserved.
// Author: Peter Schneider

using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Threading.Tasks;

using Azure.Storage.Blobs;
using FunctionApp1.Class;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using Newtonsoft.Json;

namespace TimerFunction
{
    /// <summary>
    /// TimerFunction.
    /// </summary>
    public static class TimerFunction
    {
        /// <summary>
        /// Gets or Sets Client.
        /// </summary>
        private static readonly HttpClient Client = new HttpClient();

        /// <summary>
        /// OnTimerGetMessages.
        /// </summary>
        /// <param name="myTimer">myTimer.</param>
        /// <param name="log">log.</param>
        [FunctionName("OnTimerGetMessages")]
        public static async void OnTimerGetMessages([TimerTrigger("2 34 10 * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            string connectionString = System.Environment.GetEnvironmentVariable("blobConnectionString");
            string baseAddress = System.Environment.GetEnvironmentVariable("baseAddress");
            string grant_type = "client_credentials";
            string client_id = System.Environment.GetEnvironmentVariable("client_id");
            string client_secret = System.Environment.GetEnvironmentVariable("client_secret");
            string resource = "https://manage.office.com";
            string messageUriString = System.Environment.GetEnvironmentVariable("messageUri");
            Guid guid = Guid.NewGuid();
            var methodName = nameof(OnTimerGetMessages);
            var trace = new Dictionary<string, string>();
            EventId eventId = new EventId(guid.GetHashCode(), "Automated-MS-Message-Process");
            bool hasError = false;
            var form = new Dictionary<string, string>
                {
                    { "grant_type", grant_type },
                    { "resource", resource },
                    { "client_id", client_id },
                    { "client_secret", client_secret },
                };
            HttpResponseMessage tokenResponse = await Client.PostAsync(baseAddress, new FormUrlEncodedContent(form));
            var jsonContent = await tokenResponse.Content.ReadAsStringAsync();
            Token tok = JsonConvert.DeserializeObject<Token>(jsonContent);

            Uri messageUri = new Uri(messageUriString);
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tok.TokenType, tok.AccessToken);
            string contentType = "application/json";
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
            HttpResponseMessage messageResponse = await Client.GetAsync(messageUri + "?$filter=MessageType eq 'MessageCenter'");
            jsonContent = await messageResponse.Content.ReadAsStringAsync();
            RootMessage messageRoot = JsonConvert.DeserializeObject<RootMessage>(jsonContent);
            RootAssigneeList assigneeList;
            PlannerMessage plannerMessage;
            RootPlannerMessage rootPlannerMessage;

            BlobContainerClient blobContainer = new BlobContainerClient(connectionString, "newmessages");

            using (StreamReader r = new StreamReader(@"C:\Development\AbschProj\Funktion\Automated-Microsoft-Messages-Process\FunctionApp1\Configuration\AssigneeListValues.json"))
            {
                string json = r.ReadToEnd();
                assigneeList = JsonConvert.DeserializeObject<RootAssigneeList>(json);
            }

            var clientId = System.Environment.GetEnvironmentVariable("client_id");
            var secret = System.Environment.GetEnvironmentVariable("client_secret");

            var tenantID = System.Environment.GetEnvironmentVariable("tenant");
            var user = System.Environment.GetEnvironmentVariable("usernameemail");
            var password = System.Environment.GetEnvironmentVariable("userpassword");
            SecureString securePwd = new SecureString();
            for (int i = 0; i < password.Length; i++)
            {
                securePwd.AppendChar(password[i]);
            }

            string redirect = "https://localhost/queueTrigger";
            string authority = string.Format("https://login.microsoftonline.com/{0}", tenantID);
            string[] scopes = { "Group.ReadWrite.All", "Tasks.ReadWrite" };
            IPublicClientApplication publicClientApplication = PublicClientApplicationBuilder
                .Create(clientId)
                .WithTenantId(tenantID)
                .WithAuthority(authority)
                .WithRedirectUri(redirect)
                .Build();
            UsernamePasswordProvider authProvider = new UsernamePasswordProvider(publicClientApplication, scopes);
            GraphServiceClient graphClient = new GraphServiceClient(authProvider);
            var tasks = await graphClient.Planner.Plans["ytFxtGYmZU-oHi_ttjX_HZgAGErG"].Tasks
                            .Request()
                            .WithUsernamePassword(user, securePwd)
                            .GetAsync();

            Microsoft.Graph.PlannerTask existingTask = new Microsoft.Graph.PlannerTask();

            bool exists = true;
            rootPlannerMessage = new RootPlannerMessage();
            rootPlannerMessage.RootPlannerMessage1 = new List<PlannerMessage>();

            foreach (var message in messageRoot.Value)
            {
                foreach (var assignee in assigneeList.AssigneeList)
                {
                    try
                    {
                        if (assignee.BucketName == message.AffectedWorkloadDisplayNames[0])
                        {
                            foreach (var item in tasks)
                            {
                                try
                                {
                                    existingTask = item;
                                    exists = true;
                                    if (message.Title == item.Title && assignee.BucketId == item.BucketId)
                                    {
                                        exists = false;

                                        break;
                                    }
                                }
                                catch (Exception e)
                                {
                                    hasError = true;
                                    trace.Add($"{MethodBase.GetCurrentMethod().Name} - rejected", e.Message);
                                    trace.Add($"{MethodBase.GetCurrentMethod().Name} -  rejected - StackTrace", e.StackTrace);
                                    log.LogError(eventId, $"'{methodName}' - rejected", trace, e);
                                    log.LogInformation(eventId, $"'{methodName}' - rejected", trace);
                                }
                            }

                            if (exists)
                            {
                                plannerMessage = new PlannerMessage();
                                plannerMessage.Id = message.Id;
                                plannerMessage.Title = message.Title;
                                plannerMessage.Categories = message.ActionType + "," + message.Classification + "," + message.Category;
                                plannerMessage.DueDate = message.ActionRequiredByDate;
                                plannerMessage.Updated = message.LastUpdatedTime;
                                string fullMessage = string.Empty;
                                foreach (var item in message.Messages)
                                {
                                    fullMessage += item.MessageText;
                                }

                                plannerMessage.Description = fullMessage;
                                plannerMessage.Reference = message.ExternalLink;
                                plannerMessage.Product = message.AffectedWorkloadDisplayNames[0];
                                plannerMessage.BucketId = assignee.BucketId;
                                plannerMessage.Assignee = assignee.AssigneeId;

                                rootPlannerMessage.RootPlannerMessage1.Add(plannerMessage);
                            }
                            else
                            {
                                string blobName = string.Format("NewTask-{0}-{1}", message.Title, assignee.BucketId);

                                bool somethingChanged = false;
                                BlobContainerClient blobContainerDownload = new BlobContainerClient(connectionString, "plannermessages");
                                Microsoft.Graph.PlannerTask taskFromBlob;
                                Microsoft.Azure.Storage.CloudStorageAccount storageAccount = Microsoft.Azure.Storage.CloudStorageAccount.Parse(connectionString);
                                CloudBlobClient serviceClient = storageAccount.CreateCloudBlobClient();
                                CloudBlobContainer container = serviceClient.GetContainerReference($"plannermessages");
                                IFormatter formatter = new BinaryFormatter();
                                CloudBlockBlob blob = null;
                                try
                                {

                                    blob = container.GetBlockBlobReference($"{blobName}");
                                    var text = await blob.DownloadTextAsync();
                                    taskFromBlob = JsonConvert.DeserializeObject<Microsoft.Graph.PlannerTask>(text);

                                    if (taskFromBlob.AppliedCategories != existingTask.AppliedCategories)
                                    {
                                        somethingChanged = true;
                                    }

                                    if (taskFromBlob.Assignments != existingTask.Assignments)
                                    {
                                        somethingChanged = true;
                                    }

                                    if (taskFromBlob.BucketId != existingTask.BucketId)
                                    {
                                        somethingChanged = true;
                                    }

                                    if (taskFromBlob.CompletedDateTime != existingTask.CompletedDateTime)
                                    {
                                        somethingChanged = true;
                                    }

                                    if (taskFromBlob.CreatedBy != existingTask.CreatedBy)
                                    {
                                        somethingChanged = true;
                                    }

                                    if (taskFromBlob.CreatedDateTime != existingTask.CreatedDateTime)
                                    {
                                        somethingChanged = true;
                                    }

                                    if (taskFromBlob.Details != existingTask.Details)
                                    {
                                        somethingChanged = true;
                                    }

                                    if (taskFromBlob.DueDateTime != existingTask.DueDateTime)
                                    {
                                        somethingChanged = true;
                                    }

                                    if (taskFromBlob.Id != existingTask.Id)
                                    {
                                        somethingChanged = true;
                                    }

                                    if (taskFromBlob.ODataType != existingTask.ODataType)
                                    {
                                        somethingChanged = true;
                                    }

                                    if (taskFromBlob.OrderHint != existingTask.OrderHint)
                                    {
                                        somethingChanged = true;
                                    }

                                    if (taskFromBlob.PercentComplete != existingTask.PercentComplete)
                                    {
                                        somethingChanged = true;
                                    }

                                    if (taskFromBlob.PlanId != existingTask.PlanId)
                                    {
                                        somethingChanged = true;
                                    }

                                    if (taskFromBlob.StartDateTime != existingTask.StartDateTime)
                                    {
                                        somethingChanged = true;
                                    }

                                    if (taskFromBlob.Title != existingTask.Title)
                                    {
                                        somethingChanged = true;
                                    }
                                }
                                catch (Exception e)
                                {
                                    hasError = true;
                                    trace.Add($"{MethodBase.GetCurrentMethod().Name} - rejected", e.Message);
                                    trace.Add($"{MethodBase.GetCurrentMethod().Name} -  rejected - StackTrace", e.StackTrace);
                                    log.LogError(eventId, $"'{methodName}' - rejected", trace, e);
                                    log.LogInformation(eventId, $"'{methodName}' - rejected", trace);
                                }

                                if (somethingChanged)
                                {
                                    try
                                    {


                                        FunctionApp1.Class.PlannerTask plannertask = new FunctionApp1.Class.PlannerTask();

                                        string jsonString = JsonConvert.SerializeObject(existingTask);

                                        plannertask = JsonConvert.DeserializeObject<FunctionApp1.Class.PlannerTask>(jsonString);

                                        MemoryStream stream = new MemoryStream();
                                        formatter = new BinaryFormatter();
                                        formatter.Serialize(stream, plannertask);

                                        stream.Position = 0;

                                        BlobContainerClient blobContainerUpload = new BlobContainerClient(connectionString, "plannermessages");

                                        blob.UploadFromStream(stream);
                                    }
                                    catch (Exception e)
                                    {
                                        hasError = true;
                                        trace.Add($"{MethodBase.GetCurrentMethod().Name} - rejected", e.Message);
                                        trace.Add($"{MethodBase.GetCurrentMethod().Name} -  rejected - StackTrace", e.StackTrace);
                                        log.LogError(eventId, $"'{methodName}' - rejected", trace, e);
                                        log.LogInformation(eventId, $"'{methodName}' - rejected", trace);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        hasError = true;
                        log.LogError(eventId, $"'{methodName}' - rejected", trace, e);
                        log.LogInformation(eventId, $"'{methodName}' - rejected", trace);
                    }
                }
            }

            try
            {
                MemoryStream stream = new MemoryStream();
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, rootPlannerMessage);

                stream.Position = 0;
                DateTime time = DateTime.Now;
                string blobName = string.Format("newmessages-{0}", time.Ticks);
                blobContainer.UploadBlob(blobName, stream);
            }
            catch (Exception e)
            {
                hasError = true;
                trace.Add($"{MethodBase.GetCurrentMethod().Name} - rejected", e.Message);
                trace.Add($"{MethodBase.GetCurrentMethod().Name} -  rejected - StackTrace", e.StackTrace);
                log.LogError(eventId, $"'{methodName}' - rejected", trace, e);
                log.LogInformation(eventId, $"'{methodName}' - rejected", trace);
            }
            finally
            {
                log.LogTrace(eventId, $"'{methodName}' - finished");
                log.LogInformation(eventId, $"'{methodName}' - finished", trace);
            }

            //if (hasError)
            //{
            //    throw new Exception($"{methodName} failed");
            //}

        }
    }
}
