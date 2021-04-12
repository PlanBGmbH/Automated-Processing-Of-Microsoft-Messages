// Copyright (c) PlanB. GmbH. All rights reserved.
// Author: Peter Schneider

using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace FunctionApp1.Class
{
    /// <summary>
    /// MessageValue.
    /// </summary>
    public class MessageValue
    {
        /// <summary>
        /// Gets or Sets AffectedWorkloadDisplayNames.
        /// </summary>
        public string[] AffectedWorkloadDisplayNames { get; set; }

        /// <summary>
        /// Gets or Sets Id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or Sets Title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or Sets MessageType.
        /// </summary>
        public string MessageType { get; set; }

        /// <summary>
        /// Gets or Sets ActionType.
        /// </summary>
        public string ActionType { get; set; }

        /// <summary>
        /// Gets or Sets Classification.
        /// </summary>
        public string Classification { get; set; }

        /// <summary>
        /// Gets or Sets Category.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or Sets ExternalLink.
        /// </summary>
        public string ExternalLink { get; set; }

        /// <summary>
        /// Gets or Sets ActionRequiredByDate.
        /// </summary>
        public DateTime? ActionRequiredByDate { get; set; }

        /// <summary>
        /// Gets or Sets Messages.
        /// </summary>
        public Message[] Messages { get; set; }

        /// <summary>
        /// Gets or Sets LastUpdatedTime.
        /// </summary>
        public DateTime? LastUpdatedTime { get; set; }
    }
}
