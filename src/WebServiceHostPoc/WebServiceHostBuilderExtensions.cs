using Microsoft.AspNetCore.Builder;

namespace WebServiceHostPoc
{
    /// <summary>
    /// Contains extension methods to <see cref="IApplicationBuilder"/> related to Web Service Host.
    /// </summary>
    public static class WebServiceHostBuilderExtensions
    {
        /// <summary>
        /// Adds Web Service Host for the provided <paramref name="instance"/>
        /// to the <see cref="builder"/> request execution pipeline.
        /// </summary>
        /// <typeparam name="T">The type of the provided <paramref name="instance"/>.</typeparam>
        /// <param name="builder">The <see cref="IApplicationBuilder"/> to add the web service host to.</param>
        /// <param name="instance">The instance of the service to be HTTP exposed.</param>
        /// <returns>The provided <paramref name="builder"/>.</returns>
        public static IApplicationBuilder UseWebServiceHost<T>(this IApplicationBuilder builder, T instance)
        {
            var router = new WebServiceHostRouteBuilder<T>(instance).Build(builder);
            builder.UseRouter(router);
            return builder;
        }
    }
}