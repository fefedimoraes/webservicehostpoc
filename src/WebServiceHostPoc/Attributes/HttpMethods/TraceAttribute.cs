using System.Net.Http;

namespace WebServiceHostPoc.Attributes.HttpMethods
{
    /// <summary>
    /// An <see cref="HttpMethodAttribute"/> that encapsulates a <see cref="System.Net.Http.HttpMethod.Trace"/> method.
    /// </summary>
    public sealed class TraceAttribute : HttpMethodAttribute
    {
        /// <summary>
        /// Initializes a new instance of <see cref="TraceAttribute"/>.
        /// </summary>
        public TraceAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="TraceAttribute"/> specifying a <paramref name="path"/>.
        /// </summary>
        /// <param name="path">The URL path of this <see cref="TraceAttribute"/>.</param>
        public TraceAttribute(string path) : base(path)
        {
        }

        /// <inheritdoc/>
        public override HttpMethod HttpMethod => HttpMethod.Trace;
    }
}