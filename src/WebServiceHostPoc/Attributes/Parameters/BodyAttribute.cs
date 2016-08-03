using System;
using System.Reflection;
using Microsoft.AspNetCore.Http;

namespace WebServiceHostPoc.Attributes.Parameters
{
    /// <summary>
    /// Values for parameters decorated with <see cref="BodyAttribute"/> are retrieved from the resource URI path.
    /// </summary>
    public sealed class BodyAttribute : ParameterAttribute
    {
        /// <inheritdoc/>
        public override bool TryGetValue(ParameterInfo parameterInfo, HttpContext httpContext, out object value)
        {
            throw new NotImplementedException();
        }
    }
}