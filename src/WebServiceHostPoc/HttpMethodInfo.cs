using System;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using WebServiceHostPoc.Extensions;

namespace WebServiceHostPoc
{
    /// <summary>
    /// Encapsulates an <see cref="HttpMethod"/> and a <see cref="MethodInfo"/>, making the latter HTTP accessible.
    /// </summary>
    public class HttpMethodInfo
    {
        /// <summary>
        /// Initializes a new instance of <see cref="HttpMethodInfo"/>,
        /// using the provided <paramref name="template"/>,
        /// <paramref name="httpMethod"/> and <paramref name="methodInfo"/>.
        /// </summary>
        /// <param name="template">
        /// The URL path that will serve as a route template to the <paramref name="methodInfo"/>.
        /// </param>
        /// <param name="httpMethod">
        /// The <see cref="HttpMethod"/> which the <paramref name="methodInfo"/> will be accessible from.
        /// </param>
        /// <param name="methodInfo">
        /// The <see cref="MethodInfo"/> to be HTTP accessible.
        /// </param>
        public HttpMethodInfo(string template, HttpMethod httpMethod, MethodInfo methodInfo)
        {
            ExplicitTemplate = template;
            HttpMethod = httpMethod;
            MethodInfo = methodInfo;
            Parameters = methodInfo.GetParameters();
        }

        /// <summary>
        /// Gets the URL route template of this <see cref="HttpMethodInfo"/>.
        /// </summary>
        public string Template => string.IsNullOrWhiteSpace(ExplicitTemplate) ? TemplateFromMethodInfo : ExplicitTemplate;

        /// <summary>
        /// Gets the <see cref="HttpMethod"/> of this <see cref="HttpMethodInfo"/>.
        /// </summary>
        public HttpMethod HttpMethod { get; }

        /// <summary>
        /// Gets the configured URL path of this <see cref="HttpMethodInfo"/>.
        /// </summary>
        private string ExplicitTemplate { get; }

        /// <summary>
        /// Gets the route template from the <see cref="MethodInfo"/>.
        /// </summary>
        private string TemplateFromMethodInfo => $"{MethodInfo.Name}/{Parameters.Select(ToParameterTemplateName).Join("/")}";

        /// <summary>
        /// Gets the <see cref="MethodInfo"/> of this <see cref="HttpMethodInfo"/>.
        /// </summary>
        private MethodInfo MethodInfo { get; }

        /// <summary>
        /// Gets the array of <see cref="ParameterInfo"/> from the <see cref="MethodInfo"/>.
        /// </summary>
        private ParameterInfo[] Parameters { get; }

        /// <summary>
        /// Invokes the method represented by this <see cref="HttpMethodInfo"/>
        /// using the specified <paramref name="context"/>.
        /// </summary>
        /// <param name="instance">The <see cref="object"/> on which to invoke the method.</param>
        /// <param name="context">The <see cref="HttpContext"/> from which the parameters will be extracted from.</param>
        /// <returns>A <see cref="Task"/> that represents the completion of request processing.</returns>
        public Task Invoke(object instance, HttpContext context)
        {
            var parameters = Parameters.Select(parameter =>
            {
                var routeValue = context.GetRouteValue(parameter.Name);
                return Convert.ChangeType(routeValue, parameter.ParameterType);
            }).ToArray();

            try
            {
                var result = MethodInfo.Invoke(instance, parameters);
                if (result != null) return context.Response.WriteAsJsonAsync(result);

                context.Response.StatusCode = 204;
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                context.Response.StatusCode = 500;
                return context.Response.WriteAsJsonAsync(e);
            }
        }

        /// <summary>
        /// Converts the provided <paramref name="parameterInfo"/> to its route template definition.
        /// </summary>
        /// <param name="parameterInfo">The <see cref="ParameterInfo"/> to be converted.</param>
        /// <returns>
        /// A <see cref="string"/> that represents the provided <paramref name="parameterInfo"/> in a route template.
        /// </returns>
        private static string ToParameterTemplateName(ParameterInfo parameterInfo)
        {
            var name = parameterInfo.Name;
            return $"{{{name}}}";
        }
    }
}