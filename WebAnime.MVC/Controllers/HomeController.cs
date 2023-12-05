using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using DataModels.Repository.Interface;
using ViewModels.Client;

namespace WebAnime.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAgeRatingRepository _ageRatingRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITypeRepository _typeRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly IAnimeRepository _animeRepository;
        private readonly IScheduleRepository _scheduleRepository;

        public HomeController(IAgeRatingRepository ageRatingRepository, ICategoryRepository categoryRepository, ITypeRepository typeRepository, ICountryRepository countryRepository, IStatusRepository statusRepository ,IAnimeRepository animeRepository,IScheduleRepository scheduleRepository)
        {
            _ageRatingRepository = ageRatingRepository;
            _categoryRepository = categoryRepository;
            _typeRepository = typeRepository;
            _countryRepository = countryRepository;
            _statusRepository = statusRepository;
            _animeRepository =animeRepository;
            _scheduleRepository = scheduleRepository;
        }

        public async Task<ActionResult> Index()
        {
            return await Task.FromResult(View());
        }

        public async Task<ActionResult> Search()
        {
            await LoadDependencies();

            return await Task.FromResult(View());
        }

        public async Task<ActionResult> Contact()
        {
            return await Task.FromResult(View());
        }

        public async Task<ActionResult> Schedule()
        {
            var animeTask =  _animeRepository.GetAll().Result;
            var scheduleTask = await _scheduleRepository.GetAll();

            if (animeTask.Any() && scheduleTask.Any())
            {
                var results = animeTask.Join(scheduleTask, a => a.Id, s => s.Id, (a, s) => new AnimeScheduleModelView
                {
                    Id = a.Id,
                    Title = a.Title,
                    Poster = a.Poster,
                    ViewCount = a.ViewCount,
                    TotalEpisode = a.TotalEpisodes,
                    CurrentEpisode = a.Episodes
                        .Where(episodes => !episodes.IsDeleted)
                        .GroupBy(z => z.ServerId)
                        .Select(group => group.Count())
                        .DefaultIfEmpty(0)
                        .Max(),
                    AiringDate = s.AiringDate,
                    AiringTime = s.AiringTime,
                }).Where(a=>a.AiringDate>DateTime.Now);

                return View(results);
            }
            else
            {
                // Xử lý khi ít nhất một trong hai danh sách rỗng
                // Ví dụ: thông báo lỗi hoặc trả về một trang không có dữ liệu
                return View();
            }
        }
        async Task LoadDependencies()
        {
            var ageRatingTask = _ageRatingRepository.GetAll();
            var categoryTask = _categoryRepository.GetAll();
            var countryTask = _countryRepository.GetAll();
            var statusTask = _statusRepository.GetAll();
            var typeTask = _typeRepository.GetAll();

            await Task.WhenAll(ageRatingTask, categoryTask, countryTask, statusTask, typeTask);

            ViewBag.AgeRating = ageRatingTask.Result;
            ViewBag.Category = categoryTask.Result;
            ViewBag.Country = countryTask.Result;
            ViewBag.Status = statusTask.Result;
            ViewBag.Type = typeTask.Result;
        }
    }
}