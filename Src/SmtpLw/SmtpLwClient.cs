// ***********************************************************************
// Assembly         : SmtpLw
// Author           : Guilherme Branco Stracini
// Created          : 07-05-2020
//
// Last Modified By : Guilherme Branco Stracini
// Last Modified On : 07-05-2020
// ***********************************************************************
// <copyright file="SmtpLwClient.cs" company="Guilherme Branco Stracini ME">
//     © 2020 Guilherme Branco Stracini. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using SmtpLw.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmtpLw
{
    /// <summary>
    /// Class SmtpLwClient.
    /// Implements the <see cref="SmtpLw.ISmtpLwClient" />
    /// </summary>
    /// <seealso cref="SmtpLw.ISmtpLwClient" />
    public class SmtpLwClient : ISmtpLwClient
    {
        /// <summary>
        /// The HTTP client
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="SmtpLwClient"/> class.
        /// </summary>
        public SmtpLwClient(string authToken)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.smtplw.com.br/v1/"),
                Timeout = TimeSpan.FromSeconds(30)
            };
            _httpClient.DefaultRequestHeaders.ExpectContinue = false;
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(@"application/json"));
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("x-auth-token", authToken);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SmtpLwClient" /> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <exception cref="ArgumentNullException">httpClient</exception>
        public SmtpLwClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        #region Implementation of ISmtpLwClient


        /// <summary>
        /// send message as an asynchronous operation.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        public async Task<string> SendMessageAsync(MessageModel message, CancellationToken cancellationToken)
        {
            var errors = ValidateMessage(message);

            if (errors.Any())
                throw new SmtpLwException($"There is some errors with your message: {string.Join(",", errors)}");

            var jsonContent = JsonConvert.SerializeObject(message);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("messages", contentString, cancellationToken).ConfigureAwait(false);

            return await HandleSendResponseAsync(response, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// get message status as an asynchronous operation.
        /// </summary>
        /// <param name="messageId">The message identifier.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Task&lt;StatusModel&gt;.</returns>
        public async Task<StatusModel> GetMessageStatusAsync(string messageId, CancellationToken cancellationToken)
        {

            var response = await _httpClient.GetAsync($"messages/{messageId}", cancellationToken).ConfigureAwait(false);

            return await HandleStatusResponseAsync(response, cancellationToken).ConfigureAwait(false);
        }

        #endregion

        /// <summary>
        /// Validates the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>System.String[].</returns>
        private static string[] ValidateMessage(MessageModel message)
        {
            var errors = new Collection<string>();

            if (message.Subject.Length > 998)
                errors.Add("Message subject cannot be higher than 998 characters length");

            if (message.Body.Length > 1048576)
                errors.Add("Message body cannot be higher than 1048576 characters length");

            if (message.Headers.Count > 50)
                errors.Add("Cannot send more than 50 headers per message");

            return errors.ToArray();
        }

        /// <summary>
        /// Handles the send response asynchronous.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>System.Threading.Tasks.Task&lt;System.String&gt;.</returns>
        private static async Task<string> HandleSendResponseAsync(HttpResponseMessage response, CancellationToken cancellationToken)
        {
            if (response.StatusCode != HttpStatusCode.Created)
            {
                var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                throw new SmtpLwException((int)response.StatusCode, responseContent);
            }

            var responseModel = await response.Content.ReadAsAsync<ResponseModel>(cancellationToken).ConfigureAwait(false);

            if (!responseModel.Status.Equals("ok", StringComparison.InvariantCultureIgnoreCase))
                throw new SmtpLwException($"The API returned unexpected response: {responseModel.Status}");

            var messageId = response.Headers.GetValues("x-api-message-id").FirstOrDefault();
            var location = response.Headers.Location;

            if (string.IsNullOrWhiteSpace(messageId) && location == null)
                throw new SmtpException($"Cannot find the message id in response");

            return !string.IsNullOrWhiteSpace(messageId)
                ? messageId
                : location.AbsolutePath.Replace("/v1/messages/", "");
        }

        /// <summary>
        /// handle status response as an asynchronous operation.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>StatusModel.</returns>
        /// <exception cref="SmtpLw.SmtpLwException"></exception>
        private static async Task<StatusModel> HandleStatusResponseAsync(HttpResponseMessage response, CancellationToken cancellationToken)
        {
            if (response.StatusCode != HttpStatusCode.OK)
            {
                var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                throw new SmtpLwException((int)response.StatusCode, responseContent);
            }

            return await response.Content.ReadAsAsync<StatusModel>(cancellationToken).ConfigureAwait(false);
        }
    }
}
