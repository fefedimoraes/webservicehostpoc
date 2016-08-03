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
        /// Initializes a new instance of <see cref="DeleteAttribute"/> specifying a <paramref name="path"/>.
        /// </summary>
        /// <param name="path">The URL path of this <see cref="DeleteAttribute"/>.</param>
        public DeleteAttribute(string path) : base(path)
        {
        }

        /// <inheritdoc/>
        public override HttpMethod HttpMethod => HttpMethod.Delete;
    }
}