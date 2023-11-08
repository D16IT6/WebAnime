using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels.EF;

namespace DataModels.Repository.Interface
{
    public interface IBlogCategoryRepository : IRepositoryBase<BlogCategories, int>
    {
        public Task<IEnumerable<BlogCategories>> GetAllBlogCategoriesByBlogId(int blogId);
    }
}
