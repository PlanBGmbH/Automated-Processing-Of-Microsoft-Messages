// Copyright (c) PlanB. GmbH. All rights reserved.
// Author: Peter Schneider

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FunctionApp1.Class
{
    /// <summary>
    /// Summary description for Class1
    /// </summary>
    public class MessageValue
    {
        // public List<string>? Workload { get; set; }
        public string[] AffectedWorkloadDisplayNames { get; set; }
        public string Id { get; set; }
        public string Title { get; set; }
        public string MessageType { get; set; }
        public string ActionType { get; set; }
        public string Classification { get; set; }
        public string Category { get; set; }
        public string ExternalLink { get; set; }
        public DateTime? ActionRequiredByDate { get; set; }

        // public string? Status { get; set; }
        public Message[] Messages { get; set; }
        public DateTime? LastUpdatedTime { get; set; }
    }

    public class test
    {
        string WorkloadDisplayName { get; set; }
    }
}
