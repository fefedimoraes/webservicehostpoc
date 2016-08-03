using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace WebServiceHostPoc.Attributes.Parameters
{
    /// <summary>
    /// Values for parameters decorated with <see cref="QueryStringAttribute"/> are retrieved from the resource URI path.
    /// </summary>
    public sealed class QueryStringAttribute : ParameterAttribute
    {
        /// <inheritdoc/>
        public override bool TryGetValue(ParameterInfo parameterInfo, HttpContext httpContext, out object value)
        {
            value = null;
            StringValues values;
            if (!httpContext.Request.Query.TryGetValue(parameterInfo.Name, out values)) return false;
            value = values.LastOrDefault();
            return true;
        }
    }
}