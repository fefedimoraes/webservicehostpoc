using System.Net.Http;
using static System.Net.Http.HttpMethod;

namespace WebServiceHostPoc.Attributes
{
    /// <summary>
    /// An <see cref="HttpMethodAttribute"/> that encapsulates a <see cref="Get"/> method.
    /// </summary>
    public class GetAttribute : HttpMethodAttribute
    {
        /// <summary>
        /// Initializes a new instance of <see cref="GetAttribute"/>.
        /// </summary>
        public GetAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="GetAttribute"/> specifying a <paramref name="path"/>.
        /// </summary>
        /// <param name="path">The URL path of this <see cref="GetAttribute"/>.</param>
        public GetAttribute(string path) : base(path)
        {
        }

        /// <inheritdoc/>
        public override HttpMethod HttpMethod => Get;
    }
}