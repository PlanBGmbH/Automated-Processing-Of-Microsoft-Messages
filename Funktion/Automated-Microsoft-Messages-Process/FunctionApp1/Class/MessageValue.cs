using System;
using System.Collections.Generic;



namespace FunctionApp1.Class
{
    /// <summary>
    /// Summary description for Class1
    /// </summary>
    public class MessageValue
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public object? Title { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? Status { get; set; }
        public List<Message>? Messages { get; set; }
        public DateTime? LastUpdatedTime { get; set; }
        public string? Workload { get; set; }
        public string? WorkloadDisplayName { get; set; }
        public string? Feature { get; set; }
        public string? FeatureDisplayName { get; set; }
    }
}
