using System.Net.Http;

namespace WebServiceHostPoc.Attributes.HttpMethods
{
    /// <summary>
    /// An <see cref="HttpMethodAttribute"/> that encapsulates an <see cref="System.Net.Http.HttpMethod.Options"/> method.
    /// </summary>
    public sealed class OptionsAttribute : HttpMethodAttribute
    {
        /// <summary>
        /// Initializes a new instance of <see cref="OptionsAttribute"/>.
        /// </summary>
        public OptionsAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="OptionsAttribute"/> specifying a <paramref name="path"/>.
        /// </summary>
        /// <param name="path">The URL path of this <see cref="OptionsAttribute"/>.</param>
        public OptionsAttribute(string path) : base(path)
        {
        }

        /// <inheritdoc/>
        public override HttpMethod HttpMethod => HttpMethod.Options;
    }
}