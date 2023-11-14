// ***********************************************************************
// Assembly         : SmtpLw
// Author           : Guilherme Branco Stracini
// Created          : 07-05-2020
//
// Last Modified By : Guilherme Branco Stracini
// Last Modified On : 19/01/2023
// ***********************************************************************
// <copyright file="ISmtpLwClient.cs" company="Guilherme Branco Stracini ME">
//     © 2020-2023 Guilherme Branco Stracini. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace SmtpLw
{
    using System.Threading;
    using System.Threading.Tasks;
    using SmtpLw.Models;

    /// <summary>
    /// Interface ISmtpLwClient
    /// </summary>
    public interface ISmtpLwClient
    {
        /// <summary>
        /// Sends the message asynchronous.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Task&lt;System.Int32&gt;.</returns>
        Task<int> SendMessageAsync(MessageModel message, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the message status asynchronous.
        /// </summary>
        /// <param name="messageId">The message identifier.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Task&lt;StatusModel&gt;.</returns>
        Task<StatusModel> GetMessageStatusAsync(int messageId, CancellationToken cancellationToken);
    }
}
