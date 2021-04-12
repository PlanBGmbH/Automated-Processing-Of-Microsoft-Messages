using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionApp1.Class
{
    class PlannerTask
    {
        public int activeChecklistItemCount { get; set; }
        public AppliedCategories appliedCategories { get; set; }
        public string assigneePriority { get; set; }
        public Assignments assignments { get; set; }
        public string bucketId { get; set; }
        public int checklistItemCount { get; set; }
        public CompletedBy completedBy { get; set; }
        public string completedDateTime { get; set; }
        public string conversationThreadId { get; set; }
        public CreatedBy createdBy { get; set; }
        public string createdDateTime { get; set; }
        public string dueDateTime { get; set; }
        public bool hasDescription { get; set; }
        public string id { get; set; }
        public string orderHint { get; set; }
        public int percentComplete { get; set; }
        public int priority { get; set; }
        public string planId { get; set; }
        public string previewType { get; set; }
        public int referenceCount { get; set; }
        public string startDateTime { get; set; }
        public string title { get; set; }
    }
    public class AppliedCategories
    {
        [JsonProperty("@odata.type")]
        public string OdataType { get; set; }
    }

    public class Assignments
    {
        [JsonProperty("@odata.type")]
        public string OdataType { get; set; }
    }

    public class CompletedBy
    {
        [JsonProperty("@odata.type")]
        public string OdataType { get; set; }
    }

    public class CreatedBy
    {
        [JsonProperty("@odata.type")]
        public string OdataType { get; set; }
    }
}
