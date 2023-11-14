using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DataModels.EF;
using DataModels.Repository.Interface;

namespace DataModels.Repository.Implement.Dapper
{
    public class BlogCommentRepositoryDapper : IBlogCommentRepository
    {
        private readonly IDbConnection _connection;

        public BlogCommentRepositoryDapper(IDbConnection connection)
        {
            _connection = connection;
        }

        public Task<IEnumerable<BlogComments>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<BlogComments> GetById(int id)
        {
            return await _connection.QuerySingleAsync<BlogComments>(
                "Select * from BlogComments where IsDeleted = 0 and Id = @Id", new { Id = id });
        }
        

        public Task<bool> Create(BlogComments entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(BlogComments entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id, int deletedBy = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BlogComments>> GetByBlogId(int blogId)
        {
            var sql = "Select * from dbo.BlogComments where IsDeleted = 0 and BlogId = @blogId";
            return await _connection.QueryAsync<BlogComments>(sql, new { blogId });

        }

        public async Task<int> Comment(BlogComments comment)
        {
            var parameters = new DynamicParameters();
            int id = 0;
            parameters.Add("@Id",id,direction:ParameterDirection.Output);
            parameters.Add("@CreatedBy",comment.CreatedBy);
            parameters.Add("@Content",comment.Content);
            parameters.Add("@BlogId",comment.BlogId);
            var result =
                await _connection.ExecuteAsync("usp_Blog_Comment", parameters, null, null, CommandType.StoredProcedure);
            if(result  == 0) return 0;
            id = parameters.Get<int>("@Id");
            return id;
        }

    }
}
