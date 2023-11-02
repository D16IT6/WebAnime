using AutoMapper;
using DataModels.Dto;
using DataModels.EF;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using ViewModels.Admin;

namespace WebAnime.MVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class StudioController : Controller
    {
        private readonly IMapper _mapper;
        private readonly StudioDto _studioDto;

        public StudioController(IMapper mapper, StudioDto studioDto)
        {
            _mapper = mapper;
            this._studioDto = studioDto;

        }
        [HttpGet]
        public async Task<ActionResult> Index()
        {

            var studioViewModelList =
                _mapper.Map<IEnumerable<Studios>, IEnumerable<StudioViewModel>>(await _studioDto.GetAll());
            return View(studioViewModelList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(StudioViewModel model)
        {
            if (ModelState.IsValid)
            {

                var studio = _mapper.Map<Studios>(model);
                studio.CreatedBy = User.Identity.GetUserId<int>();
                if (await _studioDto.Add(studio))
                {
                    return RedirectToAction("Index", "Studio");
                }

                ModelState.AddModelError(string.Empty, @"Lỗi không thêm được, vui lòng thử lại");
                return View();
            }
            ModelState.AddModelError(string.Empty, @"Đầu vào lỗi, vui lòng thử lại");
            return View();
        }
        public async Task<ActionResult> Update(int id)
        {

            var studioViewModel = _mapper.Map<StudioViewModel>(await _studioDto.GetById(id));
            if (studioViewModel == null) return HttpNotFound();

            return View(studioViewModel);
        }
        [HttpPost]
        public async Task<ActionResult> Update(StudioViewModel model)
        {
            if (ModelState.IsValid)
            {

                var studio = _mapper.Map<Studios>(model);
                studio.ModifiedBy = User.Identity.GetUserId<int>();

                if (await _studioDto.Update(studio))
                {
                    return RedirectToAction("Index", "Studio");
                }

                ModelState.AddModelError(string.Empty, @"Lỗi không cập nhật được, vui lòng thử lại");
                return View();
            }
            ModelState.AddModelError(string.Empty, @"Đầu vào lỗi, vui lòng thử lại");
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {

            var studioViewModel = _mapper.Map<StudioViewModel>(await _studioDto.GetById(id));
            if (studioViewModel == null) return HttpNotFound();
            return View(studioViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(StudioViewModel model)
        {
            int deletedBy = User.Identity.GetUserId<int>();

            if (await _studioDto.Delete(model.Id, deletedBy))
            {
                return RedirectToAction("Index", "Studio");
            }
            ModelState.AddModelError(string.Empty, @"Lỗi không xoá được, vui lòng thử lại");
            return View(); ;
        }

    }
}