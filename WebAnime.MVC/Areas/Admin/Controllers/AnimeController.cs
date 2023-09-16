using AutoMapper;
using DataModels.Dto;
using DataModels.EF;
using System.Collections.Generic;
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
            var animeDto = new AnimeDto();
            var animeViewModelList = _mapper.Map<IEnumerable<Animes>, IEnumerable<AnimeViewModel>>(animeDto.GetAll());
            return View(animeViewModelList);
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

        public ActionResult Update(int id)
        {
            LoadEditData();
            var animeDto = new AnimeDto();
            var anime = animeDto.GetById(id);
            if (anime == null) return new HttpNotFoundResult("");
            var animeViewModel = _mapper.Map<AnimeViewModel>(anime);
            return View(animeViewModel);
        }
        [HttpPost]
        public ActionResult Update(AnimeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var animeDto = new AnimeDto();
                var entity = _mapper.Map<Animes>(model);
                if (animeDto.Update(entity))
                {
                    return RedirectToAction("Index", "Anime");
                }
                LoadEditData();
                ModelState.AddModelError("", @"Lỗi cập nhật, vui lòng thử lại");
                return View(model);
            }
            LoadEditData();
            ModelState.AddModelError("", @"Lỗi đầu vào, vui lòng kiểm tra lại");
            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var animeDto = new AnimeDto();
            var anime = animeDto.GetById(id);
            if (anime == null) return new HttpNotFoundResult("");
            var animeViewModel = _mapper.Map<AnimeViewModel>(anime);
            return View(animeViewModel);
        }
        [HttpPost]
        public ActionResult Delete(AnimeViewModel model)
        {
            var animeDto = new AnimeDto();
            if (animeDto.Delete(model.Id))
            {
                return RedirectToAction("Index", "Anime");
            }
            ModelState.AddModelError("", @"Lỗi xoá, vui lòng thử lại");
            return View(model);
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