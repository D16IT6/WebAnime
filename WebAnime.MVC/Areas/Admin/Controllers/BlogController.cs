using AutoMapper;
using DataModels.Dto;
using DataModels.EF;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebAnime.MVC.Areas.Admin.Models;
using WebAnime.MVC.Helpers;
using WebAnime.MVC.Helpers.Session;

namespace WebAnime.MVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class BlogController : Controller
    {
        private readonly BlogCategoryDto _blogCategoryDto;
        private readonly BlogDto _blogDto;
        private readonly IMapper _mapper;

        public BlogController(BlogCategoryDto blogCategoryDto, BlogDto blogDto, IMapper mapper)
        {
            _blogCategoryDto = blogCategoryDto;
            _blogDto = blogDto;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var blogList = await _blogDto.GetAll();
            var blogListViewModel = _mapper.Map<IEnumerable<Blogs>, IEnumerable<BlogViewModel>>(blogList);

            return await Task.FromResult<ActionResult>(View(blogListViewModel));
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            await LoadDependencies();
            return await Task.FromResult(View());
        }
        [HttpPost]
        public async Task<ActionResult> Create(BlogViewModel model)
        {
            var userSession = Session[SessionConstants.UserLogin] as UserSession;
            model.CreatedBy = userSession?.Id ?? int.Parse(User.Identity.GetUserId());
            var blog = _mapper.Map<Blogs>(model);
            var result = await _blogDto.Create(blog);
            if (result)
            {
                return RedirectToAction("Index");
            }
            return await Task.FromResult(View(model));
        }

        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            await LoadDependencies();

            var blog = await _blogDto.GetById(id);
            var blogViewModel = _mapper.Map<BlogViewModel>(blog);

            return await Task.FromResult(View(blogViewModel));
        }
        [HttpPost]
        public async Task<ActionResult> Update(BlogViewModel model)
        {
            await LoadDependencies();

            if (ModelState.IsValid)
            {
                var userSession = Session[SessionConstants.UserLogin] as UserSession;
                model.CreatedBy = userSession?.Id ?? int.Parse(User.Identity.GetUserId());
                var blog = _mapper.Map<Blogs>(model);
                var result = await _blogDto.Update(blog);
                if (result)
                {
                    return RedirectToAction("Index");

                }
            }

            return await Task.FromResult(View(model));
        }
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            await LoadDependencies();
            var blogViewModel = _mapper.Map<BlogViewModel>(await _blogDto.GetById(id));

            return await Task.FromResult(View(blogViewModel));
        }

        [HttpPost]
        public async Task<ActionResult> Delete(BlogViewModel model)
        {
            var userSession = Session[SessionConstants.UserLogin] as UserSession;
            var deletedBy = userSession?.Id ?? int.Parse(User.Identity.GetUserId());
            bool result = await _blogDto.Delete(model.Id, deletedBy);
            if (result)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", @"Lỗi xóa, vui lòng thử lại");
            return View(model);
        }

        async Task LoadDependencies()
        {
            var blogCategories = await _blogCategoryDto.GetAll();
            ViewBag.BlogCategories = blogCategories;
        }
    }
}