using AutoMapper;
using Ninject;
using Ninject.Web.Common.WebHost;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;
using WebAnime.MVC.Resources.Library.Admin.Mapping;

namespace WebAnime.MVC
{
    public class MvcApplication : NinjectHttpApplication
    {
        protected override void OnApplicationStarted()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            RegisterServices(kernel);
            return kernel;
        }
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IMapper>().ToConstant(AutoMapperConfiguration.Configure());
        }
    }
}