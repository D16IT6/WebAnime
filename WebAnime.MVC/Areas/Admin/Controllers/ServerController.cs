using AutoMapper;
using DataModels.EF;
using DataModels.Repository.Interface;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using ViewModels.Admin;
using WebAnime.MVC.Components;

namespace WebAnime.MVC.Areas.Admin.Controllers
{
    [OnlyAdminAuthorize]
    public class ServerController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IServerRepository _serverRepository;

        public ServerController(IMapper mapper, IServerRepository serverRepository)
        {
            _mapper = mapper;
            _serverRepository = serverRepository;

        }
        [HttpGet]
        public async Task<ActionResult> Index()
        {

            var serverViewModelList =
                _mapper.Map<IEnumerable<Servers>, IEnumerable<ServerViewModel>>(await _serverRepository.GetAll());
            return View(serverViewModelList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(ServerViewModel model)
        {
            if (ModelState.IsValid)
            {

                var server = _mapper.Map<Servers>(model);
                server.CreatedBy = User.Identity.GetUserId<int>();

                if (await _serverRepository.Create(server))
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, @"Lỗi không thêm được, vui lòng thử lại");
                return View();
            }
            ModelState.AddModelError(string.Empty, @"Đầu vào lỗi, vui lòng thử lại");
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {

            var serverViewModel = _mapper.Map<ServerViewModel>(await _serverRepository.GetById(id));
            if (serverViewModel == null) return HttpNotFound();

            return View(serverViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Update(ServerViewModel model)
        {
            if (ModelState.IsValid)
            {

                var server = _mapper.Map<Servers>(model);
                server.ModifiedBy = User.Identity.GetUserId<int>();

                if (await _serverRepository.Update(server))
                {
                    return RedirectToAction("Index");
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

            var serverViewModel = _mapper.Map<ServerViewModel>(await _serverRepository.GetById(id));
            if (serverViewModel == null) return HttpNotFound();
            return View(serverViewModel);
        }
        [HttpPost]
        public async Task<ActionResult> Delete(ServerViewModel model)
        {
            int deletedBy = User.Identity.GetUserId<int>();
            if (await _serverRepository.Delete(model.Id, deletedBy))
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, @"Lỗi không xoá được, vui lòng thử lại");
            return View(); ;
        }
    }
}