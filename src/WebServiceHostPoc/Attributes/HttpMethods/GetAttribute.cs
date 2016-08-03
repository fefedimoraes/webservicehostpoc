using System.Net.Http;

namespace WebServiceHostPoc.Attributes.HttpMethods
{
    /// <summary>
    /// An <see cref="HttpMethodAttribute"/> that encapsulates a <see cref="System.Net.Http.HttpMethod.Get"/> method.
    /// </summary>
    public sealed class GetAttribute : HttpMethodAttribute
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
        public override HttpMethod HttpMethod => HttpMethod.Get;
    }
}