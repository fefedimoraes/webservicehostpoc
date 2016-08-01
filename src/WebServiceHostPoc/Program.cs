using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using WebServiceHostPoc.Attributes;
using Microsoft.AspNetCore.Routing;
using System.Net.Http;
using WebServiceHostPoc.Extensions;

namespace WebServiceHostPoc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webHost = new WebServiceHostBuilder<MyService>(new MyService())
                .UseUrls("http://127.0.0.1:8080/myservice")
                .Build();

            using (webHost)
            {
                webHost.Start();
                Console.WriteLine("Running!");
                Console.ReadLine();
            }
        }
    }

    public class WebServiceHostBuilder<T>
    {
        public WebServiceHostBuilder(T instance)
        {
            Instance = instance;
            WebHostBuilder = new WebHostBuilder()
                .CaptureStartupErrors(true)
                .UseKestrel()
                .ConfigureServices(services => services.AddRouting())
                .Configure(Configure);
        }

        private T Instance { get; }

        private IWebHostBuilder WebHostBuilder { get; }

        public IWebHost Build() => WebHostBuilder.Build();

        public WebServiceHostBuilder<T> UseUrls(params string[] urls)
        {
            WebHostBuilder.UseUrls(urls);
            return this;
        }

        private static IEnumerable<RestMethodInfo> ToRestMethods(MethodInfo method) => method
            .GetCustomAttributes<HttpMethodAttribute>()
            .Select(attribute => new RestMethodInfo(method, attribute.HttpMethod, attribute.Path));

        private void Configure(IApplicationBuilder application)
        {
            var router = typeof(T).GetInterfaces()
                .SelectMany(TypeExtensions.GetMethods)
                .SelectMany(ToRestMethods)
                .Aggregate((IRouteBuilder)new RouteBuilder(application), MapMethod)
                .Build();

            application.UseRouter(router);
        }

        private IRouteBuilder MapMethod(IRouteBuilder routeBuilder, RestMethodInfo restMethodInfo)
        {
            var httpVerb = restMethodInfo.HttpMethod.Method;
            var template = restMethodInfo.GetTemplate();

            return routeBuilder.MapVerb(httpVerb, template, async context =>
            {
                var methodParameters = restMethodInfo.Parameters;
                var values = methodParameters.Select(parameter => parameter.Name).Select(context.GetRouteValue).ToArray();
                var result = restMethodInfo.Method.Invoke(Instance, values);
                context.Response.StatusCode = 200;
                if (result != null) await context.Response.WriteAsJsonAsync(result);
            });
        }

        private class RestMethodInfo
        {
            public RestMethodInfo(MethodInfo method, HttpMethod httpMethod, string template)
            {
                Method = method;
                HttpMethod = httpMethod;
                Template = template;
                Parameters = method.GetParameters();
            }

            public ParameterInfo[] Parameters { get; }

            public MethodInfo Method { get; }

            public HttpMethod HttpMethod { get; }

            public string Template { get; }

            private static string ToTemplateName(ParameterInfo parameter) => $"{{{parameter.Name}}}";

            public string GetTemplate()
            {
                if (!string.IsNullOrWhiteSpace(Template)) return Template;

                var templateParameters = Parameters.Select(ToTemplateName).Join("/");
                return $"{Method.Name}/{templateParameters}";
            }
        }
    }

    public interface IMyService
    {
        [Get]
        void WriteToConsole(string message);

        [Get]
        int Sum(int a, int b);
    }

    public class MyService : IMyService
    {
        public void WriteToConsole(string message)
        {
            Console.WriteLine(message);
        }

        public int Sum(int a, int b)
        {
            return a + b;
        }
    }
}
