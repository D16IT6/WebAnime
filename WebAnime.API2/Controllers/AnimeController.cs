using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using DataModels.Repository.Interface;
namespace WebAnime.API2.Controllers
{
    [RoutePrefix("api/Anime")]
    public class AnimeController : ApiController
    {
        private readonly IAnimeRepository _animeRepository;
        private readonly ICommentRepository _commentRepository;

        public AnimeController(IAnimeRepository animeRepository, ICommentRepository commentRepository)
        {
            _animeRepository = animeRepository;
            _commentRepository = commentRepository;
        }

        [HttpGet]
        [Route("Hit/{take?}")]
        public async Task<IHttpActionResult> HotAnime(int take = 10)
        {
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
            });

            return await Task.FromResult(Ok(result));
        }

        [HttpGet]
        [Route("NewEpisodeRelease/PageNumber/{pageNumber}/PageSize/{pageSize}")]
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
                data = result,
                wrapper.TotalPages
            }));
        }

        [HttpGet]
        [Route("{id}/Comment/PageNumber/{pageNumber}/PageSize/{pageSize}")]
        public async Task<IHttpActionResult> GetAllComment(int id, int pageNumber, int pageSize = 10)
        {
            var data = await _commentRepository.GetPaging(id, pageNumber, pageSize);
            return Ok(data);
        }
    }
}
