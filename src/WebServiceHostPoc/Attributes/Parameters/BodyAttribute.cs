using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

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
            using (var streamReader = new StreamReader(httpContext.Request.Body))
            using (var jsonTextReader = new JsonTextReader(streamReader))
            {
                var serializer = JsonSerializer.Create();
                value = serializer.Deserialize(jsonTextReader, parameterInfo.ParameterType);
                return true;
            }
        }
    }
}