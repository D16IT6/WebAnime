using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels.EF;
using DataModels.Repository.Interface;

namespace DataModels.Repository.Implement.EF6
{
    public class BlogCategoryRepository : IBlogCategoryRepository
    {
        public WebAnimeDbContext Context { get; set; }
        public BlogCategoryRepository(WebAnimeDbContext context)
        {
            Context = context;
        }

        public async Task<IEnumerable<BlogCategories>> GetAll()
        {
            return await Task.FromResult(Context.BlogCategories.Where(x => !x.IsDeleted));

        }

        public Task<BlogCategories> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Create(BlogCategories entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(BlogCategories entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id, int deletedBy = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BlogCategories>> GetAllBlogCategoriesByBlogId(int blogId)
        {
            return (await Context.Blogs.Include(blogs => blogs.BlogCategories)
                .FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == blogId))?.BlogCategories;
        }
    }
}
