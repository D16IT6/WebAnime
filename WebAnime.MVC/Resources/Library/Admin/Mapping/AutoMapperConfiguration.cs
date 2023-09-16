using AutoMapper;
using DataModels.EF;
using System.Linq;
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
                        destinationMember: x => x.StudiosId,
                        memberOptions: option =>
                        {
                            option.MapFrom(src => src.Studios.Select(x => x.Id).ToArray());
                        }
                    )
                    .ForMember(
                        destinationMember: x => x.CategoriesId,
                        memberOptions: option =>
                        {
                            option.MapFrom(src => src.Categories.Select(x => x.Id).ToArray());
                        }
                    )
                    //.ForMember(
                    //    destinationMember: x => x.AgeRatingId,
                    //    memberOptions: option =>
                    //    {
                    //        option.MapFrom(src => src.AgeRatings.Id);
                    //    }
                    //)
                    //.ForMember(
                    //    destinationMember: x => x.CountryId,
                    //    memberOptions: option =>
                    //    {
                    //        option.MapFrom(src => src.Countries.Id);
                    //    }
                    //)
                    //.ForMember(
                    //    destinationMember: x => x.StatusId,
                    //    memberOptions: option =>
                    //    {
                    //        option.MapFrom(src => src.Statuses.Id);
                    //    }
                    //)
                    //.ForMember(
                    //    destinationMember: x => x.TypeId,
                    //    memberOptions: option =>
                    //    {
                    //        option.MapFrom(src => src.Types.Id);
                    //    }
                    //)
                    ;
                    cfg.CreateMap<AnimeViewModel, Animes>();

                    cfg.CreateMap<StudioViewModel, Studios>();
                    cfg.CreateMap<Studios, StudioViewModel>();
                }
            );
            return config.CreateMapper();
        }
    }

}