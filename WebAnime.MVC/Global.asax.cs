using Ninject;
using Ninject.Web.Common.WebHost;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebAnime.MVC
{
    public class MvcApplication : NinjectHttpApplication
    {
        protected override void OnApplicationStarted()//Application_Start
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }


        protected override IKernel CreateKernel() => NinjectConfig.Kernel;//DI Container
    }
}