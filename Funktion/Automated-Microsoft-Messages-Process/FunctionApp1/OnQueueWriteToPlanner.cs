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

namespace FunctionApp1
{
    public static class OnQueueWriteToPlanner
    {
        static readonly HttpClient client = new HttpClient();
        [FunctionName("OnQueueWriteToPlanner")]
        public static async void Run([QueueTrigger("myqueue-items", Connection = "")] string myQueueItem, ILogger log)
        {
            PlannerMessage plannerMessage;
            plannerMessage = JsonConvert.DeserializeObject<PlannerMessage>(myQueueItem);



            //IConfidentialClientApplication confidentialClient = ConfidentialClientApplicationBuilder
            //    .Create(System.Environment.GetEnvironmentVariable("client_id"))
            //    .WithClientSecret(System.Environment.GetEnvironmentVariable("client_secret"))
            //    .WithAuthority(System.Environment.GetEnvironmentVariable("WEBSITE_AUTH_OPENID_ISSUER"))
            //    .Build();

            //var scopes = new string[] { "https://graph.microsoft.com/.default" };


            //GraphServiceClient graphServiceClient =
            //    new GraphServiceClient(new DelegateAuthenticationProvider(async (requestMessage) =>
            //    {

            //        // Retrieve an access token for Microsoft Graph (gets a fresh token if needed).
            //        var authResult = await confidentialClient
            //           .AcquireTokenForClient(scopes)
            //           .ExecuteAsync();

            //        // Add the access token in the Authorization header of the API request.
            //        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authResult.AccessToken);
            //    })
            //     );

            //var email = System.Environment.GetEnvironmentVariable("usernameemail");
            //var password = System.Environment.GetEnvironmentVariable("userpassword");
            //SecureString securePwd = new SecureString();
            //for (int i = 0; i < password.Length; i++)
            //{
            //    securePwd.AppendChar(password[i]);
            //}
            //User me = await graphServiceClient.Me.Request()
            //                .WithUsernamePassword(email, securePwd)
            //                .GetAsync();



            //var clientId = System.Environment.GetEnvironmentVariable("client_id");
            //var secret = System.Environment.GetEnvironmentVariable("client_secret");
            //var domain = "planbcloud.onmicrosoft.com";
            //var tenantID = System.Environment.GetEnvironmentVariable("tenant");
            //var email = System.Environment.GetEnvironmentVariable("username");
            //var password = System.Environment.GetEnvironmentVariable("userpassword");
            //SecureString securePwd = new SecureString();
            //for (int i = 0; i < password.Length; i++)
            //{
            //    securePwd.AppendChar(password[i]);
            //}

            //var credentials = new Microsoft.IdentityModel.Clients.ActiveDirectory.ClientCredential(clientId, secret);
            //var authContext = new AuthenticationContext($"https://login.microsoftonline.com/{domain}/");
            //var token = await authContext.AcquireTokenAsync("https://graph.microsoft.com/", credentials);
            //var accessToken = token.AccessToken;

            //var graphServiceClient = new GraphServiceClient(
            //    new DelegateAuthenticationProvider((requestMessage) =>
            //    {
            //        requestMessage
            //    .Headers
            //    .Authorization = new AuthenticationHeaderValue("bearer", accessToken);

            //        return Task.CompletedTask;
            //    }));


            //IPublicClientApplication publicClientApplication = PublicClientApplicationBuilder
            //.Create(clientId)
            //.WithTenantId(tenantID)
            //.Build();

            //UsernamePasswordProvider authProvider = new UsernamePasswordProvider(publicClientApplication, scopes);

            //GraphServiceClient graphClient = new GraphServiceClient(authProvider);

            //User me = await graphClient.Me.Request()
            //                .WithUsernamePassword(email, securePwd)
            //                .GetAsync();



            //var tasks = await graphServiceClient.Planner.Plans[System.Environment.GetEnvironmentVariable("messageCenterPlanId")].Tasks
            //                .Request()
            //               .GetAsync();
            string asd = "";

        }
    }
}
