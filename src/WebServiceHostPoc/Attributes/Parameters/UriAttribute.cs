using System;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace WebServiceHostPoc.Attributes.Parameters
{
    /// <summary>
    /// Values for parameters decorated with <see cref="UriAttribute"/> are retrieved from the resource URI path.
    /// </summary>
    public sealed class UriAttribute : ParameterAttribute
    {
        /// <inheritdoc/>
        public override bool TryGetValue(ParameterInfo parameterInfo, HttpContext httpContext, out object value)
        {
            return (value = httpContext.GetRouteValue(parameterInfo.Name)) != null;
        }
    }
}