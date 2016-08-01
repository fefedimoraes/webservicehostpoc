using System;
using System.Net.Http;

namespace WebServiceHostPoc.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class GetAttribute : HttpMethodAttribute
    {
        public GetAttribute()
        {
        }

        public GetAttribute(string path) : base(path)
        {
        }

        public override HttpMethod HttpMethod => HttpMethod.Get;
    }
}