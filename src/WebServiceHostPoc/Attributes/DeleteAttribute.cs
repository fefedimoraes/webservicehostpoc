using System;
using System.Net.Http;

namespace WebServiceHostPoc.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DeleteAttribute : HttpMethodAttribute
    {
        public DeleteAttribute()
        {
        }

        public DeleteAttribute(string path) : base(path)
        {
        }

        public override HttpMethod HttpMethod => HttpMethod.Delete;
    }
}