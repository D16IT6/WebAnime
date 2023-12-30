using AutoMapper;
using DataModels.EF;
using DataModels.Repository.Interface;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Org.BouncyCastle.Math.EC.Rfc7748;
using ViewModels.Admin;
using WebAnime.MVC.Components;

namespace WebAnime.MVC.Areas.Admin.Controllers
{
    [AdminAreaAuthorize]
    public class BlogController(
        IBlogCategoryRepository blogCategoryRepository,
        IBlogRepository blogRepository,
        IMapper mapper)
        : Controller
    {
        [HttpGet]
        public async Task<ActionResult> Index()
        {

            var blogList = await blogRepository.GetAll();
            var blogListViewModel = mapper.Map<IEnumerable<Blogs>, IEnumerable<BlogViewModel>>(blogList);

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
            model.CreatedBy = User.Identity.GetUserId<int>();
            var blog = mapper.Map<Blogs>(model);
            var result = await blogRepository.Create(blog);
            if (result)
            {
                TempData[AlertConstants.SuccessMessage] = "Thêm mới bài viết thành công!";
                return RedirectToAction("Index");
            }

            TempData[AlertConstants.ErrorMessage] = "Có lỗi xảy ra, vui lòng thử lại";
            return await Task.FromResult(View(model));
        }

        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            await LoadDependencies();

            var blog = await blogRepository.GetById(id);
            var blogViewModel = mapper.Map<BlogViewModel>(blog);

            return await Task.FromResult(View(blogViewModel));
        }
        [HttpPost]
        public async Task<ActionResult> Update(BlogViewModel model)
        {
            await LoadDependencies();

            if (ModelState.IsValid)
            {
                model.ModifiedBy = User.Identity.GetUserId<int>();
                var blog = mapper.Map<Blogs>(model);
                var result = await blogRepository.Update(blog);
                if (result)
                {
                    TempData[AlertConstants.SuccessMessage] = "Cập nhật bài viết thành công!";
                    return RedirectToAction("Index");

                }
            }

            TempData[AlertConstants.ErrorMessage] = "Có lỗi xảy ra, vui lòng thử lại";
            return await Task.FromResult(View(model));
        }
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            await LoadDependencies();
            var blogViewModel = mapper.Map<BlogViewModel>(await blogRepository.GetById(id));

            return await Task.FromResult(View(blogViewModel));
        }

        [HttpPost]
        public async Task<ActionResult> Delete(BlogViewModel model)
        {
            var deletedBy = User.Identity.GetUserId<int>();
            bool result = await blogRepository.Delete(model.Id, deletedBy);
            if (result)
            {
                TempData[AlertConstants.SuccessMessage] = "Xóa bài viết thành công";
                return RedirectToAction("Index");
            }

            TempData[AlertConstants.SuccessMessage] = "Có lỗi xảy ra, vui lòng thử lại";
            ModelState.AddModelError(string.Empty, @"Lỗi xóa, vui lòng thử lại");
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> GetPaging(string searchTitle, int pageNumber, int pageSize)
        {
            var queryResult = await blogRepository.GetPaging(searchTitle, pageSize, pageNumber);
            var result = queryResult.Data
                .Select(
                    x => new
                    {
                        x.Title,
                        x.Content,
                        x.ImageUrl,
                        x.Id
                    });

            if (result == null)
                return HttpNotFound();

            return Json(new
            {
                data=result,
                queryResult.TotalPages,
            },JsonRequestBehavior.AllowGet);
        }

        async Task LoadDependencies()
        {
            var blogCategories = await blogCategoryRepository.GetAll();
            ViewBag.BlogCategories = blogCategories;
        }

        
    }
}