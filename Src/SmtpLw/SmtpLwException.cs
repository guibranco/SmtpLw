// ***********************************************************************
// Assembly         : SmtpLw
// Author           : Guilherme Branco Stracini
// Created          : 07-05-2020
//
// Last Modified By : Guilherme Branco Stracini
// Last Modified On : 19/01/2023
// ***********************************************************************
// <copyright file="SmtpLwException.cs" company="Guilherme Branco Stracini ME">
//     © 2020-2023 Guilherme Branco Stracini. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace SmtpLw
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Class SmtpLwException.
    /// Implements the <see cref="System.Exception" />
    /// </summary>
    /// <seealso cref="System.Exception" />
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class SmtpLwException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Exception"></see> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public SmtpLwException(string message)
            : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SmtpLwException" /> class.
        /// </summary>
        /// <param name="httpStatusCode">The HTTP status code.</param>
        /// <param name="content">The content.</param>
        public SmtpLwException(int httpStatusCode, string content)
            : base($"Response {httpStatusCode} with content {content}") { }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Exception"></see> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public SmtpLwException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
