
using System.Web.Http;

namespace WebAnime.API2
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "SwaggerUI",
                routeTemplate: "swagger/ui/{*path}",
                defaults: new { controller = "Swagger" }
            );

            config.Routes.MapHttpRoute(
                name: "OldSwaggerUI",
                routeTemplate: "swagger/old/ui/{*path}",
                defaults: new { controller = "OldSwagger" }
            );

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
