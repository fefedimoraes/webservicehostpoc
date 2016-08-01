using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using WebServiceHostPoc.Attributes;
using WebServiceHostPoc.Extensions;

namespace WebServiceHostPoc
{
    /// <summary>
    /// Contains extension methods to <see cref="IApplicationBuilder"/> related to Web Service Host.
    /// </summary>
    public static class WebServiceHostBuilderExtensions
    {
        /// <summary>
        /// Adds Web Service Host for the provided <paramref name="instance"/>
        /// to the <see cref="builder"/> request execution pipeline.
        /// </summary>
        /// <typeparam name="T">The type of the provided <paramref name="instance"/>.</typeparam>
        /// <param name="builder">The <see cref="IApplicationBuilder"/> to add the web service host to.</param>
        /// <param name="instance">The instance of the service to be HTTP exposed.</param>
        /// <returns>The provided <paramref name="builder"/>.</returns>
        public static IApplicationBuilder UseWebServiceHost<T>(this IApplicationBuilder builder, T instance)
        {
            var router = typeof(T).GetInterfaces()
                .SelectMany(TypeExtensions.GetMethods)
                .SelectMany(ToRestMethods)
                .Aggregate((IRouteBuilder)new RouteBuilder(builder), (routeBuilder, info) => MapMethod(routeBuilder, info, instance))
                .Build();
            builder.UseRouter(router);
            return builder;
        }

        private static IEnumerable<RestMethodInfo> ToRestMethods(MethodInfo method) => method
            .GetCustomAttributes<HttpMethodAttribute>()
            .Select(attribute => new RestMethodInfo(method, attribute.HttpMethod, attribute.Path));

        private static IRouteBuilder MapMethod<T>(IRouteBuilder routeBuilder, RestMethodInfo restMethodInfo, T instance)
        {
            var httpVerb = restMethodInfo.HttpMethod.Method;
            var template = restMethodInfo.GetTemplate();

            return routeBuilder.MapVerb(httpVerb, template, async context =>
            {
                var methodParameters = restMethodInfo.Parameters;
                var values = methodParameters.Select(parameter => parameter.Name)
                    .Select(context.GetRouteValue)
                    .ToArray();
                try
                {
                    var result = restMethodInfo.Method.Invoke(instance, values);
                    context.Response.StatusCode = 200;
                    if (result != null) await context.Response.WriteAsJsonAsync(result);
                }
                catch (Exception e)
                {
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsJsonAsync(e);
                }
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
}