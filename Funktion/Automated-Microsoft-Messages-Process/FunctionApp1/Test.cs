// Copyright (c) PlanB. GmbH. All rights reserved.
// Author: Peter Schneider

using Microsoft.Graph;

namespace FunctionApp1
{
    internal class Test
    {
        public string PlanId { get; set; }
        public string BucketId { get; set; }
        public string Title { get; set; }
        public PlannerAssignments Assignments { get; set; }
    }
}