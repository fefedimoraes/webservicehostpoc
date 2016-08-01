using System;
using System.Net.Http;

namespace WebServiceHostPoc.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class PostAttribute : HttpMethodAttribute
    {
        public PostAttribute()
        {
        }

        public PostAttribute(string path) : base(path)
        {
        }

        public override HttpMethod HttpMethod => HttpMethod.Post;
    }
}