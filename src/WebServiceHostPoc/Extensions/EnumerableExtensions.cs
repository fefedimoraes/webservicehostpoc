using System.Collections.Generic;

namespace WebServiceHostPoc.Extensions
{
    /// <summary>
    /// Contains extension methods to <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Concatenates the members of the provided <paramref name="enumerable"/>,
        /// using the specified <paramref name="separator"/> between each member.
        /// </summary>
        /// <typeparam name="T">The type of the members of the provided <paramref name="enumerable"/>.</typeparam>
        /// <param name="enumerable">
        /// A <see cref="IEnumerable{T}">sequence</see> that contains the objects to concatenate.
        /// </param>
        /// <param name="separator">
        /// The <see cref="string"/> to use as a separator.
        /// It is included in the returned string only if the provided
        /// <paramref name="enumerable"/> has more than one element.
        /// </param>
        /// <returns>
        /// A <see cref="string"/> that consists of the members of <paramref name="enumerable"/>
        /// delimited by the <paramref name="separator"/> string.
        /// If the provided <paramref name="enumerable"/> has no members,
        /// this method returns <see cref="string.Empty"/>.
        /// </returns>
        public static string Join<T>(this IEnumerable<T> enumerable, string separator) => string.Join(separator, enumerable);
    }
}