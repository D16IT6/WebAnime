using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

using DataModels.EF.Identity;
using DataModels.Repository.Interface;
using ViewModels.API;

namespace WebAnime.API2.Controllers
{
    [RoutePrefix("api/Comment")]
    [Authorize]
    public class CommentController : ApiController
    {
        private readonly ICommentRepository _commentRepository;
        private readonly UserManager _userManager;

        public CommentController(ICommentRepository commentRepository, UserManager userManager)
        {
            _commentRepository = commentRepository;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllComments(int animeId)
        {
            var data = await _commentRepository.GetAllAPI(animeId);
            return Ok(data.OrderByDescending(x => x.CreatedDate).Select(x =>
                new
                {
                    x.Id,
                    x.Content,
                    x.User.FullName,
                    x.User.AvatarUrl,
                    x.CreatedDate,
                    x.ParentId
                }
            ));
        }

        [HttpPut]
        [Authorize]
        public async Task<IHttpActionResult> PutNewComment(CommentPutViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest("Dữ liệu vào không hợp lệ");
            var comment = await _commentRepository.CommentAPI(model);
            if (comment == null)
                return BadRequest("Không thể bình luận, vui lòng thử lại");

            var user = await _userManager.FindByIdAsync(comment.CreatedBy);
            return Ok(new
            {
                Message = "Bình luận thành công",
                Data = new
                {
                    comment.Id,
                    comment.Content,
                    user.FullName,
                    user.AvatarUrl,
                    comment.CreatedDate
                }
            });
        }
    }
}
