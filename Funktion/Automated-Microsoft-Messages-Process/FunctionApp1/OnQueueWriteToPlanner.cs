using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using FunctionApp1.Class;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using Microsoft.Graph.Auth;
using System.Security;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Linq;
using PlannerTask = Microsoft.Graph.PlannerTask;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace FunctionApp1
{
    public static class OnQueueWriteToPlanner
    {
        static readonly HttpClient client = new HttpClient();
        [FunctionName("OnQueueWriteToPlanner")]
        public static async void Run([QueueTrigger("myqueue-items", Connection = "")] string myQueueItem, ILogger log)
        {
            //PlannerMessage plannerMessage;
            //plannerMessage = JsonConvert.DeserializeObject<PlannerMessage>(myQueueItem);





            //var clientId = System.Environment.GetEnvironmentVariable("client_id");
            //var secret = System.Environment.GetEnvironmentVariable("client_secret");
            //string connectionString = System.Environment.GetEnvironmentVariable("blobConnectionString");
            //var tenantID = System.Environment.GetEnvironmentVariable("tenant");
            //var user = System.Environment.GetEnvironmentVariable("usernameemail");
            //var password = System.Environment.GetEnvironmentVariable("userpassword");
            //SecureString securePwd = new SecureString();
            //for (int i = 0; i < password.Length; i++)
            //{
            //    securePwd.AppendChar(password[i]);
            //}



            //BlobContainerClient blobContainer = new BlobContainerClient(connectionString, "newmessages");


            ////BlobDownloadInfo download = await 
                
                

            //string redirect = "https://localhost/queueTrigger";
            //string authority = "https://login.microsoftonline.com/54a5c794-f6e6-4606-b86a-f7318722bee6";
            //string[] scopes = { "Group.ReadWrite.All", "Tasks.ReadWrite" };
            //IPublicClientApplication publicClientApplication = PublicClientApplicationBuilder
            //    .Create(clientId)
            //    .WithTenantId(tenantID)
            //    .WithAuthority(authority)
            //    .WithRedirectUri(redirect)
            //    .Build();
            //UsernamePasswordProvider authProvider = new UsernamePasswordProvider(publicClientApplication, scopes);
            //GraphServiceClient graphClient = new GraphServiceClient(authProvider);
            //var tasks = await graphClient.Planner.Plans["ytFxtGYmZU-oHi_ttjX_HZgAGErG"].Tasks
            //                .Request()
            //                .WithUsernamePassword(user, securePwd)
            //                .GetAsync();

            //Microsoft.Graph.PlannerTask existingTask = new Microsoft.Graph.PlannerTask();
            //bool exists = true;
            //foreach (var item in tasks)
            //{
            //    existingTask = item;

            //    if (existingTask.Title == plannerMessage.Title && existingTask.BucketId == plannerMessage.BucketId)
            //    {
            //        exists = false;
            //        break;
            //    }
            //}
            //if (exists)
            //{
            //    Microsoft.Graph.PlannerTask newTask = new Microsoft.Graph.PlannerTask();
            //    if (plannerMessage.DueDate != null)
            //    {
            //        newTask.DueDateTime = plannerMessage.DueDate;
            //    }
            //    newTask.OrderHint = " !";
            //    newTask.Title = plannerMessage.Title;
            //    newTask.PlanId = System.Environment.GetEnvironmentVariable("messageCenterPlanId");
            //    //Set Action Category
            //    PlannerAppliedCategories cat = new PlannerAppliedCategories();

            //    if (plannerMessage.Categories.Contains("Action"))
            //    {
            //        cat.Category1 = true;
            //    }
            //    else
            //    {
            //        cat.Category1 = false;
            //    }
            //    //Set Plan for Change Category
            //    if (plannerMessage.Categories.Contains("Plan for Change"))
            //    {
            //        cat.Category2 = true;
            //    }
            //    else
            //    {
            //        cat.Category2 = false;
            //    }
            //    //Set Prevent or Fix Issues Category
            //    if (plannerMessage.Categories.Contains("Fix Issues"))
            //    {
            //        cat.Category3 = true;
            //    }
            //    else
            //    {
            //        cat.Category3 = false;
            //    }
            //    //Set Advisory Category
            //    if (plannerMessage.Categories.Contains("Advisory"))
            //    {
            //        cat.Category4 = true;
            //    }
            //    else
            //    {
            //        cat.Category4 = false;
            //    }
            //    //Set Awareness Category
            //    if (plannerMessage.Categories.Contains("Awareness"))
            //    {
            //        cat.Category5 = true;
            //    }
            //    else
            //    {
            //        cat.Category5 = false;
            //    }
            //    //Set Stay Informed Category
            //    if (plannerMessage.Categories.Contains("Stay Informed"))
            //    {
            //        cat.Category6 = true;
            //    }
            //    else
            //    {
            //        cat.Category6 = false;
            //    }
            //    newTask.BucketId = plannerMessage.BucketId;
            //    newTask.AppliedCategories = cat;

            //    //newTask.Assignments = new PlannerAssignments
            //    //{
            //    //    AdditionalData = new Dictionary<string, object>()
            //    //     {
            //    //      {plannerMessage.Assignee, "{\"@odata.type\":\"#microsoft.graph.plannerAssignment\",\"orderHint\":\" !\"}"}
            //    //     }
            //    //};

            //    string description = plannerMessage.Description.Replace("&amp", "&");
            //    description = description.Replace("[\u201C\u201D]", "\"");
            //    description = description.Replace("“", "\"");
            //    description = description.Replace("’", "'");
            //    description = description.Replace("�", "'");
            //    description = description.Replace("</p>", "");
            //    description = description.Replace("<p>", "");
            //    description = description.Replace("</li>", "");
            //    description = description.Replace("<li>", "");
            //    description = description.Replace("<ul>", "");
            //    description = description.Replace("<ul>", "");

            //    newTask.Details = new PlannerTaskDetails
            //    {
            //        Description = description
            //    };







            //    var tmp = await graphClient.Planner.Tasks
            //                            .Request()
            //                            .AddAsync(newTask);


            //    var tsst = "asd";

           

           // }
        }
    }
}

