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







            using (StreamReader r = new StreamReader(@"C:\Users\admin\source\repos\Automated-Processing-Of-Microsoft-Messages\Funktion\Automated-Microsoft-Messages-Process\FunctionApp1\Configuration\AssigneeListValues.json"))
            {
                string json = r.ReadToEnd();
                assigneeList = JsonConvert.DeserializeObject<RootAssigneeList>(json);
            }



            foreach (var message in messageRoot.value)
            {

                foreach (var assignee in assigneeList.AssigneeList)
                {
                    try
                    {
                        if (assignee.bucketName == message.AffectedWorkloadDisplayNames[0])
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



                            return JsonConvert.SerializeObject(plannerMessage);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                   
                }

            }

            return "";

        }


    }
}
