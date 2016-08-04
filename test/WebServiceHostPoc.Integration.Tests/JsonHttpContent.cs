using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebServiceHostPoc.Integration.Tests
{
    public class JsonHttpContent<T> : HttpContent
    {
        public JsonHttpContent(T content)
        {
            Content = content;
        }

        public T Content { get; }

        protected override async Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            using (var streamWriter = new StreamWriter(stream))
            {
                JsonSerializer.Create().Serialize(streamWriter, Content, typeof(T));
                await streamWriter.FlushAsync().ConfigureAwait(false);
            }
        }

        protected override bool TryComputeLength(out long length)
        {
            length = -1;
            return false;
        }
    }
}