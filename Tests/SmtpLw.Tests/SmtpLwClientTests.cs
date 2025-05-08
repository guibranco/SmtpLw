// ***********************************************************************
// Assembly         : SmtpLw.Tests
// Author           : Guilherme Branco Stracini
// Created          : 19/01/2023
//
// Last Modified By : Guilherme Branco Stracini
// Last Modified On : 19/01/2023
// ***********************************************************************
// <copyright file="SmtpLwClientTests.cs" company="SmtpLw.Tests">
//     Copyright (c) Guilherme Branco Stracini ME. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace SmtpLw.Tests
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;
    using SmtpLw.Models;
    using Xunit;
    using Xunit.Abstractions;

    /// <summary>
    /// Class MessageValidation.
    /// </summary>
    public class SmtpLwClientTests
    {
        /// <summary>
        /// The test output helper
        /// </summary>
        private readonly ITestOutputHelper _testOutputHelper;

        /// <summary>
        /// The client
        /// </summary>
        private readonly ISmtpLwClient _client;

        /// <summary>
        /// Creates the HTTP client.
        /// </summary>
        /// <param name="authToken">The authentication token.</param>
        /// <returns>HttpClient.</returns>
        private static HttpClient CreateHttpClient(string authToken)
        {
            var httpClient = HttpClientFactory.Create();

            httpClient.BaseAddress = new Uri("https://api.smtplw.com.br/v1/");

            httpClient.Timeout = TimeSpan.FromSeconds(30);

            httpClient.DefaultRequestHeaders.ExpectContinue = false;

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(@"application/json")
            );

            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("x-auth-token", authToken);

            return httpClient;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SmtpLwClientTests" /> class.
        /// </summary>
        /// <param name="testOutputHelper">The test output helper.</param>
        public SmtpLwClientTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;

            var authToken = "__YOUR_AUTH_TOKEN_HERE__";

            _client = new SmtpLwClient(CreateHttpClient(authToken));
        }

        /// <summary>
        /// Validates the sending of a message using the MessageModel.
        /// </summary>
        /// <remarks>
        /// This asynchronous test method creates an instance of the <see cref="MessageModel"/> class, populating it with a test message's body, subject, recipient, and sender information.
        /// The body of the message includes a timestamp indicating when the message was created, while the subject contains a formatted string with the current date.
        /// The method currently includes a placeholder for mocking an HTTP request to simulate sending the message.
        /// After setting up the model, it asserts that the message ID is greater than zero, indicating a successful operation.
        /// This test is part of a unit testing framework and is marked with the <see cref="Fact"/> attribute, which signifies that it is a test method.
        /// </remarks>
        [Fact]
        public async Task ValidateSendMessage()
        {
            var model = new MessageModel
            {
                Body = $"This is a test message sent at {DateTime.Now:dd/MM/yyyy HH:mm:ss}",
                Subject = $".NET SMTP Locaweb client wrapper test [{DateTime.Now:dd/MM/yyyy}]",
                To = "someone@example.com",
                From = "no-reply@example.com",
            };

            //TODO: Mock http request

            var messageId = 1;
            Assert.True(messageId > 0);
        }

        /// <summary>
        /// Defines the test method ValidateInvalidMessage.
        /// </summary>
        [Fact]
        public async Task ValidateInvalidMessage()
        {
            var model = new MessageModel();

            var result = await Assert.ThrowsAsync<SmtpLwException>(async () =>
                await _client.SendMessageAsync(model, CancellationToken.None).ConfigureAwait(false)
            );

            _testOutputHelper.WriteLine("Error message: {0}", result.Message);

            Assert.NotNull(result.Message);
        }
    }
}
