using System.Net.Http;

namespace WebServiceHostPoc.Attributes.HttpMethods
{
    /// <summary>
    /// An <see cref="HttpMethodAttribute"/> that encapsulates a <see cref="System.Net.Http.HttpMethod.Delete"/> method.
    /// </summary>
    public sealed class DeleteAttribute : HttpMethodAttribute
    {
        /// <summary>
        /// Initializes a new instance of <see cref="DeleteAttribute"/>.
        /// </summary>
        public DeleteAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="DeleteAttribute"/> specifying a <paramref name="template"/>.
        /// </summary>
        /// <param name="template">The URL Template of this <see cref="DeleteAttribute"/>.</param>
        public DeleteAttribute(string template) : base(template)
        {
        }

        /// <inheritdoc/>
        public override HttpMethod HttpMethod => HttpMethod.Delete;
    }
}