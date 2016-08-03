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
        /// Initializes a new instance of <see cref="OptionsAttribute"/> specifying a <paramref name="template"/>.
        /// </summary>
        /// <param name="template">The URL Template of this <see cref="OptionsAttribute"/>.</param>
        public OptionsAttribute(string template) : base(template)
        {
        }

        /// <inheritdoc/>
        public override HttpMethod HttpMethod => HttpMethod.Options;
    }
}