
using System.Web.Mvc;
using System.Web.Routing;

namespace WebAnime.API2
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                //url: "swagger/ui/index",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
