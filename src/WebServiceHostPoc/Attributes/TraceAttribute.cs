using System.Net.Http;
using static System.Net.Http.HttpMethod;

namespace WebServiceHostPoc.Attributes
{
    /// <summary>
    /// An <see cref="HttpMethodAttribute"/> that encapsulates a <see cref="Trace"/> method.
    /// </summary>
    public class TraceAttribute : HttpMethodAttribute
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
        public override HttpMethod HttpMethod => Trace;
    }
}