using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using WebServiceHostPoc.Attributes.HttpMethods;
using Xunit;

namespace WebServiceHostPoc.Integration.Tests
{
    public class SimpleServiceIntegrationTests : IDisposable
    {
        public SimpleServiceIntegrationTests()
        {
            const string baseUrl = "http://127.0.0.1:8080/simpleservice";

            HttpClient = new HttpClient { BaseAddress = new Uri(baseUrl) };

            WebHost = new WebHostBuilder()
                .CaptureStartupErrors(true)
                .UseKestrel()
                .UseWebServiceHost(new SimpleService())
                .UseUrls(baseUrl)
                .Build();

            WebHost.Start();
        }

        private IWebHost WebHost { get; }

        private HttpClient HttpClient { get; }

        public void Dispose()
        {
            WebHost.Dispose();
            HttpClient.Dispose();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("message with whitespaces")]
        public async Task OnGettingWriteToConsole_ShouldRespondNoContent_ShouldWriteToConsole(string message)
        {
            // Arrange
            using (var stringWriter = new StringWriter())
            using (new ConsoleStreamSwitcher(stringWriter))
            {
                // Act
                var response = await HttpClient.GetAsync($"writetoconsole?message={message}");

                // Assert
                response.StatusCode.Should().Be(HttpStatusCode.NoContent);
                stringWriter.ToString().Should().Be(message + Environment.NewLine);
            }
        }

        [Theory]
        [InlineData(10, 7, 17)]
        [InlineData(-10, -7, -17)]
        public async Task OnGettingSum_ShouldRespondOk_ShouldContainResultInResponseBody(int a, int b, int expected)
        {
            // Act
            var response = await HttpClient.GetAsync($"sum?a={a}&b={b}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseContent = await response.Content.DeserializeAsJsonAsync<int>();
            responseContent.Should().Be(expected);
        }

        private interface ISimpleService
        {
            [Get]
            void WriteToConsole(string message);

            [Get]
            int Sum(int a, int b);
        }

        private class SimpleService : ISimpleService
        {
            public void WriteToConsole(string message) => Console.WriteLine(message);

            public int Sum(int a, int b) => a + b;
        }
    }
}
