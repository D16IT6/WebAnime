using AutoMapper;
using DataModels.EF;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DataModels.Repository.Interface;
using ViewModels.Admin;

namespace WebAnime.MVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class StudioController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IStudioRepository _studioRepository;

        public StudioController(IMapper mapper, IStudioRepository studioRepository)
        {
            _mapper = mapper;
            _studioRepository = studioRepository;
        }
        [HttpGet]
        public async Task<ActionResult> Index()
        {

            var studioViewModelList =
                _mapper.Map<IEnumerable<Studios>, IEnumerable<StudioViewModel>>(await _studioRepository.GetAll());
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
                if (await _studioRepository.Create(studio))
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

            var studioViewModel = _mapper.Map<StudioViewModel>(await _studioRepository.GetById(id));
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

                if (await _studioRepository.Update(studio))
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

            var studioViewModel = _mapper.Map<StudioViewModel>(await _studioRepository.GetById(id));
            if (studioViewModel == null) return HttpNotFound();
            return View(studioViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(StudioViewModel model)
        {
            int deletedBy = User.Identity.GetUserId<int>();

            if (await _studioRepository.Delete(model.Id, deletedBy))
            {
                return RedirectToAction("Index", "Studio");
            }
            ModelState.AddModelError(string.Empty, @"Lỗi không xoá được, vui lòng thử lại");
            return View(); ;
        }

    }
}