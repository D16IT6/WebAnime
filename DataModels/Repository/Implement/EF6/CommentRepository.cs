using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DataModels.EF;
using DataModels.Repository.Interface;
using ViewModels.Client;

namespace DataModels.Repository.Implement.EF6
{
    public class CommentRepository : ICommentRepository
    {
        public WebAnimeDbContext Context { get; set; }
        public CommentRepository(WebAnimeDbContext context)
        {
            Context = context;
        }
        public Task<IEnumerable<Comments>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Comments> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Create(Comments entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Comments entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id, int deletedBy = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CommentViewModel>> GetPaging(int animeId, int pageNumber, int pageSize)
        {
            var commentViewModels =
                Context.Comments
                .Where(x => !x.IsDeleted && x.AnimeId == animeId)
                .Join(Context.Users, c => c.CreatedBy, u => u.Id, (c, u) =>
                    new CommentViewModel()
                    {
                        AnimeId = animeId,
                        Content = c.Content,
                        CreatedBy = c.CreatedBy ?? 0,
                        CreatedDate = c.CreatedDate ?? DateTime.Now,
                        AvatarUrl = u.AvatarUrl,
                        UserFullName = u.FullName,
                        Id = c.Id
                    }
                )
                .OrderByDescending(x => x.CreatedDate)//Order before skip and take
                .Skip(pageSize * pageNumber)
                .Take(pageSize);

            //var query = (from a in Context.Animes
            //        where a.Id == animeId && !a.IsDeleted
            //        from c in a.Comments
            //        where !c.IsDeleted
            //        join u in Context.Users on c.CreatedBy equals u.Id
            //        select new CommentViewModel
            //        {
            //            AnimeId = animeId,
            //            Content = c.Content,
            //            CreatedBy = c.CreatedBy ?? 0,
            //            CreatedDate = c.CreatedDate ?? DateTime.Now,
            //            AvatarUrl = u.AvatarUrl,
            //            UserFullName = u.FullName,
            //            Id = c.Id
            //        })
            //    .OrderByDescending(x => x.CreatedDate)//Order before skip and take
            //    .Skip(pageSize * pageNumber)
            //    .Take(pageSize);
            //return await Task.FromResult(query).ConfigureAwait(false);
            return await Task.FromResult(commentViewModels).ConfigureAwait(false);
        }
    }
}
