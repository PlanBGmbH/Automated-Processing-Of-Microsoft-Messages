// Copyright (c) PlanB. GmbH. All rights reserved.
// Author: Peter Schneider

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionApp1.Class
{
    internal class Assignee
    {
        public Assignee(string odataType, string orderHint)
        {
            this.OdataType = odataType;
            this.orderHint = orderHint;
        }

        [JsonProperty("@odata.type")]
            public string OdataType { get; set; }
            public string orderHint { get; set; }
       
    }
}
