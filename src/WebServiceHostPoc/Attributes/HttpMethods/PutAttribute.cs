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
        /// Initializes a new instance of <see cref="PutAttribute"/> specifying a <paramref name="template"/>.
        /// </summary>
        /// <param name="template">The URL Template of this <see cref="PutAttribute"/>.</param>
        public PutAttribute(string template) : base(template)
        {
        }

        /// <inheritdoc/>
        public override HttpMethod HttpMethod => HttpMethod.Put;
    }
}