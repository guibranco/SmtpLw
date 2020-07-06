
using Microsoft.Extensions.Configuration;
using SmtpLw.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace SmtpLw.Tests
{
    /// <summary>
    /// Class MessageValidation.
    /// </summary>
    public class SmtpLwClientTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        /// <summary>
        /// The client
        /// </summary>
        private readonly ISmtpLwClient _client;

        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration _configuration;

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
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(@"application/json"));

            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("x-auth-token", authToken);

            return httpClient;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SmtpLwClientTests"/> class.
        /// </summary>
        public SmtpLwClientTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _configuration = new ConfigurationBuilder()
                .AddUserSecrets<SmtpLwClientTests>()
                .AddEnvironmentVariables()
                .Build();

            var authToken = string.IsNullOrWhiteSpace(_configuration["authToken"])
                ? "__YOUR_AUTH_TOKEN_HERE"
                : _configuration["authToken"];

            _client = new SmtpLwClient(CreateHttpClient(authToken));
        }

        /// <summary>
        /// Defines the test method ValidateSendMessage.
        /// </summary>
        [Fact]
        public async Task ValidateSendMessage()
        {
            var model = new MessageModel
            {
                Body = "Test",
                Subject = "Subject",
                To = _configuration["toAddress"],
                From = _configuration["fromAddress"]
            };

            var messageId = await _client.SendMessageAsync(model, CancellationToken.None).ConfigureAwait(false);

            _testOutputHelper.WriteLine("Message id: {0}", messageId);

            Assert.True(messageId > 0);
        }
    }
}
