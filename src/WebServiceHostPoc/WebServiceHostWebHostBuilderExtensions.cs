using Microsoft.AspNetCore.Hosting;

namespace WebServiceHostPoc
{
    /// <summary>
    /// Contains extension methods to <see cref="IWebHostBuilder"/> related to Web Service Host.
    /// </summary>
    public static class WebServiceHostWebHostBuilderExtensions
    {
        /// <summary>
        /// Configures the provided <paramref name="webHostBuilder"/>
        /// to use a Web Service Host for the provided <paramref name="instance"/>.
        /// </summary>
        /// <typeparam name="T">The type of the provided <paramref name="instance"/>.</typeparam>
        /// <param name="webHostBuilder">The <see cref="IWebHostBuilder"/> to configure.</param>
        /// <param name="instance">An instance to be HTTP exposed.</param>
        /// <returns>The provided <paramref name="webHostBuilder"/>.</returns>
        public static IWebHostBuilder UseWebServiceHost<T>(this IWebHostBuilder webHostBuilder, T instance)
        {
            webHostBuilder.ConfigureServices(services => services.AddWebServiceHost());
            webHostBuilder.Configure(builder => builder.UseWebServiceHost(instance));
            return webHostBuilder;
        }
    }
}