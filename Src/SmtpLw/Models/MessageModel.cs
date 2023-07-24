// ***********************************************************************
// Assembly         : SmtpLw
// Author           : Guilherme Branco Stracini
// Created          : 07-05-2020
//
// Last Modified By : Guilherme Branco Stracini
// Last Modified On : 19/01/2023
// ***********************************************************************
// <copyright file="MessageModel.cs" company="Guilherme Branco Stracini ME">
//     © 2020-2023 Guilherme Branco Stracini. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace SmtpLw.Models
{
    using Newtonsoft.Json;

    using System.Collections.Generic;

    /// <summary>
    /// Class MessageModel.
    /// </summary>
    public class MessageModel
    {
        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>The subject.</value>
        [JsonProperty("subject")]
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        /// <value>The body.</value>
        [JsonProperty("body")]
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets from.
        /// </summary>
        /// <value>From.</value>
        [JsonProperty("from")]
        public string From { get; set; }

        /// <summary>
        /// Gets or sets to.
        /// </summary>
        /// <value>To.</value>
        [JsonProperty("to")]
        public string To { get; set; }

        /// <summary>
        /// Gets or sets the carbon copy.
        /// </summary>
        /// <value>The carbon copy.</value>
        [JsonProperty("cc")]
        public string[] CarbonCopy { get; set; }

        /// <summary>
        /// Gets or sets the blind carbon copy.
        /// </summary>
        /// <value>The blind carbon copy.</value>
        [JsonProperty("bcc")]
        public string[] BlindCarbonCopy { get; set; }

        /// <summary>
        /// Gets or sets the headers.
        /// </summary>
        /// <value>The headers.</value>
        [JsonProperty("headers")]
        public Dictionary<string, string> Headers { get; set; }
    }
}
