// Copyright (c) PlanB. GmbH. All rights reserved.
// Author: Peter Schneider

using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionApp1.Class
{
    /// <summary>
    /// PlannerMessage.
    /// </summary>
    [Serializable]
    internal class PlannerMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlannerMessage"/> class.
        /// PlannerMessage.
        /// </summary>
        public PlannerMessage()
        {
        }

        /// <summary>
        /// Gets or Sets Product.
        /// </summary>
        public string Product { get; set; }

        /// <summary>
        /// Gets or Sets Product.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or Sets Id.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or Sets Title.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or Sets Description.
        /// </summary>
        public string Categories { get; set; }

        /// <summary>
        /// Gets or Sets Categories.
        /// </summary>
        public DateTime? DueDate { get; set; }

        /// <summary>
        /// Gets or Sets DueDate.
        /// </summary>
        public DateTime? Updated { get; set; }

        /// <summary>
        /// Gets or Sets Updated.
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// Gets or Sets Reference.
        /// </summary>
        public string BucketId { get; set; }

        /// <summary>
        /// Gets or Sets BucketId.
        /// </summary>
        public string Assignee { get; set; }
    }
}
