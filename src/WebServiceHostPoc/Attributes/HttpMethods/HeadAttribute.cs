using System.Net.Http;

namespace WebServiceHostPoc.Attributes.HttpMethods
{
    /// <summary>
    /// An <see cref="HttpMethodAttribute"/> that encapsulates a <see cref="System.Net.Http.HttpMethod.Head"/> method.
    /// </summary>
    public sealed class HeadAttribute : HttpMethodAttribute
    {
        /// <summary>
        /// Initializes a new instance of <see cref="HeadAttribute"/>.
        /// </summary>
        public HeadAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="HeadAttribute"/> specifying a <paramref name="path"/>.
        /// </summary>
        /// <param name="path">The URL path of this <see cref="HeadAttribute"/>.</param>
        public HeadAttribute(string path) : base(path)
        {
        }

        /// <inheritdoc/>
        public override HttpMethod HttpMethod => HttpMethod.Head;
    }
}