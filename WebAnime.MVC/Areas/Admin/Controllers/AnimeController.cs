using System;
using AutoMapper;
using DataModels.EF;
using DataModels.Repository.Interface;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using ViewModels.Admin;
using ViewModels.Client;
using WebAnime.MVC.Components;

namespace WebAnime.MVC.Areas.Admin.Controllers
{
    [AdminAreaAuthorize]
    public class AnimeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAnimeRepository _animeRepository;
        private readonly IAgeRatingRepository _ageRatingRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITypeRepository _typeRepository;
        private readonly IStudioRepository _studioRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly IServerRepository _serverRepository;


        public AnimeController(
            IMapper mapper,
            IAnimeRepository animeRepository,
            IAgeRatingRepository ageRatingRepository,
            ICategoryRepository categoryRepository,
            ITypeRepository typeRepository,
            IStudioRepository studioRepository,
            ICountryRepository countryRepository,
            IStatusRepository statusRepository,
            IServerRepository serverRepository
            )
        {
            _mapper = mapper;
            _animeRepository = animeRepository;
            _ageRatingRepository = ageRatingRepository;
            _categoryRepository = categoryRepository;
            _typeRepository = typeRepository;
            _studioRepository = studioRepository;
            _countryRepository = countryRepository;
            _statusRepository = statusRepository;
            _serverRepository = serverRepository;
        }

        public async Task<ActionResult> Index()
        {
            //var animeViewModelList = _mapper.Map<IEnumerable<Animes>, IEnumerable<AnimeViewModel>>(await _animeRepository.GetAll());
            //var firstServer = await _serverRepository.GetFirst();
            //ViewBag.FirstServerId = firstServer.Id;

            //var tempCategoriesEnumerable = await _categoryRepository.GetAll();
            //ViewBag.categoryList = tempCategoriesEnumerable.ToArray();

            //return View(animeViewModelList);


            var firstServer = await _serverRepository.GetFirst();
            ViewBag.FirstServerId = firstServer.Id;
            return View();
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
                entity.ModifiedBy = User.Identity.GetUserId<int>();

                if (await _animeRepository.Create(entity))
                {
                    TempData[AlertConstants.SuccessHeader] = "Anime mới";
                    TempData[AlertConstants.SuccessMessage] = "Thêm anime mới thành công";

                    return RedirectToAction("Index", "Anime");
                }
                await LoadEditData();
                TempData[AlertConstants.ErrorMessage] = "Lỗi thêm mới, vui lòng thử lại";
                ModelState.AddModelError(string.Empty, @"Lỗi thêm mới, vui lòng thử lại");
            }
            await LoadEditData();

            TempData[AlertConstants.ErrorMessage] = "Lỗi đầu vào, vui lòng thử lại";
            ModelState.AddModelError(string.Empty, @"Lỗi đầu vào, vui lòng kiểm tra lại");
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            await LoadEditData();
            var anime = await _animeRepository.GetById(id);
            if (anime == null) return HttpNotFound(string.Empty);
            var animeViewModel = _mapper.Map<AnimeViewModel>(anime);
            return View(animeViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Update(AnimeViewModel model)
        {
            if (ModelState.IsValid)
            {

                var entity = _mapper.Map<Animes>(model);

                entity.ModifiedBy = User.Identity.GetUserId<int>();

                if (await _animeRepository.Update(entity))
                {
                    TempData[AlertConstants.SuccessMessage] = $"Cập nhật anime {entity.Title} thành công";
                    TempData[AlertConstants.SuccessHeader] = "Cập nhật anime";

                    return RedirectToAction("Index", "Anime");
                }
                await LoadEditData();

                TempData[AlertConstants.ErrorMessage] = "Lỗi cập nhật, vui lòng thử lại";
                ModelState.AddModelError(string.Empty, @"Lỗi cập nhật, vui lòng thử lại");
            }
            await LoadEditData();
            TempData[AlertConstants.ErrorMessage] = "Lỗi đầu vào, vui lòng thử lại";
            ModelState.AddModelError(string.Empty, @"Lỗi đầu vào, vui lòng kiểm tra lại");
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var anime = await _animeRepository.GetById(id);
            if (anime == null) return HttpNotFound(string.Empty);

            var animeViewModel = _mapper.Map<AnimeViewModel>(anime);
            return View(animeViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(AnimeViewModel model)
        {
            int deletedBy = User.Identity.GetUserId<int>();

            if (await _animeRepository.Delete(model.Id, deletedBy))
            {
                TempData[AlertConstants.SuccessMessage] = $"Xóa anime {model.Title} thành công!";
                TempData[AlertConstants.SuccessHeader] = "Xóa anime";
                return RedirectToAction("Index", "Anime");
            }
            TempData[AlertConstants.ErrorMessage] = "Lỗi xóa anime, vui lòng thử lại";
            ModelState.AddModelError(string.Empty, @"Lỗi xoá, vui lòng thử lại");
            return View(model);
        }


        [HttpGet]
        public async Task<ActionResult> GetPaging(string searchTitle,int pageNumber,int pageSize)
        {
            if(pageNumber <= 0 || pageSize <= 0) return HttpNotFound(string.Empty);

            var queryResult = await _animeRepository.GetPaging(searchTitle, pageNumber, pageSize);
            var result = queryResult.Data
                .Select(
                    x => new
                    {
                        x.Title,
                        x.OriginalTitle,
                        x.Id,
                        x.Duration,
                        x.TotalEpisodes,
                        x.Synopsis,
                        Release = x.Release?.ToString("dd/MM/yyyy") ?? "Khong biet",
                        Categories = String.Join(",",x.Categories.Where(z =>!z.IsDeleted).Select(c => c.Name)) +"."
                    }
                );

            return Json(new
            {
                data = result,
                queryResult.PageCount,
                queryResult.TotalPages

            }, JsonRequestBehavior.AllowGet);
        }


        async Task LoadEditData()
        {
            var ageRatingTask = _ageRatingRepository.GetAll();
            var categoryTask = _categoryRepository.GetAll();
            var countryTask = _countryRepository.GetAll();
            var statusTask = _statusRepository.GetAll();
            var studioTask = _studioRepository.GetAll();
            var typeTask = _typeRepository.GetAll();

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