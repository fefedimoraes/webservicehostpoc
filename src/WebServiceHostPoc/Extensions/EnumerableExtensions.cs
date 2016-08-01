using System.Collections.Generic;

namespace WebServiceHostPoc.Extensions
{
    public static class EnumerableExtensions
    {
        public static string Join<T>(this IEnumerable<T> enumerable, string separator) => string.Join(separator, enumerable);
    }
}