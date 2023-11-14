// ***********************************************************************
// Assembly         : SmtpLw
// Author           : Guilherme Branco Stracini
// Created          : 07-05-2020
//
// Last Modified By : Guilherme Branco Stracini
// Last Modified On : 19/01/2023
// ***********************************************************************
// <copyright file="SmtpLwClient.cs" company="Guilherme Branco Stracini ME">
//     © 2020-2023 Guilherme Branco Stracini. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace SmtpLw
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Net.Mail;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using SmtpLw.Models;

    /// <summary>
    /// Class SmtpLwClient.
    /// Implements the <see cref="SmtpLw.ISmtpLwClient" />
    /// </summary>
    /// <seealso cref="SmtpLw.ISmtpLwClient" />

    [ExcludeFromCodeCoverage]
    public class SmtpLwClient : ISmtpLwClient
    {
        /// <summary>
        /// The HTTP client
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="SmtpLwClient" /> class.
        /// </summary>
        /// <param name="authToken">The authentication token.</param>
        public SmtpLwClient(string authToken)
        {
            _httpClient = HttpClientFactory.Create();
            _httpClient.BaseAddress = new Uri("https://api.smtplw.com.br/v1/");
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
            _httpClient.DefaultRequestHeaders.ExpectContinue = false;
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient
                .DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue(@"application/json"));
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("x-auth-token", authToken);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SmtpLwClient" /> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <exception cref="System.ArgumentNullException">httpClient</exception>
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
        /// <returns>Task&lt;System.Int32&gt;.</returns>
        /// <exception cref="SmtpLw.SmtpLwException">There is some errors with your message: {string.Join(",", errors)}</exception>
        public async Task<int> SendMessageAsync(
            MessageModel message,
            CancellationToken cancellationToken
        )
        {
            var errors = ValidateMessage(message);

            if (errors.Any())
                throw new SmtpLwException(
                    $"There is some errors with your message: {string.Join(",", errors)}"
                );

            var jsonContent = JsonConvert.SerializeObject(message);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient
                .PostAsync("messages", contentString, cancellationToken)
                .ConfigureAwait(false);

            return await HandleSendResponseAsync(response, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// get message status as an asynchronous operation.
        /// </summary>
        /// <param name="messageId">The message identifier.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Task&lt;StatusModel&gt;.</returns>
        public async Task<StatusModel> GetMessageStatusAsync(
            int messageId,
            CancellationToken cancellationToken
        )
        {
            var response = await _httpClient
                .GetAsync($"messages/{messageId}", cancellationToken)
                .ConfigureAwait(false);

            return await HandleStatusResponseAsync(response, cancellationToken)
                .ConfigureAwait(false);
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

            if (string.IsNullOrWhiteSpace(message.Subject))
                errors.Add("Message subject cannot be empty or null");

            if (string.IsNullOrWhiteSpace(message.Body))
                errors.Add("Message body cannot be empty or null");

            if (string.IsNullOrWhiteSpace(message.To))
                errors.Add("Message to cannot be empty or null");

            if (string.IsNullOrWhiteSpace(message.From))
                errors.Add("Message from cannot be empty or null");

            if (message.Subject != null && message.Subject.Length > 998)
                errors.Add("Message subject cannot be higher than 998 characters length");

            if (message.Body != null && message.Body.Length > 1048576)
                errors.Add("Message body cannot be higher than 1048576 characters length");

            if (message.Headers != null && message.Headers.Count > 50)
                errors.Add("Cannot send more than 50 headers per message");

            return errors.ToArray();
        }

        /// <summary>
        /// Handles the send response asynchronous.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>System.Threading.Tasks.Task&lt;System.String&gt;.</returns>
        /// <exception cref="System.Net.Mail.SmtpException">Invalid authorization token</exception>
        /// <exception cref="SmtpLw.SmtpLwException">Unable to send message, for the following reason(s): {string.Join(",", errors)}</exception>
        /// <exception cref="SmtpLw.SmtpLwException"></exception>
        private static async Task<int> HandleSendResponseAsync(
            HttpResponseMessage response,
            CancellationToken cancellationToken
        )
        {
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new SmtpException("Invalid authorization token");
            }

            ResponseModel responseModel;

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                responseModel = await response
                    .Content
                    .ReadAsAsync<ResponseModel>(cancellationToken)
                    .ConfigureAwait(false);
                var errors = responseModel.Errors?.Select(e => e.Detail) ?? new List<string>();

                throw new SmtpLwException(
                    $"Unable to send message, for the following reason(s): {string.Join(",", errors)}"
                );
            }

            if (response.StatusCode != HttpStatusCode.Created)
            {
                var responseContent = await response
                    .Content
                    .ReadAsStringAsync()
                    .ConfigureAwait(false);
                throw new SmtpLwException((int)response.StatusCode, responseContent);
            }

            responseModel = await response
                .Content
                .ReadAsAsync<ResponseModel>(cancellationToken)
                .ConfigureAwait(false);

            return responseModel.Data.Id;
        }

        /// <summary>
        /// handle status response as an asynchronous operation.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>StatusModel.</returns>
        /// <exception cref="SmtpLw.SmtpLwException"></exception>
        private static async Task<StatusModel> HandleStatusResponseAsync(
            HttpResponseMessage response,
            CancellationToken cancellationToken
        )
        {
            if (response.StatusCode != HttpStatusCode.OK)
            {
                var responseContent = await response
                    .Content
                    .ReadAsStringAsync()
                    .ConfigureAwait(false);
                throw new SmtpLwException((int)response.StatusCode, responseContent);
            }

            return await response
                .Content
                .ReadAsAsync<StatusModel>(cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
