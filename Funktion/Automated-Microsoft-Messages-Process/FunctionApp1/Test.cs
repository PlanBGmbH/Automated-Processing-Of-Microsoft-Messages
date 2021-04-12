// Copyright (c) PlanB. GmbH. All rights reserved.
// Author: Peter Schneider

using Microsoft.Graph;

namespace FunctionApp1
{
    /// <summary>
    /// Test.
    /// </summary>
    internal class Test
    {
        /// <summary>
        /// Gets or Sets PlanId.
        /// </summary>
        public string PlanId { get; set; }

        /// <summary>
        /// Gets or Sets BucketId.
        /// </summary>
        public string BucketId { get; set; }

        /// <summary>
        /// Gets or Sets Title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or Sets Assignments.
        /// </summary>
        public PlannerAssignments Assignments { get; set; }
    }
}