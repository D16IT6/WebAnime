using AutoMapper;
using DataModels.EF;
using DataModels.Repository.Interface;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using ViewModels.Admin;
using WebAnime.MVC.Components;

namespace WebAnime.MVC.Areas.Admin.Controllers
{
    [AdminAreaAuthorize]
    public class StudioController(IMapper mapper, IStudioRepository studioRepository) : Controller
    {
        [HttpGet]
        public async Task<ActionResult> Index()
        {

            var studioViewModelList =
                mapper.Map<IEnumerable<Studios>, IEnumerable<StudioViewModel>>(await studioRepository.GetAll());
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
                var studio = mapper.Map<Studios>(model);
                studio.CreatedBy = User.Identity.GetUserId<int>();
                if (await studioRepository.Create(studio))
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

            var studioViewModel = mapper.Map<StudioViewModel>(await studioRepository.GetById(id));
            if (studioViewModel == null) return HttpNotFound();

            return View(studioViewModel);
        }
        [HttpPost]
        public async Task<ActionResult> Update(StudioViewModel model)
        {
            if (ModelState.IsValid)
            {

                var studio = mapper.Map<Studios>(model);
                studio.ModifiedBy = User.Identity.GetUserId<int>();

                if (await studioRepository.Update(studio))
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

            var studioViewModel = mapper.Map<StudioViewModel>(await studioRepository.GetById(id));
            if (studioViewModel == null) return HttpNotFound();
            return View(studioViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(StudioViewModel model)
        {
            int deletedBy = User.Identity.GetUserId<int>();

            if (await studioRepository.Delete(model.Id, deletedBy))
            {
                return RedirectToAction("Index", "Studio");
            }
            ModelState.AddModelError(string.Empty, @"Lỗi không xoá được, vui lòng thử lại");
            return View(); ;
        }

        [HttpGet]
        public async Task<ActionResult> GetPaging(string searchName, int pageNumber, int pageSize)
        {
            var searchResult = await studioRepository.GetPaging(searchName, pageSize, pageNumber);

            var result = searchResult.Data.Select
            (x => new
                {
                    x.Id,
                    x.Name,
                });

            if (result == null)
                return HttpNotFound();
            return Json(new
            {
                data = result,
                TotalPages = searchResult.TotalPages
            }, JsonRequestBehavior.AllowGet);
        }

    }
}