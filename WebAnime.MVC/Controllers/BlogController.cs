using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using DataModels.EF;
using DataModels.EF.Identity;
using DataModels.Repository.Interface;
using ViewModels.Admin;
using ViewModels.Client;
using WebAnime.MVC.Components;

namespace WebAnime.MVC.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IBlogCommentRepository _blogCommentRepository;
        private readonly IMapper _mapper;
        private readonly UserManager _userManager;

        public BlogController(IBlogRepository blogRepository, IMapper mapper, IBlogCommentRepository blogCommentRepository, UserManager userManager)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
            _blogCommentRepository = blogCommentRepository;
            _userManager = userManager;
        }

        public async Task<ActionResult> Index()
        {
            var blogList = await _blogRepository.GetAll();
            var blogLitteListViewModel = _mapper.Map<IEnumerable<Blogs>, IEnumerable<BlogLitteViewModel>>(blogList);
            return View(blogLitteListViewModel);
        }

        public async Task<ActionResult> Detail(int id)
        {
            var blogViewModel = await _blogRepository.GetBlogViewModel(id);
            return View(blogViewModel);
        }


        [HttpPost]
        public async Task<JsonResult> Comment(BlogCommentViewModel model)
        {
            var blogComment = _mapper.Map<BlogComments>(model);
            var result = await _blogCommentRepository.Comment(blogComment);
            var jsonResult = new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

            var commentUser = await _userManager.FindByIdAsync(model.CreatedBy);

            var afterComment = await _blogCommentRepository.GetById(result);
            jsonResult.Data = new
            {
                status = result,
                avatarUrl = commentUser.AvatarUrl ?? CommonConstants.DefaultAvatarUrl,
                commentDate = afterComment.CreatedDate?.ToString("dd/MM/yyyy - HH:mm:ss") ?? DateTime.Now.ToString("dd/MM/yyyy - HH:mm:ss"),
                content = result > 0 ? blogComment.Content : "Lỗi"
            };
            return jsonResult;
        }
    }
}