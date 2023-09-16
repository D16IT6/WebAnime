using AutoMapper;
using DataModels.Dto;
using DataModels.EF;
using System.Collections.Generic;
using System.Web.Mvc;
using WebAnime.MVC.Areas.Admin.Models;

namespace WebAnime.MVC.Areas.Admin.Controllers
{
    public class EpisodeController : Controller
    {
        private readonly IMapper _mapper;
        public EpisodeController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult Index(int id, int serverId)
        {
            var serverDto = new ServerDto();
            var firstServer = serverDto.GetFirst();
            if (firstServer == null) return new HttpNotFoundResult();
            LoadEditData(id);

            ViewBag.Servers = serverDto.GetAll();

            var episodeDto = new EpisodeDto();
            var episodeViewModelList =
                _mapper.Map<IEnumerable<Episodes>, IEnumerable<EpisodeViewModel>>(episodeDto.GetAll(id, serverId));

            ViewBag.ServerId = serverId;
            return View(episodeViewModelList);
        }
        [HttpPost]
        public ActionResult Index(int id, FormCollection form)
        {
            int animeId = id;
            var serverDto = new ServerDto();
            var firstServer = serverDto.GetFirst();
            if (firstServer == null) return new HttpNotFoundResult();

            LoadEditData(animeId);
            ViewBag.Servers = serverDto.GetAll();

            bool hasServerId = int.TryParse(form["ServerFilter"], out int serverId);
            if (!hasServerId && serverId != 0) serverId = firstServer.Id;
            var episodeDto = new EpisodeDto();
            var episodeViewModelList =
                _mapper.Map<IEnumerable<Episodes>, IEnumerable<EpisodeViewModel>>(episodeDto.GetAll(animeId, serverId));

            ViewBag.ServerId = serverId;
            return View(episodeViewModelList);
        }

        [HttpGet]
        public ActionResult Create(int id, int serverId)
        {
            int animeId = id;
            var episodeDto = new EpisodeDto();
            ViewBag.ServerName = episodeDto.GetById(animeId, serverId).Servers.Name;
            LoadEditData(id);
            return View(new EpisodeViewModel()
            {
                AnimeId = animeId,
                ServerId = serverId,
                Order = episodeDto.GetMaxOrderId(id, serverId) + 1
            });
        }

        [HttpPost]
        public ActionResult Create(EpisodeViewModel model)
        {
            var episodeDto = new EpisodeDto();

            if (ModelState.IsValid)
            {
                var episode = _mapper.Map<Episodes>(model);
                if (episodeDto.Add(episode))
                {
                    return RedirectToAction("Index", "Episode", new { area = "Admin", id = model.AnimeId, serverId = model.ServerId });
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
            var episodeDto = new EpisodeDto();

            var episode = episodeDto.GetById(id);
            if (episode == null) return new HttpNotFoundResult();
            var episodeViewModel = _mapper.Map<EpisodeViewModel>(episode);

            LoadEditData(episodeViewModel.AnimeId);
            ViewBag.Server = new ServerDto().GetById(episodeViewModel.ServerId);
            return View(episodeViewModel);
        }

        [HttpPost]
        public ActionResult Update(EpisodeViewModel model)
        {
            var episodeDto = new EpisodeDto();
            LoadEditData(model.AnimeId);

            if (ModelState.IsValid)
            {
                var episode = _mapper.Map<Episodes>(model);
                if (episodeDto.Update(episode))
                {
                    return RedirectToAction("Index", "Episode", new { area = "Admin", id = episode.AnimeId, serverId = episode.ServerId });
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
            var episodeDto = new EpisodeDto();
            var episode = episodeDto.GetById(id);
            if (episode == null) return new HttpNotFoundResult();
            var episodeViewModel = _mapper.Map<EpisodeViewModel>(episode);

            LoadEditData(episodeViewModel.AnimeId);
            ViewBag.Server = new ServerDto().GetById(episodeViewModel.ServerId);
            return View(episodeViewModel);
        }
        [HttpPost]
        public ActionResult Delete(EpisodeViewModel model)
        {
            var episodeDto = new EpisodeDto();
            var deleted = episodeDto.GetById(model.Id);
            var animeId = deleted.AnimeId;
            var serverId = deleted.ServerId;
            if (episodeDto.Delete(model.Id))
            {
                return RedirectToAction("Index", "Episode", new { area = "Admin", id = animeId, serverId });
            }
            ModelState.AddModelError("", @"Lỗi không xoá được, vui lòng thử lại");
            return View();

        }
        private void LoadEditData(int animeId)
        {

            var animeDto = new AnimeDto();
            ViewBag.Anime = animeDto.GetById(animeId);
        }



    }
}