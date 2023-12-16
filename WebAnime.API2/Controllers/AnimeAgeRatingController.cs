using DataModels.Repository.Interface;
using System.Linq;

using System.Threading.Tasks;
using System.Web.Http;

namespace WebAnime.API2.Controllers
{
    [Authorize]
    public class AnimeAgeRatingController : ApiController
    {
        private readonly IAgeRatingRepository _ageRatingRepository;

        public AnimeAgeRatingController(IAgeRatingRepository ageRatingRepository)
        {
            _ageRatingRepository = ageRatingRepository;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            var data = await _ageRatingRepository.GetAll();
            return Ok(data.Select(x => new { x.Id, x.Name }));
        }
    }
}
