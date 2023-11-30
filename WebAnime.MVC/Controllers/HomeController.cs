using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using DataModels.Repository.Interface;

namespace WebAnime.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAgeRatingRepository _ageRatingRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITypeRepository _typeRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IStatusRepository _statusRepository;

        public HomeController(IAgeRatingRepository ageRatingRepository, ICategoryRepository categoryRepository, ITypeRepository typeRepository, ICountryRepository countryRepository, IStatusRepository statusRepository)
        {
            _ageRatingRepository = ageRatingRepository;
            _categoryRepository = categoryRepository;
            _typeRepository = typeRepository;
            _countryRepository = countryRepository;
            _statusRepository = statusRepository;
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