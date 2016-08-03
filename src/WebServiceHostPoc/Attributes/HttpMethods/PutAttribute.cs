using System.Net.Http;

namespace WebServiceHostPoc.Attributes.HttpMethods
{
    /// <summary>
    /// An <see cref="HttpMethodAttribute"/> that encapsulates a <see cref="System.Net.Http.HttpMethod.Put"/> method.
    /// </summary>
    public sealed class PutAttribute : HttpMethodAttribute
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
        public override HttpMethod HttpMethod => HttpMethod.Put;
    }
}