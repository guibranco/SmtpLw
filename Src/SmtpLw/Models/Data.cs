// ***********************************************************************
// Assembly         : SmtpLw
// Author           : Guilherme Branco Stracini
// Created          : 07-05-2020
//
// Last Modified By : Guilherme Branco Stracini
// Last Modified On : 07-05-2020
// ***********************************************************************
// <copyright file="Data.cs" company="Guilherme Branco Stracini ME">
//     © 2020 Guilherme Branco Stracini. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace SmtpLw.Models
{
    /// <summary>
    /// Class Data.
    /// </summary>
    public class Data
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the attributes.
        /// </summary>
        /// <value>The attributes.</value>
        public MessageModel Attributes { get; set; }

        /// <summary>
        /// Gets or sets the links.
        /// </summary>
        /// <value>The links.</value>
        public Links Links { get; set; }

    }
}