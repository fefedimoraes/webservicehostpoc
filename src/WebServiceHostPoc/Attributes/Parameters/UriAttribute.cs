using System;

namespace WebServiceHostPoc.Attributes.Parameters
{
    /// <summary>
    /// Values for parameters decorated with <see cref="UriAttribute"/> are retrieved from the resource URI path.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class UriAttribute : Attribute
    {
    }
}