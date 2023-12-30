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
    [AdminAreaAuthorize]
    public class EpisodeController(
        IMapper mapper,
        IServerRepository serverRepository,
        IEpisodeRepository episodeRepository,
        IAnimeRepository animeRepository)
        : Controller
    {
        public async Task<ActionResult> Index(int animeId, int serverId)
        {
            var firstServer = await serverRepository.GetFirst();
            if (firstServer == null) return HttpNotFound();

            await LoadEditData(animeId);
            ViewBag.Servers = await serverRepository.GetAll();

            var episodeViewModelList = mapper.Map<IEnumerable<Episodes>, IEnumerable<EpisodeViewModel>>(await episodeRepository.GetAll(animeId, serverId));
            ViewBag.ServerId = serverId;

            return View(episodeViewModelList);
        }

        [HttpGet]
        public async Task<ActionResult> Create(int animeId, int serverId)
        {
            var server = await serverRepository.GetById(serverId);
            ViewBag.ServerName = server.Name;
            await LoadEditData(animeId);

            return View(new EpisodeViewModel()
            {
                Id = 0,
                AnimeId = animeId,
                ServerId = serverId,
                SortOrder = await episodeRepository.GetMaxOrderId(animeId, serverId) + 1
            });
        }
        [HttpPost]
        public async Task<ActionResult> CreateMultiple(List<EpisodeViewModel> model)
        {
            var episodeList = mapper.Map<List<EpisodeViewModel>, List<Episodes>>(model);

            var result = await episodeRepository.AddRange(episodeList);

            return Json(new
            {
                success = result
            });

        }

        [HttpPost]
        public async Task<ActionResult> Create(EpisodeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var episode = mapper.Map<Episodes>(model);
                episode.CreatedBy = User.Identity.GetUserId<int>();

                if (await episodeRepository.Create(episode))
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
            var episode = await episodeRepository.GetById(id);
            if (episode == null) return HttpNotFound();
            var episodeViewModel = mapper.Map<EpisodeViewModel>(episode);
            await LoadEditData(episodeViewModel.AnimeId);
            ViewBag.Server = await serverRepository.GetById(episodeViewModel.ServerId);
            return View(episodeViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Update(EpisodeViewModel model)
        {
            await LoadEditData(model.AnimeId);

            if (ModelState.IsValid)
            {
                var episode = mapper.Map<Episodes>(model);
                episode.ModifiedBy = User.Identity.GetUserId<int>();

                if (await episodeRepository.Update(episode))
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
            var episode = await episodeRepository.GetById(id);
            if (episode == null) return HttpNotFound();
            var episodeViewModel = mapper.Map<EpisodeViewModel>(episode);
            await LoadEditData(episodeViewModel.AnimeId);
            ViewBag.Server = await serverRepository.GetById(episodeViewModel.ServerId);
            return View(episodeViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(EpisodeViewModel model)
        {

            var deleted = await episodeRepository.GetById(model.Id);
            var animeId = deleted.AnimeId;
            var serverId = deleted.ServerId;

            int deletedBy = User.Identity.GetUserId<int>();

            if (await episodeRepository.Delete(model.Id, deletedBy))
            {
                return RedirectToAction("Index", "Episode", new { area = "Admin", animeId, serverId });
            }
            ModelState.AddModelError(string.Empty, @"Lỗi không xoá được, vui lòng thử lại");
            return View();
        }

        async Task LoadEditData(int animeId)
        {
            ViewBag.Anime = await animeRepository.GetById(animeId);
        }
    }
}