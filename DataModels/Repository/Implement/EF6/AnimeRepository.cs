using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DataModels.EF;
using DataModels.Repository.Interface;

namespace DataModels.Repository.Implement.EF6
{
    public class AnimeRepository : IAnimeRepository
    {
        public AnimeRepository(WebAnimeDbContext context)
        {
            Context = context;
        }
        public WebAnimeDbContext Context { get; set; }

        public async Task<IEnumerable<Animes>> GetAll()
        {
            return await Task.FromResult(Context.Animes
                .Where(x => !x.IsDeleted));
        }

        public async Task<Animes> GetById(int id)
        {
            return await Context.Animes
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task<bool> Create(Animes entity)
        {
            try
            {
                foreach (var categoryId in entity.CategoriesId)
                {
                    var category = await Context.Categories
                        .FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == categoryId);

                    if (category != null) entity.Categories.Add(category);
                }

                foreach (var studioId in entity.StudiosId)
                {
                    var studio = await Context.Studios
                        .FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == studioId);

                    if (studio != null) entity.Studios.Add(studio);
                }

                entity.CreatedDate = entity.ModifiedDate = DateTime.Now;
                entity.IsDeleted = false;

                Context.Animes.Add(entity);
                await Context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(Animes entity)
        {
            try
            {
                var updateEntity = await Context.Animes.FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == entity.Id);
                if (updateEntity == null) return false;
                updateEntity.CategoriesId ??= new int[] { };
                updateEntity.StudiosId ??= new int[] { };

                await UpdateCategories(entity, updateEntity);
                await UpdateStudios(entity, updateEntity);

                UpdateSingleProperties(entity, updateEntity);

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
                var deleteEntity = await Context.Animes.FindAsync(id);
                if (deleteEntity == null) return false;

                deleteEntity.IsDeleted = true;
                deleteEntity.DeletedDate = DateTime.Now;
                deleteEntity.DeletedBy = deletedBy;

                await Context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }


        private void UpdateSingleProperties(Animes newEntity, Animes updateEntity)
        {
            updateEntity.Title = newEntity.Title;
            updateEntity.OriginalTitle = newEntity.OriginalTitle;
            updateEntity.Synopsis = newEntity.Synopsis;
            updateEntity.Poster = newEntity.Poster;
            updateEntity.Duration = newEntity.Duration;
            updateEntity.Release = newEntity.Release;
            updateEntity.Trailer = newEntity.Trailer;
            updateEntity.TotalEpisodes = newEntity.TotalEpisodes;
            updateEntity.StatusId = newEntity.StatusId;
            updateEntity.TypeId = newEntity.TypeId;
            updateEntity.CountryId = newEntity.CountryId;
            updateEntity.AgeRatingId = newEntity.AgeRatingId;

            updateEntity.ModifiedDate = DateTime.Now;
        }

        private async Task UpdateStudios(Animes newEntity, Animes updateEntity)
        {
            var oldStudioIds = updateEntity.Studios.Where(x => !x.IsDeleted).Select(s => s.Id).ToArray();
            var newStudioIds = newEntity.StudiosId;

            var removeStudioIds = oldStudioIds.Except(newStudioIds);
            var insertStudioIds = newStudioIds.Except(oldStudioIds);

            foreach (var studioId in removeStudioIds)
            {
                var removeStudio = updateEntity.Studios.FirstOrDefault(x => x.Id == studioId && !x.IsDeleted);
                if (removeStudio == null) continue;
                updateEntity.Studios.Remove(removeStudio);
            }

            foreach (var studioId in insertStudioIds)
            {
                var insertStudio = await Context.Studios.FirstOrDefaultAsync(x => x.Id == studioId && !x.IsDeleted);
                if (insertStudio == null) continue;
                updateEntity.Studios.Add(insertStudio);
            }
        }

        private async Task UpdateCategories(Animes newEntity, Animes updateEntity)
        {
            var oldCategoryIds = updateEntity.Categories.Where(x => !x.IsDeleted).Select(x => x.Id).ToArray();
            var newCategoryIds = newEntity.CategoriesId;

            var removeCategoryIds = oldCategoryIds.Except(newCategoryIds);
            var insertCategoryIds = newCategoryIds.Except(oldCategoryIds);

            foreach (var categoryId in removeCategoryIds)
            {
                var removeCategory = updateEntity.Categories.FirstOrDefault(x => x.Id == categoryId && !x.IsDeleted);
                if (removeCategory != null)
                {
                    updateEntity.Categories.Remove(removeCategory);
                }
            }

            foreach (var categoryId in insertCategoryIds)
            {
                var insertCategory = await Context.Categories.FirstOrDefaultAsync(x => x.Id == categoryId && !x.IsDeleted);
                if (insertCategory != null)
                {
                    updateEntity.Categories.Add(insertCategory);
                }
            }
        }
    }


}
