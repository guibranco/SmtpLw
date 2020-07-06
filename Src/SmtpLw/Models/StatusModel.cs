// ***********************************************************************
// Assembly         : SmtpLw
// Author           : Guilherme Branco Stracini
// Created          : 07-05-2020
//
// Last Modified By : Guilherme Branco Stracini
// Last Modified On : 07-05-2020
// ***********************************************************************
// <copyright file="StatusModel.cs" company="Guilherme Branco Stracini ME">
//     © 2020 Guilherme Branco Stracini. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace SmtpLw.Models
{
    /// <summary>
    /// Class StatusModel.
    /// Implements the <see cref="SmtpLw.Models.MessageModel" />
    /// </summary>
    /// <seealso cref="SmtpLw.Models.MessageModel" />
    public class StatusModel : MessageModel
    {

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the response.
        /// </summary>
        /// <value>The response.</value>
        public string Response { get; set; }
    }
}
