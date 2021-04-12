// Copyright (c) PlanB. GmbH. All rights reserved.
// Author: Peter Schneider

using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;

namespace FunctionApp1.Class
{
    /// <summary>
    /// Token.
    /// </summary>
    internal class Token
    {
        /// <summary>
        /// Gets or Sets AccessToken.
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets or Sets TokenType.
        /// </summary>
        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        /// <summary>
        /// Gets or Sets ExpiresIn.
        /// </summary>
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        /// <summary>
        /// Gets or Sets RefreshToken.
        /// </summary>
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}
