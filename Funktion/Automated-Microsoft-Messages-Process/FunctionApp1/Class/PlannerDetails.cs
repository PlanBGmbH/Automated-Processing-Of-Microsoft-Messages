// Copyright (c) PlanB. GmbH. All rights reserved.
// Author: Peter Schneider

using Newtonsoft.Json;

namespace FunctionApp1.Class
{
    /// <summary>
    /// PlannerDetails.
    /// </summary>
    internal class PlannerDetails
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlannerDetails"/> class.
        /// PlannerDetails.
        /// </summary>
        public PlannerDetails()
        {
        }

        /// <summary>
        /// Gets or Sets Description.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "description", Required = Required.Default)]
        public string Description { get; set; }
    }
}
