using System;
using System.Net.Http;
using System.Net.Http.Headers;
using FunctionApp1.Class;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FunctionApp1
{
    public static class Function1
    {
        static readonly HttpClient client = new HttpClient();

        [FunctionName("OnTimerGetMessages")]
        public static async void OnTimerGetMessages([TimerTrigger("* * * * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");


            //
            HttpResponseMessage response;

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
            HttpResponseMessage MessageResponse = await client.GetAsync(messageUri);
            jsonContent = await MessageResponse.Content.ReadAsStringAsync();
            Root messageRoot = JsonConvert.DeserializeObject<Root>(jsonContent);

            var test = 1;



        }


        [FunctionName("OnQueuePostMessagesIntoPlanner")]
        public static void Run([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }




    }
}
