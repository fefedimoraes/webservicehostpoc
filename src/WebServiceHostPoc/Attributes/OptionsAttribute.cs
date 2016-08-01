using System.Net.Http;
using static System.Net.Http.HttpMethod;

namespace WebServiceHostPoc.Attributes
{
    /// <summary>
    /// An <see cref="HttpMethodAttribute"/> that encapsulates an <see cref="Options"/> method.
    /// </summary>
    public class OptionsAttribute : HttpMethodAttribute
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
        public override HttpMethod HttpMethod => Options;
    }
}