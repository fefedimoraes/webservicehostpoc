using System;
using Microsoft.AspNetCore.Hosting;
using WebServiceHostPoc.Attributes;

namespace WebServiceHostPoc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webHost = new WebHostBuilder()
                .CaptureStartupErrors(true)
                .UseKestrel()
                .UseWebServiceHost(new MyService())
                .UseUrls("http://127.0.0.1:8080/myservice")
                .Build();

            using (webHost)
            {
                webHost.Start();
                Console.WriteLine("Running!");
                Console.ReadLine();
            }
        }

        private interface IMyService
        {
            [Get]
            void WriteToConsole(string message);

            [Get]
            int Sum(int a, int b);
        }

        private class MyService : IMyService
        {
            public void WriteToConsole(string message) => Console.WriteLine(message);

            public int Sum(int a, int b) => a + b;
        }
    }
}
