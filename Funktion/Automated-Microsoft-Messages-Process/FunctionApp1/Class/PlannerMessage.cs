using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionApp1.Class
{
    class PlannerMessage
    {
        public string Product { get; set; }
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Categories { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? Updated { get; set; }       
        public string Reference { get; set; }  
        public string BucketId { get; set; }
        public string Assignee { get; set; }


        public PlannerMessage()
        {
        }
    }
}
