using AutoMapper;
using DataModels.Dto;
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
            kernel.Bind<AnimeDto>().To<AnimeDto>();
            kernel.Bind<AgeRatingDto>().To<AgeRatingDto>();
            kernel.Bind<CategoryDto>().To<CategoryDto>();
            kernel.Bind<CountryDto>().To<CountryDto>();
            kernel.Bind<EpisodeDto>().To<EpisodeDto>();
            kernel.Bind<ServerDto>().To<ServerDto>();
            kernel.Bind<StatusDto>().To<StatusDto>();
            kernel.Bind<StudioDto>().To<StudioDto>();
            kernel.Bind<TypeDto>().To<TypeDto>();
        }
    }
}