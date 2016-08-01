using Microsoft.Extensions.DependencyInjection;

namespace WebServiceHostPoc
{
    /// <summary>
    /// Contains extension methods to <see cref="IServiceCollection"/> related to Web Service Host.
    /// </summary>
    public static class WebServiceHostServiceCollectionExtensions
    {
        /// <summary>
        /// Adds services required for web hosting a service.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <returns>The provided <paramref name="services"/>.</returns>
        public static IServiceCollection AddWebServiceHost(this IServiceCollection services)
        {
            services.AddRouting();
            return services;
        }
    }
}