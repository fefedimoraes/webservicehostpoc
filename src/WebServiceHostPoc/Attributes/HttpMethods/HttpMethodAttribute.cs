using System;
using System.Net.Http;

namespace WebServiceHostPoc.Attributes.HttpMethods
{
    /// <summary>
    /// Base class for HTTP Methods Attributes.
    /// This <see cref="Attribute"/> can be used to decorate methods in order to expose them through HTTP.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class HttpMethodAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of <see cref="HttpMethodAttribute"/>.
        /// </summary>
        protected HttpMethodAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="HttpMethodAttribute"/>
        /// with the provided <paramref name="path"/>.
        /// </summary>
        /// <param name="path">The URL path of this <see cref="HttpMethodAttribute"/>.</param>
        protected HttpMethodAttribute(string path)
        {
            Path = path;
        }

        /// <summary>
        /// Gets the respective <see cref="HttpMethod"/> of this <see cref="HttpMethodAttribute"/>.
        /// </summary>
        public abstract HttpMethod HttpMethod { get; }

        /// <summary>
        /// Gets the URL path of this <see cref="HttpMethodAttribute"/>.
        /// </summary>
        public string Path { get; }
    }
}