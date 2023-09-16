using AutoMapper;
using DataModels.Dto;
using DataModels.EF;
using System.Collections.Generic;
using System.Web.Mvc;
using WebAnime.MVC.Areas.Admin.Models;

namespace WebAnime.MVC.Areas.Admin.Controllers
{
    public class CountryController : Controller
    {
        private readonly IMapper _mapper;

        public CountryController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public ActionResult Index()
        {
            var countryDto = new CountryDto();
            var countryViewModelList =
                _mapper.Map<IEnumerable<Countries>, IEnumerable<CountryViewModel>>(countryDto.GetAll());
            return View(countryViewModelList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CountryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var countryDto = new CountryDto();
                var country = _mapper.Map<Countries>(model);
                if (countryDto.Add(country))
                {
                    return RedirectToAction("Index", "Country");
                }

                ModelState.AddModelError("", @"Lỗi không thêm được, vui lòng thử lại");
                return View();
            }
            ModelState.AddModelError("", @"Đầu vào lỗi, vui lòng thử lại");
            return View();
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var countryDto = new CountryDto();
            var countryViewModel = _mapper.Map<CountryViewModel>(countryDto.GetById(id));
            if (countryViewModel == null) return HttpNotFound();

            return View(countryViewModel);
        }

        [HttpPost]
        public ActionResult Update(CountryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var countryDto = new CountryDto();
                var country = _mapper.Map<Countries>(model);
                if (countryDto.Update(country))
                {
                    return RedirectToAction("Index", "Country");
                }

                ModelState.AddModelError("", @"Lỗi không cập nhật được, vui lòng thử lại");
                return View();
            }
            ModelState.AddModelError("", @"Đầu vào lỗi, vui lòng thử lại");
            return View();
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var countryDto = new CountryDto();
            var countryViewModel = _mapper.Map<CountryViewModel>(countryDto.GetById(id));
            if (countryViewModel == null) return HttpNotFound();
            return View(countryViewModel);
        }

        [HttpPost]
        public ActionResult Delete(CountryViewModel model)
        {
            var countryDto = new CountryDto();
            if (countryDto.Delete(model.Id))
            {
                return RedirectToAction("Index", "Country");
            }
            ModelState.AddModelError("", @"Lỗi không xoá được, vui lòng thử lại");
            return View(); ;
        }
    }
}