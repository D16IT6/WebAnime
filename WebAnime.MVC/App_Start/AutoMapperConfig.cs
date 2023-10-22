﻿using AutoMapper;
using DataModels.EF;
using DataModels.EF.Identity;
using Microsoft.AspNet.Identity;
using System.Linq;
using WebAnime.MVC.Areas.Admin.Models;
using static DataModels.Helpers.RoleManagerExtensions;
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


                    cfg.CreateMap<UserViewModel, Users>();
                }
            );
            return config.CreateMapper();
        }
    }
}