using AutoMapper;
using DataModels.EF;
using DataModels.Repository.Interface;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using ViewModels.Client;
using WebAnime.MVC.Components;

namespace WebAnime.MVC.Controllers
{
    public class AnimeController(
        IAnimeRepository animeRepository,
        IRatingRepository ratingRepository,
        ICommentRepository commentRepository,
        IMapper mapper)
        : Controller
    {
        public async Task<ActionResult> Detail(int id)
        {
            var anime = await animeRepository.GetAnimeDetail(id);
            return await Task.FromResult(View(anime));
        }

        [ChildActionOnly]
        public async Task<ActionResult> TrendingPartial()
        {
            var animeTrending = await animeRepository.GetAnimeTrending();
            return PartialView(animeTrending);
        }

        [ChildActionOnly]
        public async Task<ActionResult> RecenlyPartial()
        {
            var animeRecenly = await animeRepository.GetAnimeRecenly();
            return await Task.FromResult(PartialView(animeRecenly));

        }

        public async Task<ActionResult> Watch(int id)
        {
            var model = await animeRepository.GetAnimeWatching(id);

            return await Task.FromResult(View(model));
        }

        public async Task<ActionResult> Rate(int animeId, int userId, float ratePoint)
        {
            if (animeId <= 0 || userId <= 0 || ratePoint <= 0) return HttpNotFound("Error");

            var result = await ratingRepository.Create(new Ratings()
            {
                AnimeId = animeId,
                UserId = userId,
                RatePoint = ratePoint
            });

            var jsonResult = new JsonResult()
            {
                Data = new
                {
                    success = result
                }
            };

            return await Task.FromResult(jsonResult);
        }

        public async Task<ActionResult> Comment(CommentViewModel model)
        {
            if (model.AnimeId <= 0 || model.CreatedBy <= 0) return HttpNotFound("Error");

            var comment = mapper.Map<Comments>(model);
            var result = await commentRepository.Comment(comment);

            if (String.IsNullOrEmpty(result.AvatarUrl)) result.AvatarUrl = CommonConstants.DefaultAvatarUrl;
            if (String.IsNullOrEmpty(result.UserFullName)) result.UserFullName = "Không biết";

            if (String.IsNullOrEmpty(result.EpisodeTitle))
            {
                result.EpisodeTitle = "";
            }
            else
            {
                if (!(result.EpisodeTitle.Contains("Tập") || result.EpisodeTitle.Contains("Ep")))
                {
                    result.EpisodeTitle = "Tập "+ result.EpisodeTitle;
                }
            }
            var json = new JsonResult()
            {
                Data = new
                {
                    data = result
                }
            };
            return json;
        }

        [HttpPost]
        public async Task<ActionResult> IncreaseView(int id)
        {
            bool result = await animeRepository.IncreaseView(id);
            var json = new JsonResult()
            {
                Data = new
                {
                    success = result
                }
            };
            return json;
        }

        [HttpGet]
        public async Task<ActionResult> AdvancedSearch(AnimeSearchViewModel model)
        {
            model.CategoryIds ??= new int[] { };
            var result = await animeRepository.AdvanceSearch(model);
            

            return await Task.FromResult(
                Json(new
                    {
                        data = result.Data,
                        result.TotalPages,
                        result.PageSize,
                        result.PageNumber
                    }, 
                    JsonRequestBehavior.AllowGet)
                );
        }

    }
}