using AutoMapper;
using DataModels.Dto;
using DataModels.EF;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebAnime.MVC.Areas.Admin.Models;

namespace WebAnime.MVC.Areas.Admin.Controllers
{
    public class AnimeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly AnimeDto _animeDto;
        private readonly AgeRatingDto _ageRatingDto;
        private readonly CategoryDto _categoryDto;
        private readonly TypeDto _typeDto;
        private readonly StudioDto _studioDto;
        private readonly CountryDto _countryDto;
        private readonly StatusDto _statusDto;
        private readonly ServerDto _serverDto;


        public AnimeController(IMapper mapper, AnimeDto animeDto, AgeRatingDto ageRatingDto, CategoryDto categoryDto, TypeDto typeDto, StudioDto studioDto, CountryDto countryDto, StatusDto statusDto, ServerDto serverDto)
        {
            _mapper = mapper;
            _animeDto = animeDto;
            _ageRatingDto = ageRatingDto;
            _categoryDto = categoryDto;
            _typeDto = typeDto;
            _studioDto = studioDto;
            _countryDto = countryDto;
            _statusDto = statusDto;
            _serverDto = serverDto;
        }

        public async Task<ActionResult> Index()
        {
            var animeViewModelList = _mapper.Map<IEnumerable<Animes>, IEnumerable<AnimeViewModel>>(await _animeDto.GetAll());
            var firstServer = await _serverDto.GetFirst();
            ViewBag.FirstServerId = firstServer.Id;

            var tempCategoriesEnumerable = await _categoryDto.GetAll();
            ViewBag.categoryList = tempCategoriesEnumerable.ToArray();

            return View(animeViewModelList);
        }
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            await LoadEditData();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(AnimeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = _mapper.Map<Animes>(model);
                if (await _animeDto.Add(entity))
                {
                    return RedirectToAction("Index", "Anime");
                }
                await LoadEditData();
                ModelState.AddModelError("", @"Lỗi thêm mới, vui lòng thử lại");
            }
            await LoadEditData();
            ModelState.AddModelError("", @"Lỗi đầu vào, vui lòng kiểm tra lại");
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            await LoadEditData();
            var anime = await _animeDto.GetById(id);
            if (anime == null) return HttpNotFound("");
            var animeViewModel = _mapper.Map<AnimeViewModel>(anime);
            return View(animeViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Update(AnimeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = _mapper.Map<Animes>(model);
                if (await _animeDto.Update(entity))
                {
                    return RedirectToAction("Index", "Anime");
                }
                await LoadEditData();
                ModelState.AddModelError("", @"Lỗi cập nhật, vui lòng thử lại");
            }
            await LoadEditData();
            ModelState.AddModelError("", @"Lỗi đầu vào, vui lòng kiểm tra lại");
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var anime = await _animeDto.GetById(id);
            if (anime == null) return HttpNotFound("");
            var animeViewModel = _mapper.Map<AnimeViewModel>(anime);
            return View(animeViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(AnimeViewModel model)
        {
            if (await _animeDto.Delete(model.Id))
            {
                return RedirectToAction("Index", "Anime");
            }
            ModelState.AddModelError("", @"Lỗi xoá, vui lòng thử lại");
            return View(model);
        }

        async Task LoadEditData()
        {
            var ageRatingTask = _ageRatingDto.GetAll();
            var categoryTask = _categoryDto.GetAll();
            var countryTask = _countryDto.GetAll();
            var statusTask = _statusDto.GetAll();
            var studioTask = _studioDto.GetAll();
            var typeTask = _typeDto.GetAll();

            await Task.WhenAll(ageRatingTask, categoryTask, countryTask, statusTask, studioTask, typeTask);

            ViewBag.AgeRating = ageRatingTask.Result;
            ViewBag.Category = categoryTask.Result;
            ViewBag.Country = countryTask.Result;
            ViewBag.Status = statusTask.Result;
            ViewBag.Studio = studioTask.Result;
            ViewBag.Type = typeTask.Result;
        }

    }
}