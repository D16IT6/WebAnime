﻿using AutoMapper;
using DataModels.EF;
using DataModels.EF.Identity;
using DataModels.Repository.Implement.Dapper;
using DataModels.Repository.Implement.EF6;
using DataModels.Repository.Interface;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.WebApi;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace WebAnime.API2
{
    public class NinjectConfig
    {
        private static bool _cannotGet;
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

                    GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(_kernel);

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

            RegisterRepositoryEf(kernel);
            RegisterRepositoryDapper(kernel);

        }

        static void RegisterRepositoryEf(IKernel kernel)
        {
            //kernel.Bind<IAgeRatingRepository>().To<AgeRatingRepository>();
            kernel.Bind<IAnimeRepository>().To<AnimeRepository>();
            //kernel.Bind<IBlogCategoryRepository>().To<BlogCategoryRepository>();
            //kernel.Bind<IBlogRepository>().To<BlogRepository>();
            kernel.Bind<ICategoryRepository>().To<CategoryRepository>();
            //kernel.Bind<ICountryRepository>().To<CountryRepository>();
            kernel.Bind<ICommentRepository>().To<CommentRepository>();
            kernel.Bind<IEpisodeRepository>().To<EpisodeRepository>();
            kernel.Bind<IServerRepository>().To<ServerRepository>();
            //kernel.Bind<IStatusRepository>().To<StatusRepository>();
            kernel.Bind<IStudioRepository>().To<StudioRepository>();
            kernel.Bind<ITypeRepository>().To<TypeRepository>();
            kernel.Bind<IRatingRepository>().To<RatingRepository>();
            kernel.Bind<IUserTokenRepository>().To<UserTokenRepository>();
            kernel.Bind<IAnimeFavoriteRepository>().To<AnimeFavoriteRepository>();
        }

        static void RegisterRepositoryDapper(IKernel kernel)
        {
            RegisterConnection(kernel);

            kernel.Bind<IAgeRatingRepository>().To<AgeRatingRepositoryDapper>();
            kernel.Bind<IBlogCategoryRepository>().To<BlogCategoryRepositoryDapper>();
            kernel.Bind<IBlogRepository>().To<BlogRepositoryDapper>();
            kernel.Bind<IBlogCommentRepository>().To<BlogCommentRepositoryDapper>();
            kernel.Bind<ICountryRepository>().To<CountryRepositoryDapper>();
            //kernel.Bind<IEpisodeRepository>().To<EpisodeRepositoryDapper>();
            kernel.Bind<IStatusRepository>().To<StatusRepositoryDapper>();
        }
        private static void RegisterConnection(IKernel kernel)
        {
            kernel.Bind<IDbConnection>().ToMethod(_ =>
            {
                string connectionString =
                    System.Web.Configuration.WebConfigurationManager
                        .ConnectionStrings[nameof(WebAnimeDbContext) + "Dapper"].ConnectionString;

                return new SqlConnection(connectionString);
            });
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
                var userManager = new UserManager(userStore);


                return userManager;
            });


        }
    }
}