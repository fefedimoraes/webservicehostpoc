using System;
using System.Net.Http;

namespace WebServiceHostPoc.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class OptionsAttribute : HttpMethodAttribute
    {
        public OptionsAttribute()
        {
        }

        public OptionsAttribute(string path) : base(path)
        {
        }

        public override HttpMethod HttpMethod => HttpMethod.Options;
    }
}