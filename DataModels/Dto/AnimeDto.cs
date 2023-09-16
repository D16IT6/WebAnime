using DataModels.EF;
using DataModels.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace DataModels.Dto
{
    public class AnimeDto : BaseDto, IRepository<Animes>
    {
        public Animes GetById(int id)
        {
            return Context.Animes.Find(id);
        }

        public IEnumerable<Animes> GetAll()
        {
            return Context.Animes;
        }

        public bool Add(Animes entity)
        {
            try
            {
                foreach (int categoryId in entity.CategoriesId)
                {
                    var category = Context.Categories.Find(categoryId);
                    if (category != null)
                    {
                        entity.Categories.Add(category);
                    }
                }

                foreach (int studioId in entity.StudiosId)
                {
                    var studio = Context.Studios.Find(studioId);
                    if (studio != null)
                    {
                        entity.Studios.Add(studio);
                    }
                }

                Context.Animes.Add(entity);
                Context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(Animes newEntity)
        {
            try
            {
                var updateEntity = Context.Animes.Find(newEntity.Id);
                if (updateEntity == null) return false;
                if (updateEntity.CategoriesId == null) updateEntity.CategoriesId = new int[] { };
                if (updateEntity.StudiosId == null) updateEntity.StudiosId = new int[] { };
                UpdateSingleProperties(newEntity, updateEntity);
                UpdateCategories(newEntity, updateEntity);
                UpdateStudios(newEntity, updateEntity);
                Context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                var deleteEntity = Context.Animes.Find(id);
                if (deleteEntity == null) return false;
                Context.Animes.Remove(deleteEntity);
                Context.SaveChanges();
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
        }

        private void UpdateStudios(Animes newEntity, Animes updateEntity)
        {
            var oldStudioIds = updateEntity.Studios.Select(s => s.Id).ToArray();
            var newStudioIds = newEntity.StudiosId;

            var removeStudioIds = oldStudioIds.Except(newStudioIds);
            var insertStudioIds = newStudioIds.Except(oldStudioIds);

            foreach (int studioId in removeStudioIds)
            {
                var removeStudio = updateEntity.Studios.FirstOrDefault(x => x.Id == studioId);
                if (removeStudio == null) continue;
                updateEntity.Studios.Remove(removeStudio);
            }
            foreach (int studioId in insertStudioIds)
            {
                var insertStudio = Context.Studios.Find(studioId);
                if (insertStudio == null) continue;
                updateEntity.Studios.Add(insertStudio);
            }

        }



        void UpdateCategories(Animes newEntity, Animes updateEntity)
        {
            var oldCategoryIds = updateEntity.Categories.Select(x => x.Id).ToArray();

            var newCategoryIds = newEntity.CategoriesId;

            var removeCategoryIds = oldCategoryIds.Except(newCategoryIds);
            var insertCategoryIds = newCategoryIds.Except(oldCategoryIds);

            foreach (int categoryId in removeCategoryIds)
            {
                var removeCategory = updateEntity.Categories.FirstOrDefault(x => x.Id == categoryId);
                if (removeCategory == null) continue;
                updateEntity.Categories.Remove(removeCategory);
            }

            foreach (int categoryId in insertCategoryIds)
            {
                var insertCategory = Context.Categories.Find(categoryId);
                if (insertCategory == null) continue;
                updateEntity.Categories.Add(insertCategory);
            }
        }
    }
}
