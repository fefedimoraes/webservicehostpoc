using System.Net.Http;

namespace WebServiceHostPoc.Attributes.HttpMethods
{
    /// <summary>
    /// An <see cref="HttpMethodAttribute"/> that encapsulates a <see cref="System.Net.Http.HttpMethod.Post"/> method.
    /// </summary>
    public sealed class PostAttribute : HttpMethodAttribute
    {
        /// <summary>
        /// Initializes a new instance of <see cref="PostAttribute"/>.
        /// </summary>
        public PostAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="PostAttribute"/> specifying a <paramref name="path"/>.
        /// </summary>
        /// <param name="path">The URL path of this <see cref="PostAttribute"/>.</param>
        public PostAttribute(string path) : base(path)
        {
        }

        /// <inheritdoc/>
        public override HttpMethod HttpMethod => HttpMethod.Post;
    }
}