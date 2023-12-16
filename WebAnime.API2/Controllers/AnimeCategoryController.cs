using DataModels.Repository.Interface;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebAnime.API2.Controllers
{
    [Authorize]

    public class AnimeCategoryController : ApiController
    {
        private readonly  ICategoryRepository _categoryRepository;

        public AnimeCategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            var data = await _categoryRepository.GetAll();
            return Ok(data.Select(x => new { x.Id, x.Name }));
        }
    }
}
