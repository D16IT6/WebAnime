using System.Linq;
using AutoMapper;
using DataModels.EF;
using WebAnime.MVC.Areas.Admin.Models;

namespace WebAnime.MVC.Resources.Library.Admin.Mapping
{
    public class AutoMapperConfiguration
    {
        public static IMapper Configure()
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
                        )
                        ;
                    cfg.CreateMap<AnimeViewModel, Animes>();

                    cfg.CreateMap<StudioViewModel, Studios>();
                    cfg.CreateMap<Studios, StudioViewModel>();


                    cfg.CreateMap<ServerViewModel, Servers>();
                    cfg.CreateMap<Servers, ServerViewModel>();

                    cfg.CreateMap<EpisodeViewModel, Episodes>();
                    cfg.CreateMap<Episodes, EpisodeViewModel>();
                }
            );
            return config.CreateMapper();
        }
    }
}