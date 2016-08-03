using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using WebServiceHostPoc.Attributes.HttpMethods;

namespace WebServiceHostPoc
{
    /// <summary>
    /// <see cref="IRouter"/> builder based on a <typeparamref name="T"/> service.
    /// </summary>
    /// <typeparam name="T">The service type from whose the routes will be built.</typeparam>
    public class WebServiceHostRouteBuilder<T>
    {
        /// <summary>
        /// Initializes an instance of <see cref="WebServiceHostRouteBuilder{T}"/>
        /// for the provided <paramref name="instance"/>.
        /// </summary>
        /// <param name="instance">The service instance from whose the routes will be built.</param>
        public WebServiceHostRouteBuilder(T instance)
        {
            Instance = instance;
        }

        /// <summary>
        /// Gets the service instance of this <see cref="WebServiceHostRouteBuilder{T}"/>.
        /// </summary>
        private T Instance { get; }

        /// <summary>
        /// Builds an <see cref="IRouter"/> that routes to the methods of the provided service.
        /// </summary>
        /// <param name="builder">
        /// The instance of <see cref="IApplicationBuilder"/>
        /// which this <see cref="WebServiceHostRouteBuilder{T}"/> will build to.
        /// </param>
        /// <returns>The resulting <see cref="IRouter"/>.</returns>
        public IRouter Build(IApplicationBuilder builder) => typeof(T)
            .GetInterfaces()
            .SelectMany(TypeExtensions.GetMethods)
            .SelectMany(ToHttpMethods)
            .Aggregate(new RouteBuilder(builder), AddHttpMethod)
            .Build();

        /// <summary>
        /// Maps the provided <paramref name="method"/>
        /// to a sequence of <see cref="HttpMethodInfo"/>s.
        /// </summary>
        /// <param name="method">An instance of <see cref="MethodInfo"/> to be converted.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="HttpMethodInfo"/>s.</returns>
        private static IEnumerable<HttpMethodInfo> ToHttpMethods(MethodInfo method) => method
            .GetCustomAttributes<HttpMethodAttribute>()
            .Select(attribute => new HttpMethodInfo(attribute.HttpMethod, method, attribute.Path));

        /// <summary>
        /// Maps the provided <paramref name="method"/>
        /// into the provided <paramref name="routeBuilder"/>.
        /// </summary>
        /// <param name="routeBuilder">
        /// The instance of <see cref="RouteBuilder"/> which the provided <paramref name="method"/> will be appended to.
        /// </param>
        /// <param name="method">
        /// The instance of <see cref="HttpMethodInfo"/> to be appended.
        /// </param>
        /// <returns>The provided <paramref name="routeBuilder"/>.</returns>
        private RouteBuilder AddHttpMethod(RouteBuilder routeBuilder, HttpMethodInfo method)
        {
            routeBuilder.MapVerb(method.HttpMethod.Method, method.Template, context => method.Invoke(Instance, context));
            return routeBuilder;
        }
    }
}