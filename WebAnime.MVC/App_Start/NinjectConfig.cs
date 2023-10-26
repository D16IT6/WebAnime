using AutoMapper;
using DataModels.EF;
using DataModels.EF.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Ninject;
using Ninject.Web.Common;
using System;
using System.Reflection;
using System.Web;

namespace WebAnime.MVC
{
    public class NinjectConfig
    {
        private static bool _cannotGet = false;
        private static IKernel _kernel;
        public static IKernel Kernel
        {
            get
            {
                if (_cannotGet)
                {
                    throw new NotSupportedException("Cannot get kernel!");
                }
                if (_kernel == null)
                {
                    _kernel = new StandardKernel();
                    _kernel.Load(Assembly.GetExecutingAssembly());
                    RegisterServices(_kernel);
                    _cannotGet = true;
                }
                return _kernel;
            }
            set => _kernel = value;
        }
        public static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IAuthenticationManager>().ToMethod(
                ninjectContext =>
                HttpContext.Current.GetOwinContext().Authentication
                );
            kernel.Bind<WebAnimeDbContext>().ToSelf();
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            kernel.Bind<IMapper>().ToConstant(AutoMapperConfig.RegisterAutoMapper());

            RegisterIdentityStores(kernel);
            RegisterIdentityManagers(kernel);

            //kernel.Bind<>()
        }

        public static T GetService<T>()
        {
            _cannotGet = false;
            T service = Kernel.TryGet<T>();
            _cannotGet = true;
            return service;
        }

        public static void RegisterIdentityStores(IKernel kernel)
        {
            kernel.Bind<IRoleStore<Roles, int>>().ToMethod(ninjectContext =>
                {
                    var dbContext = ninjectContext.Kernel.Get<WebAnimeDbContext>();
                    return new RoleStore(dbContext);
                }
            );
            kernel.Bind<IUserStore<Users, int>>().ToMethod(ninjectContext =>
            {
                var dbContext = ninjectContext.Kernel.Get<WebAnimeDbContext>();
                return new UserStore(dbContext);
            });
        }
        public static void RegisterIdentityManagers(IKernel kernel)
        {
            kernel.Bind<RoleManager<Roles, int>>().ToMethod(ninjectContext =>
                {
                    var roleStore = ninjectContext.Kernel.Get<RoleStore>();
                    return new RoleManager(roleStore);
                }
            );
            kernel.Bind<UserManager<Users, int>>().ToMethod(ninjectContext =>
            {
                var userStore = ninjectContext.Kernel.Get<UserStore>();

                return new UserManager(userStore);
            });


        }
    }
}