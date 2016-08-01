using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace WebServiceHostPoc.Extensions
{
    /// <summary>
    /// Contains extension methods to <see cref="HttpResponse"/>.
    /// </summary>
    public static class HttpResponseExtensions
    {
        /// <summary>
        /// Writes the provided <paramref name="object"/> as a JSON object to the provided <paramref name="response"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the provided <paramref name="object"/>.
        /// </typeparam>
        /// <param name="response">
        /// The <see cref="HttpResponse"/> to write the <paramref name="object"/> to.
        /// </param>
        /// <param name="object">
        /// An instance of <typeparamref name="T"/> to be written to the <paramref name="response"/>.
        /// </param>
        /// <returns>A <see cref="Task"/> that completes after flushing the <paramref name="response"/>.</returns>
        public static async Task WriteAsJsonAsync<T>(this HttpResponse response, T @object)
        {
            using (var writer = new StreamWriter(response.Body))
            {
                var serializer = JsonSerializer.CreateDefault();
                serializer.Serialize(writer, @object);
                await writer.FlushAsync().ConfigureAwait(false);
            }
        }
    }
}