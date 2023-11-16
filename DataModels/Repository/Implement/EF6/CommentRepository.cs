﻿using System;
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

        public async Task<IEnumerable<CommentShowViewModel>> GetPaging(int animeId, int pageNumber, int pageSize)
        {
            var commentViewModels =
                Context.Comments
                .Where(x => !x.IsDeleted && x.AnimeId == animeId)
                .Join(Context.Users, c => c.CreatedBy, u => u.Id, (c, u) =>
                    new CommentShowViewModel()
                    {
                        AnimeId = animeId,
                        Content = c.Content,
                        CreatedBy = c.CreatedBy,
                        CreatedDate = c.CreatedDate ?? DateTime.Now,
                        AvatarUrl = u.AvatarUrl,
                        UserFullName = u.FullName,
                        Id = c.Id
                    }
                )
                .OrderByDescending(x => x.CreatedDate)//Order before skip and take
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize);

            return await Task.FromResult(commentViewModels).ConfigureAwait(false);
        }

        public async Task<CommentShowViewModel> Comment(Comments comment)
        {
            try
            {
                comment.CreatedDate = DateTime.Now;
                Context.Comments.Add(comment);

                await Context.SaveChangesAsync();
                
                var user = Context.Users.FirstOrDefault(x => !x.IsDeleted && x.Id == comment.CreatedBy);

                return new CommentShowViewModel()
                {
                    AnimeId = comment.AnimeId,
                    Content = comment.Content,
                    CreatedBy = comment.CreatedBy,
                    AvatarUrl = user?.AvatarUrl,
                    UserFullName = user?.FullName,
                    CreatedDate = comment.CreatedDate ?? DateTime.Now,
                };
            }
            catch 
            {
                return null;
            }
        }
    }
}
