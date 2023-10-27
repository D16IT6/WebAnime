using AutoMapper;
using DataModels.Dto;
using DataModels.EF;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebAnime.MVC.Areas.Admin.Models;

namespace WebAnime.MVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CountryController : Controller
    {
        private readonly IMapper _mapper;
        private readonly CountryDto _countryDto;

        public CountryController(IMapper mapper, CountryDto countryDto)
        {
            _mapper = mapper;
            _countryDto = countryDto;
        }

        public async Task<ActionResult> Index()
        {
            var countryViewModelList = _mapper.Map<IEnumerable<Countries>, IEnumerable<CountryViewModel>>(await _countryDto.GetAll());
            return View(countryViewModelList);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CountryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var country = _mapper.Map<Countries>(model);
                country.CreatedBy = int.Parse(User.Identity.GetUserId());

                if (await _countryDto.Add(country))
                {
                    return RedirectToAction("Index", "Country");
                }

                ModelState.AddModelError(string.Empty, @"Lỗi không thêm được, vui lòng thử lại");
            }
            ModelState.AddModelError(string.Empty, @"Đầu vào lỗi, vui lòng thử lại");
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            var countryViewModel = _mapper.Map<CountryViewModel>(await _countryDto.GetById(id));
            if (countryViewModel == null) return HttpNotFound();
            return View(countryViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Update(CountryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var country = _mapper.Map<Countries>(model);
                country.ModifiedBy = int.Parse(User.Identity.GetUserId());

                if (await _countryDto.Update(country))
                {
                    return RedirectToAction("Index", "Country");
                }

                ModelState.AddModelError(string.Empty, @"Lỗi không cập nhật được, vui lòng thử lại");
            }
            ModelState.AddModelError(string.Empty, @"Đầu vào lỗi, vui lòng thử lại");
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var countryViewModel = _mapper.Map<CountryViewModel>(await _countryDto.GetById(id));
            if (countryViewModel == null) return HttpNotFound();
            return View(countryViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(CountryViewModel model)
        {
            int deletedBy = int.Parse(User.Identity.GetUserId());

            if (await _countryDto.Delete(model.Id, deletedBy))
            {
                return RedirectToAction("Index", "Country");
            }
            ModelState.AddModelError(string.Empty, @"Lỗi không xoá được, vui lòng thử lại");
            return View();
        }


    }
}