using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using DataModels.EF.Identity;
using DataModels.Helpers;
using DataModels.Repository.Interface;
using ViewModels.API;

namespace WebAnime.API2.Controllers
{
    [RoutePrefix("api/Anime")]
    [Authorize]

    public class AnimeController : ApiController
    {
        private readonly IAnimeRepository _animeRepository;
        private readonly UserManager _userManager;

        public AnimeController(IAnimeRepository animeRepository, UserManager userManager)
        {
            _animeRepository = animeRepository;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("Random")]
        public async Task<IHttpActionResult> GetRandom()
        {
            var data = await _animeRepository.GetRandomAPI();
            var result = data.Include(x => x.Ratings).Select(x =>
                new
                {
                    x.Id,
                    x.Poster,
                    Rating = x.Ratings.Any()
                        ? Math.Round(x.Ratings.Sum(t => t.RatePoint) / (x.Ratings.Count * 1.0), 2)
                        : 0
                }
            );
            return Ok(result);
        }

        [HttpGet]
        [Route("Hit/{take?}")]
        public async Task<IHttpActionResult> HotAnime(int take = 10)
        {
            var check = int.TryParse(ClaimsPrincipal.Current.Claims
                .FirstOrDefault(x => x.Type == ClaimTypes.Sid)?.Value, out var userId);

            if (!check)
            {
                return Unauthorized();
            }

            var user = await _userManager.FindByIdAsync(userId);

            var data = await _animeRepository.GetHotAnimesAPI(take);
            var result = data.Select(x => new
            {
                x.Id,
                x.Title,
                x.Release?.Year,
                Country = x.Countries.Name,
                Categories = string.Join(",", x.Categories.Select(c => c.Name)),
                x.Poster,
                Rating = x.Ratings.Sum(y => y.RatePoint) / x.Ratings.Count(),
                IsFavorite = user.Favorites.Where(f => !f.IsDeleted).Any(f => f.AnimeId == x.Id)
            });

            return await Task.FromResult(Ok(result));
        }

        [HttpGet]
        [Route("NewEpisodeRelease")]
        public async Task<IHttpActionResult> NewEpisodeRelease(int pageNumber = 1, int pageSize = 6)
        {
            var wrapper = await _animeRepository.GetNewEpisodesReleaseAPI(pageNumber, pageSize);

            var result = wrapper.Data.Select(x => new
            {
                x.Id,
                x.Poster,
                Rating = x.Ratings.Sum(y => y.RatePoint) / x.Ratings.Count(),
                CurrentEpisode = x.Episodes
                                .Where(e => !e.IsDeleted)
                                .GroupBy(g => g.ServerId)
                                .Select(g => new { g.Key, CurrrentEpisodes = g.Count() })
                                .Max(g => g.CurrrentEpisodes),
            });

            return await Task.FromResult(Ok(new
            {
                data = result

            }));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetDetail(int id)
        {
            var check = int.TryParse(ClaimsPrincipal.Current.Claims
                .FirstOrDefault(x => x.Type == ClaimTypes.Sid)?.Value, out var userId);

            if (!check)
            {
                return Unauthorized();
            }

            var user = await _userManager.FindByIdAsync(userId);

            var anime = await _animeRepository.GetById(id);
            if (anime == null) return BadRequest("Cannot find anime");

            return Ok(new
            {
                anime.Id,
                anime.Title,
                anime.Poster,
                Rating = anime.Ratings.Any()
                    ? Math.Round(anime.Ratings.Sum(t => t.RatePoint) / (anime.Ratings.Count * 1.0), 2)
                    : 0,
                anime.Release?.Year,
                Country = anime.Countries.Name,
                AgeRating = anime.AgeRatings.Name,
                Categories = anime.Categories.Select(x => x.Name),
                anime.Synopsis,
                Episodes = anime.Episodes.Where(x => x.ServerId == AnimeConstants.MobileServerId && !x.IsDeleted)
                        .OrderBy(x => x.SortOrder)
                        .ThenByDescending(x => x.CreatedDate)
                        .Select(x => new
                        {
                            x.Id,
                            x.Title,
                            x.Url
                        }),
                IsFavorite = user.Favorites.Where(x => !x.IsDeleted).Any(x => x.AnimeId == id),

            });
        }


        [HttpPut]
        [Route("Search")]
        public async Task<IHttpActionResult> SearchAnime(AnimeSearchViewModel model)
        {
            var data = await _animeRepository.SearchAPI(model);
            return Ok(data.Select(x => new
            {
                x.Id,
                x.Title,
                x.Poster,
            }));
        }
    }
}
