using System.Threading.Tasks;
using System.Web.Http;
using DataModels.Repository.Interface;

namespace WebAnime.API2.Controllers
{
    [RoutePrefix("API/Anime")]
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
        [Route("Trend/{take?}")]
        public async Task<IHttpActionResult> GetAnimeTrending(int take = 9)
        {
            if (take <= 0 || take > 9)
            {
                return BadRequest("Plase re-input value");
            }

            var data = await _animeRepository
                .GetAnimeTrending(take);

            return await Task.FromResult(Ok(data));
        }

        [HttpGet]
        [Route("Recenly/{take?}")]
        public async Task<IHttpActionResult> GetAnimeRecenly(int take = 9)
        {
            if (take <= 0 || take > 9)
            {
                return BadRequest("Plase re-input value");
            }

            var data = await _animeRepository
                .GetAnimeTrending(take);

            return await Task.FromResult(Ok(data));
        }

        [HttpGet]
        [Route("{id}/Comment/PageNumber/{pageNumber}/PageSize/{pageSize}")]
        public async Task<IHttpActionResult> GetAllComment(int id,int pageNumber,int pageSize = 10)
        {
            var data = await _commentRepository.GetPaging(id, pageNumber, pageSize);
            return Ok(data);
        }
    }
}
