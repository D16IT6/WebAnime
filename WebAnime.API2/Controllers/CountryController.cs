using DataModels.Repository.Interface;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebAnime.API2.Controllers
{
    [Authorize]
    public class CountryController : ApiController
    {
        private readonly ICountryRepository _countryRepository;

        public CountryController(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            var data = await _countryRepository.GetAll();
            return Ok(data.Select(x => new { x.Id, x.Name }));
        }
    }
}
