using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels.EF;

namespace DataModels.Repository.Interface
{
    public interface IBlogCommentRepository : IRepositoryBase<BlogComments,int>
    {
        public Task<IEnumerable<BlogComments>> GetByBlogId(int blogId);
        public Task<int> Comment(BlogComments comment);
    }
}
