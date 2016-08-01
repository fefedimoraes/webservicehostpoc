using System;
using System.Net.Http;

namespace WebServiceHostPoc.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class PutAttribute : HttpMethodAttribute
    {
        public PutAttribute()
        {
        }

        public PutAttribute(string path) : base(path)
        {
        }

        public override HttpMethod HttpMethod => HttpMethod.Put;
    }
}