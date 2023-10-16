using DataModels.EF;
using DataModels.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DataModels.Dto
{
    public class AnimeDto : BaseDto
    {
        public async Task<Animes> GetById(int id)
        {
            return await Context.Animes
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task<IEnumerable<Animes>> GetAll()
        {
            return await Context.Animes
                .Where(x => !x.IsDeleted)
                .ToListAsync();
        }

        public async Task<bool> Add(Animes entity)
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


        public async Task<bool> Update(Animes newEntity)
        {
            try
            {
                var updateEntity = await Context.Animes.FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == newEntity.Id);
                if (updateEntity == null) return false;
                if (updateEntity.CategoriesId == null) updateEntity.CategoriesId = new int[] { };
                if (updateEntity.StudiosId == null) updateEntity.StudiosId = new int[] { };

                await UpdateCategories(newEntity, updateEntity);
                await UpdateStudios(newEntity, updateEntity);

                UpdateSingleProperties(newEntity, updateEntity);

                await Context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var deleteEntity = await Context.Animes.FindAsync(id);
                if (deleteEntity == null) return false;

                deleteEntity.IsDeleted = true;
                deleteEntity.DeletedDate = DateTime.Now;

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