// Copyright (c) PlanB. GmbH. All rights reserved.
// Author: Peter Schneider

using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace FunctionApp1.Class
{
    /// <summary>
    /// PlannerTask.
    /// </summary>
    [Serializable]
    public class PlannerTask
    {
        /// <summary>
        /// Gets or sets a value indicating whether AppliedCategories.
        /// </summary>
        public AppliedCategories AppliedCategories { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether BucketId.
        /// </summary>
        public string BucketId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether OrderHint.
        /// </summary>
        public string OrderHint { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether PlanId.
        /// </summary>
        public string PlanId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Details.
        /// </summary>
        public Details Details { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether CompletedDateTime.
        /// </summary>
        public DateTimeOffset? CompletedDateTime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether StartDateTime.
        /// </summary>
        public DateTimeOffset? StartDateTime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether CreatedDateTime.
        /// </summary>
        public DateTimeOffset? CreatedDateTime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether PercentComplete.
        /// </summary>
        public int? PercentComplete { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether DueDateTime.
        /// </summary>
        public DateTimeOffset? DueDateTime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether OdataType.
        /// </summary>
        [JsonProperty("@odata.type")]
        public string OdataType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether AffectedWorkloadDisplayNames.
        /// </summary>
        public string[]? AffectedWorkloadDisplayNames { get; set; }
    }
}
