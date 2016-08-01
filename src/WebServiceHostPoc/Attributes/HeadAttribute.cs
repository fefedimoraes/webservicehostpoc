using System.Net.Http;
using static System.Net.Http.HttpMethod;

namespace WebServiceHostPoc.Attributes
{
    /// <summary>
    /// An <see cref="HttpMethodAttribute"/> that encapsulates a <see cref="Head"/> method.
    /// </summary>
    public class HeadAttribute : HttpMethodAttribute
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
        public override HttpMethod HttpMethod => Head;
    }
}