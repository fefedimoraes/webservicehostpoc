using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebServiceHostPoc.Integration.Tests
{
    public static class HttpContentExtensions
    {
        public static async Task<T> DeserializeAsJsonAsync<T>(this HttpContent httpContent)
        {
            using (var stream = await httpContent.ReadAsStreamAsync().ConfigureAwait(false))
            using (var streamReader = new StreamReader(stream))
            using (var jsonTextReader = new JsonTextReader(streamReader))
                return JsonSerializer.Create().Deserialize<T>(jsonTextReader);
        }
    }

    public static class HttpClientExtensions
    {
        public static Task<HttpResponseMessage> PutAsJsonAsync<T>(this HttpClient client, string requestUri, T content)
        {
            return client.PutAsync(requestUri, new JsonHttpContent<T>(content));
        }
    }
}