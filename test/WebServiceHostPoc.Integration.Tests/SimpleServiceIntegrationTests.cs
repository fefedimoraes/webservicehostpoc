using System;
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
            const string baseUrl = "http://127.0.0.1:8080/myservice";

            HttpClient = new HttpClient { BaseAddress = new Uri(baseUrl) };

            WebHost = new WebHostBuilder()
                .CaptureStartupErrors(true)
                .UseKestrel()
                .UseWebServiceHost(new MyService())
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

        [Fact]
        public async Task OnGettingWriteToConsole_ShouldRespondNoContent_ShouldWriteToConsole()
        {
            const string message = "message with whitespaces";

            // Arrange
            using (var stringWriter = new StringWriter())
            using (new ConsoleStreamSwitcher(stringWriter))
            {
                // Act
                var response = await HttpClient.GetAsync($"writetoconsole/{message}");

                // Assert
                response.StatusCode.Should().Be(HttpStatusCode.NoContent);
                stringWriter.ToString().Should().Be(message + Environment.NewLine);
            }
        }

        private interface IMyService
        {
            [Get]
            void WriteToConsole(string message);

            [Get]
            int Sum(int a, int b);
        }

        private class MyService : IMyService
        {
            public void WriteToConsole(string message) => Console.WriteLine(message);

            public int Sum(int a, int b) => a + b;
        }
    }
}
