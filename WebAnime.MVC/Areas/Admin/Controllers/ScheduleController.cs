using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using DataModels.EF;
using DataModels.Repository.Interface;
using Microsoft.AspNet.Identity;
using ViewModels.Admin;
using WebAnime.MVC.Components;

namespace WebAnime.MVC.Areas.Admin.Controllers
{
    [AdminAreaAuthorize]
    public class ScheduleController : Controller
    {

        private readonly IMapper _mapper;
        private readonly IScheduleRepository _scheduleRepository;

        public ScheduleController(IMapper mapper, IScheduleRepository scheduleRepository)
        {
            _mapper = mapper;
            _scheduleRepository = scheduleRepository;
        }
        [HttpGet]
        public async Task<ActionResult> Index(int animeId)
        {
            var scheduleList = await _scheduleRepository.GetAll();
            var schedule = scheduleList.Where(x => x.Id == animeId).ToList();
            ViewBag.AnimeId = animeId;
            var scheduleRepsitoryViewModelList = _mapper.Map<IEnumerable<Schedules>, IEnumerable<ScheduleViewModel>>(schedule);
            return View(scheduleRepsitoryViewModelList);

        }

        [HttpGet]
        public async Task<ActionResult> Create(int id)
        {
            ViewBag.AnimeID = id;
            return await Task.FromResult(View());
        }

        [HttpPost]
        public async Task<ActionResult> Create(ScheduleViewModel model)
        {
            if (ModelState.IsValid)
            {
                int animeId = model.Id;
                var entity = _mapper.Map<Schedules>(model);
                entity.ModifiedBy = User.Identity.GetUserId<int>();
                entity.Id = animeId;
                if (await _scheduleRepository.Create(entity))
                {
                    TempData[AlertConstants.SuccessHeader] = "Lịch mới";
                    TempData[AlertConstants.SuccessMessage] = "Thêm lịch mới thành công";

                    return RedirectToAction("Index", "Anime");
                }
                TempData[AlertConstants.ErrorMessage] = "Lỗi thêm mới, vui lòng thử lại";
                ModelState.AddModelError(string.Empty, "Lỗi thêm mới, vui lòng thử lại");
            }
            TempData[AlertConstants.ErrorMessage] = "Lỗi đầu vào, vui lòng thử lại";
            ModelState.AddModelError(string.Empty, "Lỗi đầu vào, vui lòng kiểm tra lại");
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            ViewBag.AnimeID = id;
            var update = _mapper.Map<ScheduleViewModel>(await _scheduleRepository.GetById(id));
            if (update == null)
                return HttpNotFound();
            return View(update);
        }
        [HttpPost]
        public async Task<ActionResult> Update(ScheduleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = _mapper.Map<Schedules>(model);
                entity.ModifiedBy = User.Identity.GetUserId<int>();
                if (await _scheduleRepository.Update(entity))
                {
                    TempData[AlertConstants.SuccessHeader] = "Lịch mới";
                    TempData[AlertConstants.SuccessMessage] = "Sửa lịch chiếu thành công";
                    return RedirectToAction("Index", "Schedule", new { animeId = entity.Id });
                }
                TempData[AlertConstants.ErrorHeader] = "Lỗi";
                TempData[AlertConstants.ErrorMessage] = "Sửa không thành công";
                return View();
            }
            TempData[AlertConstants.ErrorMessage] = "Lỗi đầu vào, vui lòng thử lại";
            ModelState.AddModelError(string.Empty, @"Lỗi đầu vào, vui lòng kiểm tra lại");
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            ViewBag.AnimeID = id;
            var delete = _mapper.Map<ScheduleViewModel>(await _scheduleRepository.GetById(id));
            if (delete == null) return HttpNotFound();
            return View(delete);
        }
        [HttpPost]
        public async Task<ActionResult> Delete(ScheduleViewModel model)
        {
            int deletedBy = User.Identity.GetUserId<int>();
            if (await _scheduleRepository.Delete(model.Id, deletedBy))
            {
                TempData[AlertConstants.SuccessHeader] = "Xoá";
                TempData[AlertConstants.SuccessMessage] = "Xoá lịch chiếu thành công";
                return RedirectToAction("Index", "Schedule", new { animeId = model.Id });
            }
            TempData[AlertConstants.ErrorHeader] = "Lỗi";
            TempData[AlertConstants.ErrorMessage] = "Xoá không thành công";
            return View();
        }

    }
}