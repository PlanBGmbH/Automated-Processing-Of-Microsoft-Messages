// Copyright (c) PlanB. GmbH. All rights reserved.
// Author: Peter Schneider

using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace FunctionApp1.Class
{
    /// <summary>
    /// AppliedCategories.
    /// </summary>
    [Serializable]
    public class AppliedCategories
    {
        /// <summary>
        /// Gets or Sets OdataType.
        /// </summary>
        [JsonProperty("@odata.type")]
        public string OdataType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether category1.
        /// </summary>
        public bool Category1 { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether category2.
        /// </summary>
        public bool Category2 { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether category3.
        /// </summary>
        public bool Category3 { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether category4.
        /// </summary>
        public bool Category4 { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether category5.
        /// </summary>
        public bool Category5 { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether category6.
        /// </summary>
        public bool Category6 { get; set; }
    }
}
