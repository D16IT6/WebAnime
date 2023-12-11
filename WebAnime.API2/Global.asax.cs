
using Ninject;
using Ninject.Web.Common.WebHost;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebAnime.API2
{
    public class WebApiApplication : NinjectHttpApplication
    {
        protected override void OnApplicationStarted()
        {
            AreaRegistration.RegisterAllAreas();
           GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected override IKernel CreateKernel() => NinjectConfig.Kernel;
    }
}
