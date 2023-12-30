using DataModels.Repository.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebAnime.MVC.Components;

namespace WebAnime.MVC.Controllers
{
    public class CommentController(ICommentRepository commentRepository) : Controller
    {
        [HttpGet]
        public async Task<JsonResult> LoadComment(int animeId, int pageNumber, int pageSize)
        {
            if (pageSize == 0) pageSize = CommonConstants.AnimeCommentPageSize;

            if (pageNumber <= 0) pageNumber = 1;

            var commentPage = await commentRepository.GetPaging(animeId, pageNumber, pageSize);

            var jsonResult = new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new
                {
                    data = commentPage.Select(x =>
                    {
                        if (string.IsNullOrEmpty(x.AvatarUrl)) x.AvatarUrl = CommonConstants.DefaultAvatarUrl;
                        if (string.IsNullOrEmpty(x.UserFullName)) x.UserFullName = "Không biết";

                        if (string.IsNullOrEmpty(x.EpisodeTitle))
                        {
                            x.EpisodeTitle = "";
                        }
                        else
                        {
                            if (!(x.EpisodeTitle.Contains("Tập") || x.EpisodeTitle.Contains("Ep")))
                            {
                                x.EpisodeTitle = "Tập " + x.EpisodeTitle;
                            }
                        }
                        return new
                        {
                            x.AnimeId,
                            x.AvatarUrl,
                            x.UserFullName,
                            x.EpisodeTitle,
                            x.Content,
                            x.Id,
                            CreatedDate = x.CreatedDate?.ToString("dd/MM/yyyy - HH:mm:ss") ?? DateTime.Now.ToString("dd/MM/yyyy - HH:mm:ss")
                        };
                    })
                }
            };

            return jsonResult;
        }
    }
}