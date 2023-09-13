using AutoMapper;
using DataModels.Dto;
using DataModels.EF;
using System.Web.Mvc;
using WebAnime.MVC.Areas.Admin.Models;

namespace WebAnime.MVC.Areas.Admin.Controllers
{
    public class AnimeController : Controller
    {
        private readonly IMapper _mapper;

        public AnimeController(IMapper mapper)
        {
            _mapper = mapper;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            LoadEditData();
            return View();
        }
        [HttpPost]
        public ActionResult Create(AnimeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var animeDto = new AnimeDto();
                var entity = _mapper.Map<Animes>(model);
                if (animeDto.Add(entity))
                {
                    return RedirectToAction("Index", "Anime");
                }
                LoadEditData();
                ModelState.AddModelError("", @"Lỗi thêm mới, vui lòng thử lại");
                return View();
            }
            LoadEditData();
            ModelState.AddModelError("", @"Lỗi đầu vào, vui lòng kiểm tra lại");
            return View();
        }
        void LoadEditData()
        {
            var ageRatingDto = new AgeRatingDto();
            ViewBag.AgeRating = ageRatingDto.GetAll();

            var categoryDto = new CategoryDto();
            ViewBag.Category = categoryDto.GetAll();

            var countryDto = new CountryDto();
            ViewBag.Country = countryDto.GetAll();

            var statusDto = new StatusDto();
            ViewBag.Status = statusDto.GetAll();

            var studioDto = new StudioDto();
            ViewBag.Studio = studioDto.GetAll();

            var typeDto = new TypeDto();
            ViewBag.Type = typeDto.GetAll();
        }
    }
}