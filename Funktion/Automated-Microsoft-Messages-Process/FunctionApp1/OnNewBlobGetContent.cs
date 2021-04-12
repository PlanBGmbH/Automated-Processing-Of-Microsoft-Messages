// Copyright (c) PlanB. GmbH. All rights reserved.
// Author: Peter Schneider

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Security.Claims;
using System.Threading.Tasks;

using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using FunctionApp1.Class;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;

namespace FunctionApp1
{
    /// <summary>
    /// OnNewBlobGetContent.
    /// </summary>
    public static class OnNewBlobGetContent
    {
        /// <summary>
        /// Run.
        /// </summary>
        /// <param name="myBlob">myBlob.</param>
        /// <param name="name">name.</param>
        /// <param name="log">log.</param>
        [FunctionName("OnNewBlobGetContent")]
        public static async void Run([BlobTrigger("newmessages/{name}", Connection = "blobConnectionString")] Stream myBlob, string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
            PlannerMessage plannerMessage;

            IFormatter formatter = new BinaryFormatter();
            myBlob.Seek(0, SeekOrigin.Begin);
            var o = (RootPlannerMessage)formatter.Deserialize(myBlob);

            var clientId = System.Environment.GetEnvironmentVariable("client_id");
            var secret = System.Environment.GetEnvironmentVariable("client_secret");
            string connectionString = System.Environment.GetEnvironmentVariable("blobConnectionString");
            var tenantID = System.Environment.GetEnvironmentVariable("tenant");
            var user = System.Environment.GetEnvironmentVariable("usernameemail");
            var password = System.Environment.GetEnvironmentVariable("userpassword");
            SecureString securePwd = new SecureString();
            for (int i = 0; i < password.Length; i++)
            {
                securePwd.AppendChar(password[i]);
            }

            BlobContainerClient blobContainer = new BlobContainerClient(connectionString, "newmessages");

            string redirect = "https://localhost/queueTrigger";
            string authority = "https://login.microsoftonline.com/54a5c794-f6e6-4606-b86a-f7318722bee6";
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

            foreach (var item in o.RootPlannerMessage1)
            {
                plannerMessage = item;

                Microsoft.Graph.PlannerTask newTask = new Microsoft.Graph.PlannerTask();
                if (plannerMessage.DueDate != null)
                {
                    newTask.DueDateTime = plannerMessage.DueDate;
                }

                newTask.OrderHint = " !";
                newTask.Title = plannerMessage.Title;
                newTask.PlanId = System.Environment.GetEnvironmentVariable("messageCenterPlanId");

                // Set Action Category
                PlannerAppliedCategories cat = new PlannerAppliedCategories();

                if (plannerMessage.Categories.Contains("Action"))
                {
                    cat.Category1 = true;
                }
                else
                {
                    cat.Category1 = false;
                }

                // Set Plan for Change Category
                if (plannerMessage.Categories.Contains("Plan for Change"))
                {
                    cat.Category2 = true;
                }
                else
                {
                    cat.Category2 = false;
                }

                // Set Prevent or Fix Issues Category
                if (plannerMessage.Categories.Contains("Fix Issues"))
                {
                    cat.Category3 = true;
                }
                else
                {
                    cat.Category3 = false;
                }

                // Set Advisory Category
                if (plannerMessage.Categories.Contains("Advisory"))
                {
                    cat.Category4 = true;
                }
                else
                {
                    cat.Category4 = false;
                }

                // Set Awareness Category
                if (plannerMessage.Categories.Contains("Awareness"))
                {
                    cat.Category5 = true;
                }
                else
                {
                    cat.Category5 = false;
                }

                // Set Stay Informed Category
                if (plannerMessage.Categories.Contains("Stay Informed"))
                {
                    cat.Category6 = true;
                }
                else
                {
                    cat.Category6 = false;
                }

                newTask.BucketId = plannerMessage.BucketId;
                newTask.AppliedCategories = cat;

                newTask.Assignments = new PlannerAssignments
                {
                    AdditionalData = new Dictionary<string, object>()
                     {
                      { plannerMessage.Assignee, "{\"@odata.type\":\"#microsoft.graph.plannerAssignment\",\"orderHint\":\" !\"}" },
                     },
                };

                string description = plannerMessage.Description.Replace("&amp", "&");
                description = description.Replace("[\u201C\u201D]", "\"");
                description = description.Replace("“", "\"");
                description = description.Replace("’", "'");
                description = description.Replace("�", "'");
                description = description.Replace("</p>", string.Empty);
                description = description.Replace("<p>", string.Empty);
                description = description.Replace("</li>", string.Empty);
                description = description.Replace("<li>", string.Empty);
                description = description.Replace("<ul>", string.Empty);
                description = description.Replace("<ul>", string.Empty);

                newTask.Details = new PlannerTaskDetails
                {
                    Description = description,
                };

                Class.PlannerTask plannertask = new Class.PlannerTask();

                try
                {
                    string jsonString = JsonConvert.SerializeObject(newTask);

                    plannertask = JsonConvert.DeserializeObject<Class.PlannerTask>(jsonString);

                    MemoryStream stream = new MemoryStream();
                    formatter = new BinaryFormatter();
                    formatter.Serialize(stream, plannertask);

                    stream.Position = 0;
                    string blobName = string.Format("NewTask-{0}-{1}", newTask.Title, newTask.BucketId);
                    BlobContainerClient blobContainerUpload = new BlobContainerClient(connectionString, "existingmessages");
                    blobContainerUpload.UploadBlob(blobName, stream);

                    var tmp = await graphClient.Planner.Tasks
                                            .Request()
                                            .AddAsync(newTask);
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
