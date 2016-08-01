using System.Net.Http;
using static System.Net.Http.HttpMethod;

namespace WebServiceHostPoc.Attributes
{
    /// <summary>
    /// An <see cref="HttpMethodAttribute"/> that encapsulates a <see cref="Put"/> method.
    /// </summary>
    public class PutAttribute : HttpMethodAttribute
    {
        /// <summary>
        /// Initializes a new instance of <see cref="PutAttribute"/>.
        /// </summary>
        public PutAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="PutAttribute"/> specifying a <paramref name="path"/>.
        /// </summary>
        /// <param name="path">The URL path of this <see cref="PutAttribute"/>.</param>
        public PutAttribute(string path) : base(path)
        {
        }

        /// <inheritdoc/>
        public override HttpMethod HttpMethod => Put;
    }
}