using AutoMapper;
using DataModels.Dto;
using DataModels.EF;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebAnime.MVC.Areas.Admin.Models;

namespace WebAnime.MVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class EpisodeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ServerDto _serverDto;
        private readonly EpisodeDto _episodeDto;
        private readonly AnimeDto _animeDto;
        public EpisodeController(IMapper mapper, ServerDto serverDto, EpisodeDto episodeDto, AnimeDto animeDto)
        {
            _mapper = mapper;
            _serverDto = serverDto;
            _episodeDto = episodeDto;
            _animeDto = animeDto;
        }

        public async Task<ActionResult> Index(int animeId, int serverId)
        {
            var firstServer = await _serverDto.GetFirst();
            if (firstServer == null) return HttpNotFound();

            await LoadEditData(animeId);
            ViewBag.Servers = await _serverDto.GetAll();

            var episodeViewModelList = _mapper.Map<IEnumerable<Episodes>, IEnumerable<EpisodeViewModel>>(await _episodeDto.GetAll(animeId, serverId));
            ViewBag.ServerId = serverId;

            return View(episodeViewModelList);
        }

        [HttpGet]
        public async Task<ActionResult> Create(int animeId, int serverId)
        {
            var server = await _serverDto.GetById(serverId);
            ViewBag.ServerName = server.Name;
            await LoadEditData(animeId);

            return View(new EpisodeViewModel()
            {
                Id = 0,
                AnimeId = animeId,
                ServerId = serverId,
                SortOrder = await _episodeDto.GetMaxOrderId(animeId, serverId) + 1
            });
        }

        [HttpPost]
        public async Task<ActionResult> Create(EpisodeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var episode = _mapper.Map<Episodes>(model);
                if (await _episodeDto.Add(episode))
                {
                    return RedirectToAction("Index", "Episode", new { area = "Admin", animeId = model.AnimeId, serverId = model.ServerId });
                }
                ModelState.AddModelError(string.Empty, @"Lỗi không thêm được, vui lòng thử lại");
            }
            ModelState.AddModelError(string.Empty, @"Đầu vào lỗi, vui lòng kiểm tra lại");
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            var episode = await _episodeDto.GetById(id);
            if (episode == null) return HttpNotFound();
            var episodeViewModel = _mapper.Map<EpisodeViewModel>(episode);
            await LoadEditData(episodeViewModel.AnimeId);
            ViewBag.Server = await _serverDto.GetById(episodeViewModel.ServerId);
            return View(episodeViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Update(EpisodeViewModel model)
        {
            await LoadEditData(model.AnimeId);

            if (ModelState.IsValid)
            {
                var episode = _mapper.Map<Episodes>(model);
                if (await _episodeDto.Update(episode))
                {
                    return RedirectToAction("Index", "Episode", new { area = "Admin", animeId = episode.AnimeId, serverId = episode.ServerId });
                }
                ModelState.AddModelError(string.Empty, @"Lỗi không cập nhật được, vui lòng thử lại");
            }
            ModelState.AddModelError(string.Empty, @"Đầu vào lỗi, vui lòng kiểm tra lại");
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var episode = await _episodeDto.GetById(id);
            if (episode == null) return HttpNotFound();
            var episodeViewModel = _mapper.Map<EpisodeViewModel>(episode);
            await LoadEditData(episodeViewModel.AnimeId);
            ViewBag.Server = await _serverDto.GetById(episodeViewModel.ServerId);
            return View(episodeViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(EpisodeViewModel model)
        {
            var deleted = await _episodeDto.GetById(model.Id);
            var animeId = deleted.AnimeId;
            var serverId = deleted.ServerId;

            if (await _episodeDto.Delete(model.Id))
            {
                return RedirectToAction("Index", "Episode", new { area = "Admin", animeId = animeId, serverId });
            }
            ModelState.AddModelError(string.Empty, @"Lỗi không xoá được, vui lòng thử lại");
            return View();
        }

        async Task LoadEditData(int animeId)
        {
            ViewBag.Anime = await _animeDto.GetById(animeId);
        }


    }
}