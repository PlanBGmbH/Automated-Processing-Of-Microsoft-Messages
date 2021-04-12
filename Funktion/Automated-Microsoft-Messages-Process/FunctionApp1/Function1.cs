using System;
using System.Net.Http;
using System.Net.Http.Headers;
using FunctionApp1.Class;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Security;
using Microsoft.Identity.Client;
using Microsoft.Graph.Auth;
using Microsoft.Graph;
using Azure.Storage.Blobs;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace FunctionApp1
{
    public static class Function1
    {
        static readonly HttpClient client = new HttpClient();

        [FunctionName("OnTimerGetMessages")]
        [return: Queue("myqueue-items")]
        public static async Task<string> OnTimerGetMessages([TimerTrigger("* * * * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");



            //
            HttpResponseMessage response;
            string connectionString = System.Environment.GetEnvironmentVariable("blobConnectionString");
            string baseAddress = System.Environment.GetEnvironmentVariable("baseAddress");
            string grant_type = "client_credentials";
            string client_id = System.Environment.GetEnvironmentVariable("client_id");
            string client_secret = System.Environment.GetEnvironmentVariable("client_secret");
            string resource = "https://manage.office.com";
            string messageUriString = System.Environment.GetEnvironmentVariable("messageUri");
            var form = new Dictionary<string, string>
                {
                    {"grant_type", grant_type},
                    {"resource", resource},
                    {"client_id", client_id},
                    {"client_secret", client_secret},
                };
            HttpResponseMessage tokenResponse = await client.PostAsync(baseAddress, new FormUrlEncodedContent(form));
            var jsonContent = await tokenResponse.Content.ReadAsStringAsync();
            Token tok = JsonConvert.DeserializeObject<Token>(jsonContent);


            Uri messageUri = new Uri(messageUriString);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tok.TokenType, tok.AccessToken);
            string contentType = "application/json";
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
            HttpResponseMessage MessageResponse = await client.GetAsync(messageUri + "?$filter=MessageType eq 'MessageCenter'");
            jsonContent = await MessageResponse.Content.ReadAsStringAsync();
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
            string authority =String.Format("https://login.microsoftonline.com/{0}", tenantID);
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
            rootPlannerMessage.rootPlannerMessage = new List<PlannerMessage>();



            foreach (var message in messageRoot.value)
            {

                foreach (var assignee in assigneeList.AssigneeList)
                {



                    try
                    {



                        if (assignee.bucketName == message.AffectedWorkloadDisplayNames[0])
                        {

                            foreach (var item in tasks)
                            {
                                try
                                {
                                    existingTask = item;
                                    exists = true;
                                    if (message.Title == item.Title && assignee.bucketId == item.BucketId)
                                    {
                                        exists = false;
                                        break;
                                    }
                                }
                                catch (Exception)
                                {

                                    throw;
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
                                string FullMessage = "";
                                foreach (var item in message.Messages)
                                {
                                    FullMessage += item.MessageText;
                                }
                                plannerMessage.Description = FullMessage;
                                plannerMessage.Reference = message.ExternalLink;
                                plannerMessage.Product = message.AffectedWorkloadDisplayNames[0];
                                plannerMessage.BucketId = assignee.bucketId;
                                plannerMessage.Assignee = assignee.assigneeId;


                                rootPlannerMessage.rootPlannerMessage.Add(plannerMessage);
                            }
                            else
                            {

                            }
                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
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
                string blobName = String.Format("newmessages-{0}", time.Ticks);
                blobContainer.UploadBlob(blobName, stream);
                return blobName;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return "";

        }


    }
}
