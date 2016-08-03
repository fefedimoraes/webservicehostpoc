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
        /// Initializes a new instance of <see cref="GetAttribute"/> specifying a <paramref name="template"/>.
        /// </summary>
        /// <param name="template">The URL Template of this <see cref="GetAttribute"/>.</param>
        public GetAttribute(string template) : base(template)
        {
        }

        /// <inheritdoc/>
        public override HttpMethod HttpMethod => HttpMethod.Get;
    }
}