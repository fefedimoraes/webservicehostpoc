using System;
using System.Net.Http;

namespace WebServiceHostPoc.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class HeadAttribute : HttpMethodAttribute
    {
        public HeadAttribute()
        {
        }

        public HeadAttribute(string path) : base(path)
        {
        }

        public override HttpMethod HttpMethod => HttpMethod.Head;
    }
}