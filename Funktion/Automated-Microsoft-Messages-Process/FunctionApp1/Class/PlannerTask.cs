using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionApp1.Class
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    [Serializable]
    public class AppliedCategories
    {
        [JsonProperty("@odata.type")]
        public string OdataType { get; set; }
        public bool category1 { get; set; }
        public bool category2 { get; set; }
        public bool category3 { get; set; }
        public bool category4 { get; set; }
        public bool category5 { get; set; }
        public bool category6 { get; set; }
    }
    [Serializable]
    public class Details
    {
        public string description { get; set; }

        [JsonProperty("@odata.type")]
        public string OdataType { get; set; }
    }
    [Serializable]
    public class PlannerTask
    {
        public AppliedCategories appliedCategories { get; set; }
        public string bucketId { get; set; }
        public string orderHint { get; set; }
        public string planId { get; set; }
        public string title { get; set; }
        public Details details { get; set; }
        public DateTimeOffset? CompletedDateTime { get; set; }
        public DateTimeOffset? StartDateTime { get; set; }
        public DateTimeOffset? CreatedDateTime { get; set; }
        public int? PercentComplete { get; set; }
        public DateTimeOffset? DueDateTime { get; set; }

        [JsonProperty("@odata.type")]
        public string OdataType { get; set; }
    }

}
