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
        public ActionResult Index(int animeId)
        {
            LoadEditData(animeId);

            var episodeDto = new EpisodeDto();
            var episodeViewModelList =
                _mapper.Map<IEnumerable<Episodes>, IEnumerable<EpisodeViewModel>>(episodeDto.GetAll(animeId));

            return View(episodeViewModelList);
        }

        [HttpGet]
        public ActionResult Create(int animeId)
        {
            LoadEditData(animeId);
            return View();
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
                    return RedirectToAction("Index", "Episode", new { area = "Admin", animeId = model.AnimeId });
                }

                ModelState.AddModelError("", @"Lỗi không thêm được, vui lòng thử lại");
                return View();
            }
            ModelState.AddModelError("", @"Đầu vào lỗi, vui lòng thử lại");
            return View();
        }
        [HttpGet]
        public ActionResult Update(int animeId, int serverId, int order)
        {
            LoadEditData(animeId);
            var episodeDto = new EpisodeDto();
            var episode = episodeDto.GetById(animeId, serverId, order);
            if (episode == null) return new HttpNotFoundResult();
            var episodeViewModel = _mapper.Map<EpisodeViewModel>(episode);
            return View(episodeViewModel);
        }

        [HttpPost]
        public ActionResult Update(EpisodeViewModel model)
        {
            var episodeDto = new EpisodeDto();

            if (ModelState.IsValid)
            {
                var episode = _mapper.Map<Episodes>(model);
                if (episodeDto.Update(episode))
                {
                    return RedirectToAction("Index", "Episode", new { area = "Admin", animeId = model.AnimeId });
                }

                ModelState.AddModelError("", @"Lỗi không cập nhật được, vui lòng thử lại");
                LoadEditData(model.AnimeId);
                return View();
            }
            ModelState.AddModelError("", @"Đầu vào lỗi, vui lòng thử lại");
            LoadEditData(model.AnimeId);
            return View();
        }

        [HttpGet]
        public ActionResult Delete(int animeId, int serverId, int order)
        {

            return View();
        }
        private void LoadEditData(int animeId)
        {
            var serverDto = new ServerDto();
            ViewBag.Servers = serverDto.GetAll();

            var animeDto = new AnimeDto();
            ViewBag.Anime = animeDto.GetById(animeId);
        }



    }
}