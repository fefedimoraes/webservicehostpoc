using System;
using System.Reflection;
using Microsoft.AspNetCore.Http;

namespace WebServiceHostPoc.Attributes.Parameters
{
    /// <summary>
    /// Base class for API Parameter Attributes.
    /// This <see cref="Attribute"/> can be used to decorate API parameters
    /// in order to specify where the parameter value is extracted from.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public abstract class ParameterAttribute : Attribute
    {
        public abstract bool TryGetValue(ParameterInfo parameterInfo, HttpContext httpContext, out object value);
    }
}