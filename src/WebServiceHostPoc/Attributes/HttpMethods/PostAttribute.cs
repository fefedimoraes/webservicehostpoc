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
        /// Initializes a new instance of <see cref="PostAttribute"/> specifying a <paramref name="template"/>.
        /// </summary>
        /// <param name="template">The URL Template of this <see cref="PostAttribute"/>.</param>
        public PostAttribute(string template) : base(template)
        {
        }

        /// <inheritdoc/>
        public override HttpMethod HttpMethod => HttpMethod.Post;
    }
}