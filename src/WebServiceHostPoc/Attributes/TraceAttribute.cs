using System;
using System.Net.Http;

namespace WebServiceHostPoc.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class TraceAttribute : HttpMethodAttribute
    {
        public TraceAttribute()
        {
        }

        public TraceAttribute(string path) : base(path)
        {
        }

        public override HttpMethod HttpMethod => HttpMethod.Trace;
    }
}