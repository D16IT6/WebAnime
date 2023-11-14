using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using DataModels.Repository.Interface;
using WebAnime.MVC.Components;

namespace WebAnime.MVC.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;

        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        [HttpGet]
        public async Task<JsonResult> LoadComment(int animeId, int pageNumber, int pageSize)
        {
            if (pageSize == 0) pageSize = CommonConstants.AnimeCommentPageSize;

            if (pageNumber <= 0) pageNumber = 1;

            var commentPage = await _commentRepository.GetPaging(animeId, pageNumber, pageSize);

            var jsonResult = new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new
                {
                    data= commentPage.Select(x =>new
                    {
                        AvatarUrl = x.AvatarUrl ?? CommonConstants.DefaultAvatarUrl,
                        x.Content,
                        CreatedDate = x.CreatedDate!.Value.ToString("dd/MM/yyyy - hh:mm:ss"),
                        UserFullName = x.UserFullName ?? "Khách vãng lai"
                    })
                }
            };

            return jsonResult;
        }
    }
}