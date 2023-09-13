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
                }
            );
            return config.CreateMapper();
        }
    }

}