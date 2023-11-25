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
    public class BlogRepository : IBlogRepository
    {
        public BlogRepository(WebAnimeDbContext context)
        {
            Context = context;
        }
        public WebAnimeDbContext Context { get; set; }
        public async Task<IEnumerable<Blogs>> GetAll()
        {
            return await Task.FromResult(Context.Blogs.Where(x => !x.IsDeleted).AsEnumerable());

        }

        public async Task<Blogs> GetById(int id)
        {
            return await Context.Blogs.FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == id);
        }

        public async Task<bool> Create(Blogs entity)
        {
            try
            {
                var sameSlugList = Context.Blogs.Where(x => !x.IsDeleted && x.Slug.Contains(entity.Slug))
                    .Select(x => x.Slug);
                if (sameSlugList.Any())
                {
                    entity.Slug = $"{await sameSlugList.FirstOrDefaultAsync()}-{await sameSlugList.CountAsync()}";
                }
                entity.CreatedDate = DateTime.Now;
                entity.IsDeleted = false;
                entity.BlogCategories = new HashSet<BlogCategories>();

                foreach (int blogCategoryId in entity.BlogCategoryIds)
                {
                    var blogCategories =
                        await Context.BlogCategories.FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == blogCategoryId);
                    if (blogCategories != null)
                    {
                        entity.BlogCategories.Add(blogCategories);
                    }
                }
                Context.Blogs.Add(entity);

                await Context.SaveChangesAsync();

                return entity.Id > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(Blogs entity)
        {
            try
            {
                var updateEntity = await Context.Blogs.FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == entity.Id);
                if (updateEntity == null) return false;

                UpdateSingleProperties(entity, updateEntity);

                await UpdateCategories(entity, updateEntity);

                updateEntity.ModifiedDate = DateTime.Now;

                await Context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Delete(int id, int deletedBy = default)
        {
            try
            {
                var deleteEntity = Context.Blogs.FirstOrDefault(x => !x.IsDeleted && x.Id == id);
                if (deleteEntity == null) return false;

                deleteEntity.IsDeleted = true;
                deleteEntity.DeletedBy = deletedBy;
                deleteEntity.DeletedDate = DateTime.Now;


                await Context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<BlogViewModel> GetBlogViewModel(int blogId)
        {
            var result = await Context.Blogs
                .Include(blogs => blogs.BlogCategories)
                .Include(blogs => blogs.BlogComments.Select(blogComments => blogComments.User))
                .FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == blogId);
            if (result == null) return null;
            return new BlogViewModel()
            {
                Id = result.Id,
                CreatedBy = result.CreatedBy,
                CreatedDate = result.CreatedDate ?? DateTime.Now,
                Content = result.Content,
                Slug = result.Slug,
                Title = result.Title,
                ImageUrl = result.ImageUrl,
                BlogCategories = result.BlogCategories
                    .Where(x => !x.IsDeleted)
                    .Select(x => new BlogCategoryViewModel()
                    { Id = x.Id, Name = x.Name }
                    ),
                BlogComments = result.BlogComments
                    .Where(x => !x.IsDeleted)
                    .Select(x => new BlogCommentViewModel()
                    {
                        Id = x.Id,
                        AvatarUrl = x.User.AvatarUrl,
                        BlogId = blogId,
                        Content = x.Content,
                        CreatedBy = x.CreatedBy ?? 1,
                        CreatedDate = x.CreatedDate ?? DateTime.Now,
                    }
                    )
            };

        }


        private async Task UpdateCategories(Blogs entity, Blogs updateEntity)
        {
            var oldCategoryIds = updateEntity.BlogCategories.Where(x => !x.IsDeleted).Select(x => x.Id).ToArray();
            var newCategoryIds = entity.BlogCategoryIds;

            var removeCategoryIds = oldCategoryIds.Except(newCategoryIds);
            var insertCategoryIds = newCategoryIds.Except(oldCategoryIds);

            foreach (var categoryId in removeCategoryIds)
            {
                var removeCategory = updateEntity.BlogCategories.FirstOrDefault(x => x.Id == categoryId && !x.IsDeleted);
                if (removeCategory != null)
                {
                    updateEntity.BlogCategories.Remove(removeCategory);
                }
            }

            foreach (var categoryId in insertCategoryIds)
            {
                var insertCategory = await Context.BlogCategories.FirstOrDefaultAsync(x => x.Id == categoryId && !x.IsDeleted);
                if (insertCategory != null)
                {
                    updateEntity.BlogCategories.Add(insertCategory);
                }
            }
        }
        void UpdateSingleProperties(Blogs source, Blogs dest)
        {
            dest.Slug = source.Slug;
            dest.Title = source.Title;
            dest.Content = source.Content;
            dest.ImageUrl = source.ImageUrl;
            dest.ModifiedBy = source.ModifiedBy;
        }
    }
}
