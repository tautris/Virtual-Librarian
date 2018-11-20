using System.Net.Http.Headers;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using VirtualLibrarian.API.Core;

namespace VirtualLibrarian.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //Json config
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling
                = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            config.Formatters.JsonFormatter.SupportedMediaTypes
               .Add(new MediaTypeHeaderValue("text/html"));

            //Unity container for DI
            var container = new UnityContainer();
            container.RegisterType<ILibrary, Library>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);
        }
    }
}
