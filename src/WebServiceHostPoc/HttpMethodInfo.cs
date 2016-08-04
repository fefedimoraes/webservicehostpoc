using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebServiceHostPoc.Attributes.Parameters;
using WebServiceHostPoc.Extensions;

namespace WebServiceHostPoc
{
    /// <summary>
    /// Encapsulates an <see cref="HttpMethod"/> and a <see cref="MethodInfo"/>, making the latter HTTP accessible.
    /// </summary>
    public class HttpMethodInfo
    {
        private static readonly ParameterAttribute DefaultParameterAttribute = new QueryStringAttribute();

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
        public string Template => string.IsNullOrWhiteSpace(ExplicitTemplate) ? ComputedTemplate : ExplicitTemplate;

        /// <summary>
        /// Gets the <see cref="HttpMethod"/> of this <see cref="HttpMethodInfo"/>.
        /// </summary>
        public HttpMethod HttpMethod { get; }

        /// <summary>
        /// Gets the configured URL path of this <see cref="HttpMethodInfo"/>.
        /// </summary>
        private string ExplicitTemplate { get; }

        /// <summary>
        /// Gets the <see cref="MethodInfo"/> of this <see cref="HttpMethodInfo"/>.
        /// </summary>
        private MethodInfo MethodInfo { get; }

        /// <summary>
        /// Gets the array of <see cref="ParameterInfo"/> from the <see cref="MethodInfo"/>.
        /// </summary>
        private ParameterInfo[] Parameters { get; }

        /// <summary>
        /// Gets the concatenated <see cref="string"/> that contains all parameters that are part of the template route.
        /// </summary>
        private string RouteTemplateParameters => Parameters
            .Where(ParameterInfoExtensions.IsRouteTemplateParameter)
            .Select(ParameterInfoExtensions.ToRouteTemplateParameterName)
            .Join("/");

        /// <summary>
        /// Gets the route template from the <see cref="MethodInfo"/>.
        /// </summary>
        private string ComputedTemplate => $"{MethodInfo.Name}/{RouteTemplateParameters}";

        /// <summary>
        /// Invokes the method represented by this <see cref="HttpMethodInfo"/>
        /// using the specified <paramref name="context"/>.
        /// </summary>
        /// <param name="instance">The <see cref="object"/> on which to invoke the method.</param>
        /// <param name="context">The <see cref="HttpContext"/> from which the parameters will be extracted from.</param>
        /// <returns>A <see cref="Task"/> that represents the completion of request processing.</returns>
        public Task Invoke(object instance, HttpContext context)
        {
            try
            {
                var parameters = GetParameterValues(context).ToArray();

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

        private IEnumerable<object> GetParameterValues(HttpContext context)
        {
            foreach (var parameter in Parameters)
            {
                object value;
                var attribute = parameter.GetCustomAttribute<ParameterAttribute>() ?? DefaultParameterAttribute;

                if (attribute.TryGetValue(parameter, context, out value))
                {
                    yield return Convert.ChangeType(value, parameter.ParameterType);
                }
                else if (parameter.HasDefaultValue)
                {
                    yield return parameter.DefaultValue;
                }
                else
                {
                    throw new ArgumentException(parameter.Name);
                }
            }
        }
    }
}