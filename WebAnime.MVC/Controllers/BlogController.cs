using AutoMapper;
using DataModels.EF;
using DataModels.EF.Identity;
using DataModels.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using ViewModels.Client;
using WebAnime.MVC.Components;

namespace WebAnime.MVC.Controllers
{
    public class BlogController(
        IBlogRepository blogRepository,
        IMapper mapper,
        IBlogCommentRepository blogCommentRepository,
        UserManager userManager)
        : Controller
    {
        public async Task<ActionResult> Index()
        {
            var blogList = await blogRepository.GetAll();
            var blogLitteListViewModel = mapper.Map<IEnumerable<Blogs>, IEnumerable<BlogLitteViewModel>>(blogList);
            return View(blogLitteListViewModel);
        }

        public async Task<ActionResult> Detail(int id)
        {
            var blogViewModel = await blogRepository.GetBlogViewModel(id);
            return View(blogViewModel);
        }


        [HttpPost]
        public async Task<JsonResult> Comment(BlogCommentViewModel model)
        {
            var blogComment = mapper.Map<BlogComments>(model);
            var result = await blogCommentRepository.Comment(blogComment);
            var jsonResult = new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

            var commentUser = await userManager.FindByIdAsync(model.CreatedBy);

            var afterComment = await blogCommentRepository.GetById(result);

            jsonResult.Data = new
            {
                status = result,
                avatarUrl = commentUser.AvatarUrl ?? CommonConstants.DefaultAvatarUrl,
                commentDate = afterComment.CreatedDate?.ToString("dd/MM/yyyy - HH:mm:ss") ?? DateTime.Now.ToString("dd/MM/yyyy - HH:mm:ss"),
                userFullName = commentUser.FullName,
                content = result > 0 ? blogComment.Content : "Lỗi"
            };
            return jsonResult;
        }
    }
}