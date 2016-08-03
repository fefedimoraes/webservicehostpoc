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
        /// Initializes a new instance of <see cref="TraceAttribute"/> specifying a <paramref name="template"/>.
        /// </summary>
        /// <param name="template">The URL Template of this <see cref="TraceAttribute"/>.</param>
        public TraceAttribute(string template) : base(template)
        {
        }

        /// <inheritdoc/>
        public override HttpMethod HttpMethod => HttpMethod.Trace;
    }
}