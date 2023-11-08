using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DataModels.EF;
using DataModels.Repository.Interface;

namespace DataModels.Repository.Implement.Dapper
{
    public class BlogCategoryRepositoryDapper : IBlogCategoryRepository
    {
        private readonly IDbConnection _connection;

        public BlogCategoryRepositoryDapper(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<IEnumerable<BlogCategories>> GetAll()
        {
            return await _connection.QueryAsync<BlogCategories>("Select* from dbo.BlogCategories where IsDeleted = 0 ");

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
            return await _connection.QueryAsync<BlogCategories>("usp_Get_Blog_Category_By_BlogId",
                new { @BlogId = blogId }, commandType: CommandType.StoredProcedure);
        }
    }
}
