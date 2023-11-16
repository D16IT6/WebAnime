using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using DataModels.EF;
using DataModels.Repository.Interface;
using ViewModels.Client;
using WebAnime.MVC.Components;

namespace WebAnime.MVC.Controllers
{
    public class AnimeController : Controller
    {
        private readonly IAnimeRepository _animeRepository;
        private readonly IRatingRepository _ratingRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public AnimeController(IAnimeRepository animeRepository, IRatingRepository ratingRepository, ICommentRepository commentRepository, IMapper mapper)
        {
            _animeRepository = animeRepository;
            _ratingRepository = ratingRepository;
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index()
        {
            return await Task.FromResult(View());
        }

        public async Task<ActionResult> Detail(int id)
        {
            var anime = await _animeRepository.GetAnimeDetail(id);
            return await Task.FromResult(View(anime));
        }

        [ChildActionOnly]
        public async Task<ActionResult> TrendingPartial()
        {
            var animeTrending = await _animeRepository.GetAnimeTrending();
            return await Task.FromResult(PartialView(animeTrending));
        }

        [ChildActionOnly]
        public async Task<ActionResult> RecenlyPartial()
        {
            var animeRecenly = await _animeRepository.GetAnimeRecenly();
            return await Task.FromResult(PartialView(animeRecenly));

        }

        public ActionResult Watch(int id)
        {
            return View();
        }

        public async Task<ActionResult> Rate(int animeId, int userId, float ratePoint)
        {
            if (animeId <= 0 || userId <= 0 || ratePoint <= 0) return HttpNotFound("Error");

            var result = await _ratingRepository.Create(new Ratings()
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

            var comment = _mapper.Map<Comments>(model);
            var result = await _commentRepository.Comment(comment);

            if (String.IsNullOrEmpty(result.AvatarUrl)) result.AvatarUrl = CommonConstants.DefaultAvatarUrl;
            if (String.IsNullOrEmpty(result.UserFullName)) result.UserFullName = "Không biết";

            var json = new JsonResult()
            {
                Data = new
                { 
                    data = result
                }
            };
            return json;
        }

    }
}