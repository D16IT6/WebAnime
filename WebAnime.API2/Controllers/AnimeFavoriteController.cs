
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using DataModels.Repository.Interface;

namespace WebAnime.API2.Controllers
{
    [RoutePrefix("api/AnimeFavorite")]
    [Authorize]
    public class AnimeFavoriteController : ApiController
    {
        private readonly IAnimeFavoriteRepository _animeFavoriteRepository;

        public AnimeFavoriteController(IAnimeFavoriteRepository animeFavoriteRepository)
        {
            _animeFavoriteRepository = animeFavoriteRepository;
        }

        [Route("{userId:int}")]
        public async Task<IHttpActionResult> GetByUserId(int userId)
        {
            var wrapper = await _animeFavoriteRepository.GetByUserIdAPI(userId);
            var data = wrapper
                .OrderByDescending(x => x.CreatedDate)
                .Include(x => x.Anime)
                .Include(x => x.Anime.Ratings)
                .Select(x => new
                {
                    x.Anime.Id,
                    x.Anime.Title,
                    x.Anime.Poster,
                    Rating = x.Anime.Ratings.Any()
                        ? Math.Round(x.Anime.Ratings.Sum(t => t.RatePoint) / (x.Anime.Ratings.Count * 1.0), 2)
                        : 0,
                });

            return Ok(data);
        }
    }
}
