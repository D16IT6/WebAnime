using AutoMapper;
using DataModels.EF;
using DataModels.EF.Identity;
using ViewModels.Admin;

namespace WebAnime.APINetCore
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Countries, CountryViewModel>();
            CreateMap<CountryViewModel, Countries>();


            CreateMap<Animes, AnimeViewModel>()
                .ForMember(
                    x => x.StudiosId,
                    option => { option.MapFrom(src => src.Studios.Select(x => x.Id).ToArray()); }
                )
                .ForMember(
                    x => x.CategoriesId,
                    option => { option.MapFrom(src => src.Categories.Select(x => x.Id).ToArray()); }
                );

            CreateMap<AnimeViewModel, Animes>();

            CreateMap<StudioViewModel, Studios>();
            CreateMap<Studios, StudioViewModel>();


            CreateMap<ServerViewModel, Servers>();
            CreateMap<Servers, ServerViewModel>();

            CreateMap<EpisodeViewModel, Episodes>();
            CreateMap<Episodes, EpisodeViewModel>();


            CreateMap<Users, UserViewModel>()
                .ForMember
                (
                    x => x.RoleList,
                    options => options.MapFrom(
                        user => new string[] { "Implement later" })
                )
                .ForMember(
                    x => x.RoleListIds,
                    options => options.MapFrom(user => new int[] { 0 }
                    )
                );


            CreateMap<UserViewModel, Users>();

            CreateMap<Blogs, BlogViewModel>()
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
            CreateMap<BlogViewModel, Blogs>();
        }
    }

}
