using AutoMapper;
using DataModels.EF;
using DataModels.EF.Identity;
using Microsoft.AspNet.Identity;
using System.Linq;
using ViewModels.Admin;
using ViewModels.Client;
using static DataModels.Helpers.RoleManagerExtensions;
using UserViewModel = ViewModels.Admin.UserViewModel;

namespace WebAnime.MVC
{
    public class AutoMapperConfig
    {
        public static IMapper RegisterAutoMapper()
        {
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<Countries, CountryViewModel>();
                    cfg.CreateMap<CountryViewModel, Countries>();


                    cfg.CreateMap<Animes, AnimeViewModel>()
                        .ForMember(
                            x => x.StudiosId,
                            option => { option.MapFrom(src => src.Studios.Select(x => x.Id).ToArray()); }
                        )
                        .ForMember(
                            x => x.CategoriesId,
                            option => { option.MapFrom(src => src.Categories.Select(x => x.Id).ToArray()); }
                        );

                    cfg.CreateMap<AnimeViewModel, Animes>();

                    cfg.CreateMap<StudioViewModel, Studios>();
                    cfg.CreateMap<Studios, StudioViewModel>();


                    cfg.CreateMap<ServerViewModel, Servers>();
                    cfg.CreateMap<Servers, ServerViewModel>();

                    cfg.CreateMap<EpisodeViewModel, Episodes>();
                    cfg.CreateMap<Episodes, EpisodeViewModel>();


                    cfg.CreateMap<Users, UserViewModel>()
                        .ForMember
                        (
                            x => x.RoleList,
                            options => options.MapFrom(
                                user =>
                                    NinjectConfig.GetService<UserManager>().GetRoles(user.Id).ToArray()
                            )
                        )
                        .ForMember(
                            x => x.RoleListIds,
                            options => options.MapFrom(user =>
                                NinjectConfig.GetService<RoleManager>()
                                    .GetRoleIdsFromUser(NinjectConfig.GetService<UserManager>(), user.Id)
                            )
                        );

                    cfg.CreateMap<ExternalLoginConfirmationViewModel, Users>();


                    cfg.CreateMap<UserViewModel, Users>();

                    cfg.CreateMap<Blogs, ViewModels.Admin.BlogViewModel>()
                        .ForMember(
                            x => x.BlogCategoryIds,
                            memberOptions: options =>
                            {
                                options.MapFrom(
                                    blog =>
                                        blog.BlogCategories
                                            .Where(x => !x.IsDeleted)
                                            .Select(x => x.Id)
                                            .ToArray()
                                    );
                            }
                            )
                        ;
                    cfg.CreateMap<ViewModels.Admin.BlogViewModel, Blogs>();

                    cfg.CreateMap<Blogs, BlogLitteViewModel>();


                    cfg.CreateMap<BlogCommentViewModel, BlogComments>();

                    cfg.CreateMap<BlogComments, BlogCommentViewModel>();

                    cfg.CreateMap<BlogCategories, BlogCategoryViewModel>();
                    cfg.CreateMap<BlogCategoryViewModel, BlogCategories>();

                    cfg.CreateMap<CommentViewModel, Comments>();
                    cfg.CreateMap<Comments, CommentViewModel>();

                    cfg.CreateMap<RegisterViewModel, Users>();

                    cfg.CreateMap<ScheduleViewModel, Schedules>();
                    cfg.CreateMap<Schedules, ScheduleViewModel>();

                }
            );
            return config.CreateMapper();
        }
    }
}