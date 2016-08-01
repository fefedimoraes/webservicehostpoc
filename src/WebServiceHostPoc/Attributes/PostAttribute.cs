using System.Net.Http;
using static System.Net.Http.HttpMethod;

namespace WebServiceHostPoc.Attributes
{
    /// <summary>
    /// An <see cref="HttpMethodAttribute"/> that encapsulates a <see cref="Post"/> method.
    /// </summary>
    public class PostAttribute : HttpMethodAttribute
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
        public override HttpMethod HttpMethod => Post;
    }
}