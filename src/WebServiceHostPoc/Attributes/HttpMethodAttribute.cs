using System;
using System.Net.Http;

namespace WebServiceHostPoc.Attributes
{
    public abstract class HttpMethodAttribute : Attribute
    {
        protected HttpMethodAttribute()
        {
        }

        protected HttpMethodAttribute(string path)
        {
            Path = path;
        }

        public abstract HttpMethod HttpMethod { get; }

        public string Path { get; }


    }
}