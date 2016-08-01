using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace WebServiceHostPoc.Extensions
{
    public static class HttpResponseExtensions
    {
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