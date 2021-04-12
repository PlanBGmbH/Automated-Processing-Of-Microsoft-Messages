using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionApp1.Class
{
    class PlannerDetails
    {
        //
        // Summary:
        //     The PlannerTaskDetails constructor
        public PlannerDetails() {

        }


       
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "description", Required = Required.Default)]
        public string Description { get; set; }
     
    }
}
