using AutoMapper;
using DataModels.Dto;
using DataModels.EF;
using System.Collections.Generic;
using System.Web.Mvc;
using WebAnime.MVC.Areas.Admin.Models;

namespace WebAnime.MVC.Areas.Admin.Controllers
{
    public class ServerController : Controller
    {
        private readonly IMapper _mapper;
        public ServerController(IMapper mapper)
        {
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult Index()
        {
            var serverDto = new ServerDto();
            var serverViewModelList =
                _mapper.Map<IEnumerable<Servers>, IEnumerable<ServerViewModel>>(serverDto.GetAll());
            return View(serverViewModelList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(ServerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var serverDto = new ServerDto();
                var server = _mapper.Map<Servers>(model);
                if (serverDto.Add(server))
                {
                    return RedirectToAction("Index");
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
            var serverDto = new ServerDto();
            var serverViewModel = _mapper.Map<ServerViewModel>(serverDto.GetById(id));
            if (serverViewModel == null) return HttpNotFound();

            return View(serverViewModel);
        }

        [HttpPost]
        public ActionResult Update(ServerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var serverDto = new ServerDto();
                var server = _mapper.Map<Servers>(model);
                if (serverDto.Update(server))
                {
                    return RedirectToAction("Index");
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
            var serverDto = new ServerDto();
            var serverViewModel = _mapper.Map<ServerViewModel>(serverDto.GetById(id));
            if (serverViewModel == null) return HttpNotFound();
            return View(serverViewModel);
        }
        [HttpPost]
        public ActionResult Delete(ServerViewModel model)
        {
            var serverDto = new ServerDto();
            if (serverDto.Delete(model.Id))
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", @"Lỗi không xoá được, vui lòng thử lại");
            return View(); ;
        }
    }
}