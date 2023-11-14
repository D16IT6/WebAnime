using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using DataModels.EF;
using DataModels.Repository.Interface;
using Org.BouncyCastle.Utilities.Collections;
using ViewModels.Client;
using static Dapper.SqlMapper;

namespace DataModels.Repository.Implement.Dapper
{
    public class BlogRepositoryDapper : IBlogRepository
    {
        private readonly IDbConnection _connection;
        private readonly IBlogCategoryRepository _blogCategoryRepository;
        private readonly IBlogCommentRepository _blogCommentRepository;
        private readonly IMapper _mapper;

        public BlogRepositoryDapper(IDbConnection connection, IBlogCategoryRepository blogCategoryRepository, IBlogCommentRepository blogCommentRepository, IMapper mapper)
        {
            _connection = connection;
            _blogCategoryRepository = blogCategoryRepository;
            _blogCommentRepository = blogCommentRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Blogs>> GetAll()
        {
            return await _connection.QueryAsync<Blogs>("usp_Get_All_Blogs", commandType: CommandType.StoredProcedure);
        }

        public async Task<Blogs> GetById(int id)
        {
            var blog = await _connection.QuerySingleAsync<Blogs>("usp_Get_Blog_By_Id", new { @Id = id }, commandType: CommandType.StoredProcedure);

            blog.BlogCategories = await _blogCategoryRepository.GetAllBlogCategoriesByBlogId(id) as ICollection<BlogCategories>;
            return blog;

        }

        public async Task<int> CountSameSlug(string slug, int excludeId = 0)
        {
            int count = 0;
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("@Slug", slug, DbType.String, ParameterDirection.Input, 250);
            dynamicParams.Add("@ExcludeId", excludeId);
            dynamicParams.Add("@Count", count, DbType.Int32, ParameterDirection.Output);
            await _connection.QueryAsync("usp_Get_Blog_Same_Slug_Count", dynamicParams, null, null,
                CommandType.StoredProcedure);
            count = dynamicParams.Get<int>("@Count");
            return count;
        }

        public async Task<bool> Create(Blogs entity)
        {
            int count = await CountSameSlug(entity.Slug);
            if (count > 0)
            {
                entity.Slug += $"-{count}";
            }

            var dynamicParams = new DynamicParameters();

            string splitChar = "|";

            int id = 0;

            dynamicParams.Add("@Title", entity.Title);
            dynamicParams.Add("@Slug", entity.Slug);
            dynamicParams.Add("@ImageUrl", entity.ImageUrl);
            dynamicParams.Add("@Content", entity.Content);
            dynamicParams.Add("@CreatedBy", entity.CreatedBy);

            dynamicParams.Add("@CategoryIdList", String.Join(splitChar, entity.BlogCategoryIds ?? new int[] { }));
            dynamicParams.Add("@SplitChar", splitChar);

            dynamicParams.Add("@Id", id, DbType.Int32, ParameterDirection.Output);


            var result = await _connection.ExecuteAsync("usp_Create_Blog", dynamicParams, null, null,
                CommandType.StoredProcedure);
            if (result == 0) return false;

            id = dynamicParams.Get<int>("@Id");
            return id > 0;


        }

        public async Task<bool> Update(Blogs entity)
        {
            int count = await CountSameSlug(entity.Slug, entity.Id);
            if (count > 0)
            {
                entity.Slug += $"-{count}";
            }
            var dynamicParams = new DynamicParameters();

            string splitChar = "|";


            dynamicParams.Add("@Title", entity.Title);
            dynamicParams.Add("@Slug", entity.Slug);
            dynamicParams.Add("@ImageUrl", entity.ImageUrl);
            dynamicParams.Add("@Content", entity.Content);
            dynamicParams.Add("@ModifiedBy", entity.ModifiedBy);

            dynamicParams.Add("@CategoryIdList", String.Join(splitChar, entity.BlogCategoryIds ?? new int[] { }));
            dynamicParams.Add("@SplitChar", splitChar);

            dynamicParams.Add("@Id", entity.Id, DbType.Int32);


            var result = await _connection.ExecuteAsync("usp_Update_Blog", dynamicParams, null, null,
                CommandType.StoredProcedure);

            return result > 0;
        }

        public async Task<bool> Delete(int id, int deletedBy = default)
        {
            return await _connection.ExecuteAsync("usp_Delete_Blog", new { Id = id, DeletedBy = deletedBy },
                commandType: CommandType.StoredProcedure) > 0;
        }

        public async Task<Blogs> GetByIdForClient(int id)
        {
            var blog = await GetById(id);
            if (blog == null) return null;
            blog.BlogComments = (ICollection<BlogComments>)await _blogCommentRepository.GetByBlogId(id);
            return blog;
        }

        public async Task<BlogViewModel> GetBlogViewModel(int blogId)
        {
            var sql = "select * from dbo.Blogs where id = @blogId and IsDeleted = 0";
            var result = await _connection.QueryFirstOrDefaultAsync<BlogViewModel>(sql, new { blogId });
            if(result == null) return null;

            result.BlogCategories = _mapper.Map
                <IEnumerable<BlogCategories>,IEnumerable<BlogCategoryViewModel>>
                (
                    await _blogCategoryRepository.GetAllBlogCategoriesByBlogId(blogId)
                );
            result.BlogComments = await GetBlogCommentViewModel(blogId);
            return result;
        }

        private async Task<IEnumerable<BlogCommentViewModel>> GetBlogCommentViewModel(int blogId)
        {
            var procName = "usp_Get_Blog_Comment_View_Model";
            return await _connection.QueryAsync<BlogCommentViewModel>(procName, new { @BlogId = blogId },
                commandType: CommandType.StoredProcedure);
        }
    }
}
