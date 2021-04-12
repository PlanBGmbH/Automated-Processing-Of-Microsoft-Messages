// Copyright (c) PlanB. GmbH. All rights reserved.
// Author: Peter Schneider

using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace FunctionApp1.Class
{
    /// <summary>
    /// Details.
    /// </summary>
    [Serializable]
    public class Details
    {
        /// <summary>
        /// Gets or Sets Description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or Sets OdataType.
        /// </summary>
        [JsonProperty("@odata.type")]
        public string OdataType { get; set; }
    }
}
