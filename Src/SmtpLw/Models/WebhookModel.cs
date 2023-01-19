// ***********************************************************************
// Assembly         : SmtpLw
// Author           : Guilherme Branco Stracini
// Created          : 07-05-2020
//
// Last Modified By : Guilherme Branco Stracini
// Last Modified On : 07-05-2020
// ***********************************************************************
// <copyright file="WebhookModel.cs" company="Guilherme Branco Stracini ME">
//     © 2020 Guilherme Branco Stracini. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace SmtpLw.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// Class WebhookModel.
    /// </summary>
    public class WebhookModel
    {
        /// <summary>
        /// Gets or sets the bounce description.
        /// </summary>
        /// <value>The bounce description.</value>
        [JsonProperty("bounce_description")]
        public string BounceDescription { get; set; }


        /// <summary>
        /// Gets or sets the bounce code.
        /// </summary>
        /// <value>The bounce code.</value>
        [JsonProperty("bounce_code")]
        public string BounceCode { get; set; }

        /// <summary>
        /// Gets or sets the sender.
        /// </summary>
        /// <value>The sender.</value>
        public string Sender { get; set; }

        /// <summary>
        /// Gets or sets to.
        /// </summary>
        /// <value>To.</value>
        public string To { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>The subject.</value>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the x SMTP lw.
        /// </summary>
        /// <value>The x SMTP lw.</value>
        [JsonProperty("x-smtplw")]
        public string XSmtpLw { get; set; }
    }
}
