using System.Linq;
using System.Reflection;
using WebServiceHostPoc.Attributes.Parameters;

namespace WebServiceHostPoc.Extensions
{
    /// <summary>
    /// Contains extension methods to <see cref="ParameterInfo"/>.
    /// </summary>
    public static class ParameterInfoExtensions
    {
        /// <summary>
        /// Checks whether the provided <paramref name="parameterInfo"/> is part of the route template.
        /// </summary>
        /// <param name="parameterInfo">An instance of <see cref="ParameterInfo"/> to be checked.</param>
        /// <returns>
        /// <c>true</c> if the provided <paramref name="parameterInfo"/>
        /// is part of the route template; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsRouteTemplateParameter(this ParameterInfo parameterInfo) => parameterInfo
            .GetCustomAttributes<UriAttribute>()
            .Any();

        /// <summary>
        /// Converts the provided <paramref name="parameterInfo"/> to its route template definition.
        /// </summary>
        /// <param name="parameterInfo">The <see cref="ParameterInfo"/> to be converted.</param>
        /// <returns>
        /// A <see cref="string"/> that represents the provided <paramref name="parameterInfo"/> in a route template.
        /// </returns>
        public static string ToRouteTemplateParameterName(this ParameterInfo parameterInfo) => $"{{{parameterInfo.Name}}}";
    }
}