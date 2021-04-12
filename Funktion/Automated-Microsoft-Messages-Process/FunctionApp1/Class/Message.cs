// Copyright (c) PlanB. GmbH. All rights reserved.
// Author: Peter Schneider

using System;

namespace FunctionApp1.Class
{
    /// <summary>
    /// Message Claa.
    /// </summary>
#nullable enable
    public class Message
    {
        /// <summary>
        /// Gets or Sets PublishedTime.
        /// </summary>
        public DateTime? PublishedTime { get; set; }

        /// <summary>
        /// Gets or Sets MessageText.
        /// </summary>
        public string? MessageText { get; set; }
    }
}
