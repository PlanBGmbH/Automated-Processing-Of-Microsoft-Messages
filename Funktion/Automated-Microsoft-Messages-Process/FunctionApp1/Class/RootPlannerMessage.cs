// Copyright (c) PlanB. GmbH. All rights reserved.
// Author: Peter Schneider

using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionApp1.Class
{
    /// <summary>
    /// RootPlannerMessage.
    /// </summary>
    [Serializable]
    internal class RootPlannerMessage
    {
        /// <summary>
        /// Gets or Sets RootPlannerMessage1.
        /// </summary>
        public List<PlannerMessage> RootPlannerMessage1 { get; set; }
    }
}
