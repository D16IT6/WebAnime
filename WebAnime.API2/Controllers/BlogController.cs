using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using DataModels.Repository.Interface;

namespace WebAnime.API2.Controllers
{
    public class BlogController : ApiController
    {
        private readonly IBlogRepository _blogRepository;

        public BlogController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<IHttpActionResult> GetAllBlog()
        {
            var data = await _blogRepository.GetAll();
            return Ok(new
            {
                data = data.Select(x => new
                {
                    x.Id,
                    x.Title,
                    x.Slug,
                    //x.Content,
                    x.CreatedDate
                })
            });
        }
    }
}