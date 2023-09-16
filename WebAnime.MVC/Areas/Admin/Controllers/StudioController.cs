using AutoMapper;
using DataModels.Dto;
using DataModels.EF;
using System.Collections.Generic;
using System.Web.Mvc;
using WebAnime.MVC.Areas.Admin.Models;

namespace WebAnime.MVC.Areas.Admin.Controllers
{
    public class StudioController : Controller
    {
        private readonly IMapper _mapper;

        public StudioController(IMapper mapper)
        {
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult Index()
        {
            var studioDto = new StudioDto();
            var studioViewModelList =
                _mapper.Map<IEnumerable<Studios>, IEnumerable<StudioViewModel>>(studioDto.GetAll());
            return View(studioViewModelList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(StudioViewModel model)
        {
            if (ModelState.IsValid)
            {
                var studioDto = new StudioDto();
                var studio = _mapper.Map<Studios>(model);
                if (studioDto.Add(studio))
                {
                    return RedirectToAction("Index", "Studio");
                }

                ModelState.AddModelError("", @"Lỗi không thêm được, vui lòng thử lại");
                return View();
            }
            ModelState.AddModelError("", @"Đầu vào lỗi, vui lòng thử lại");
            return View();
        }
        public ActionResult Update(int id)
        {
            var studioDto = new StudioDto();
            var studioViewModel = _mapper.Map<StudioViewModel>(studioDto.GetById(id));
            if (studioViewModel == null) return HttpNotFound();

            return View(studioViewModel);
        }
        [HttpPost]
        public ActionResult Update(StudioViewModel model)
        {
            if (ModelState.IsValid)
            {
                var studioDto = new StudioDto();
                var studio = _mapper.Map<Studios>(model);
                if (studioDto.Update(studio))
                {
                    return RedirectToAction("Index", "Studio");
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
            var studioDto = new StudioDto();
            var studioViewModel = _mapper.Map<StudioViewModel>(studioDto.GetById(id));
            if (studioViewModel == null) return HttpNotFound();
            return View(studioViewModel);
        }

        [HttpPost]
        public ActionResult Delete(StudioViewModel model)
        {
            var studioDto = new StudioDto();
            if (studioDto.Delete(model.Id))
            {
                return RedirectToAction("Index", "Studio");
            }
            ModelState.AddModelError("", @"Lỗi không xoá được, vui lòng thử lại");
            return View(); ;
        }

    }
}