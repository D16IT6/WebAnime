﻿using AutoMapper;
using DataModels.EF;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using DataModels.Repository.Implement.EF6;
using DataModels.Repository.Interface;
using ViewModels.Admin;

namespace WebAnime.MVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CountryController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICountryRepository _countryRepository;

        public CountryController(IMapper mapper, ICountryRepository countryRepository)
        {
            _mapper = mapper;
            _countryRepository = countryRepository;

        }

        public async Task<ActionResult> Index()
        {
            var countryViewModelList = _mapper.Map<IEnumerable<Countries>, IEnumerable<CountryViewModel>>(await _countryRepository.GetAll());

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
                country.CreatedBy = User.Identity.GetUserId<int>();

                if (await _countryRepository.Create(country))
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
            var countryViewModel = _mapper.Map<CountryViewModel>(await _countryRepository.GetById(id));
            if (countryViewModel == null) return HttpNotFound();
            return View(countryViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Update(CountryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var country = _mapper.Map<Countries>(model);
                country.ModifiedBy = User.Identity.GetUserId<int>();

                if (await _countryRepository.Update(country))
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
            var countryViewModel = _mapper.Map<CountryViewModel>(await _countryRepository.GetById(id));
            if (countryViewModel == null) return HttpNotFound();
            return View(countryViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(CountryViewModel model)
        {
            int deletedBy = User.Identity.GetUserId<int>();

            if (await _countryRepository.Delete(model.Id, deletedBy))
            {
                return RedirectToAction("Index", "Country");
            }
            ModelState.AddModelError(string.Empty, @"Lỗi không xoá được, vui lòng thử lại");
            return View();
        }


    }
}