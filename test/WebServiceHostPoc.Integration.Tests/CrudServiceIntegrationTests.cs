using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using WebServiceHostPoc.Attributes.HttpMethods;
using WebServiceHostPoc.Attributes.Parameters;
using Xunit;

namespace WebServiceHostPoc.Integration.Tests
{
    public class CrudServiceIntegrationTests
    {
        public CrudServiceIntegrationTests()
        {
            const string baseUrl = "http://127.0.0.1:8081/crudservice";

            HttpClient = new HttpClient { BaseAddress = new Uri(baseUrl) };

            WebHost = new WebHostBuilder()
                .CaptureStartupErrors(true)
                .UseKestrel()
                .UseWebServiceHost(new CrudService())
                .UseUrls(baseUrl)
                .Build();

            WebHost.Start();
        }

        public IWebHost WebHost { get; }

        public HttpClient HttpClient { get; }

        public void Dispose()
        {
            WebHost.Dispose();
            HttpClient.Dispose();
        }

        [Fact]
        public async Task OnStoringAndReading_ShouldRespondAccordingly()
        {
            const string id = "123";

            var complex = new Complex
            {
                IntegerProperty = 1,
                StringProperty = "Hello",
                InnerComplexInstance = new Complex
                {
                    IntegerProperty = 2,
                    StringProperty = "World"
                }
            };

            // Act
            var storeResponse = await HttpClient.PutAsJsonAsync($"store/{id}", complex);
            var readExistingResponse = await HttpClient.GetAsync($"read/{id}");

            // Assert
            storeResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
            readExistingResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var complexFromService = await readExistingResponse.Content.DeserializeAsJsonAsync<Complex>();
            complexFromService.ShouldBeEquivalentTo(complex);
        }

        private class Complex
        {
            public int IntegerProperty { get; set; }

            public string StringProperty { get; set; }

            public Complex InnerComplexInstance { get; set; }
        }

        private interface ICrudService
        {
            [Get]
            Complex Read([Uri] string id);

            [Put]
            void Store([Uri] string id, [Body] Complex instance);
        }

        private class CrudService : ICrudService
        {
            private IDictionary<string, Complex> ComplexInstances { get; } = new Dictionary<string, Complex>();

            public Complex Read(string id) => ComplexInstances[id];

            public void Store(string id, Complex instance) => ComplexInstances[id] = instance;
        }
    }
}