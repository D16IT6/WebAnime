﻿using DataModels.EF;
using DataModels.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataModels.Dto
{
    public class AnimeDto : BaseDto
    {
        public Animes GetById(int id)
        {
            return Context.Animes.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
        }

        public IEnumerable<Animes> GetAll()
        {
            return Context.Animes.Where(x => !x.IsDeleted);
        }

        public bool Add(Animes entity)
        {
            try
            {
                foreach (int categoryId in entity.CategoriesId)
                {
                    var category = Context.Categories.FirstOrDefault(x => !x.IsDeleted && x.Id == categoryId);
                    if (category != null)
                    {
                        entity.Categories.Add(category);
                    }
                }

                foreach (int studioId in entity.StudiosId)
                {
                    var studio = Context.Studios.FirstOrDefault(x => !x.IsDeleted && x.Id == studioId);
                    if (studio != null)
                    {
                        entity.Studios.Add(studio);
                    }
                }

                entity.CreatedDate = entity.ModifiedDate = DateTime.Now;
                entity.IsDeleted = false;

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
                var updateEntity = Context.Animes.FirstOrDefault(x => !x.IsDeleted && x.Id == newEntity.Id);
                if (updateEntity == null) return false;
                if (updateEntity.CategoriesId == null) updateEntity.CategoriesId = new int[] { };
                if (updateEntity.StudiosId == null) updateEntity.StudiosId = new int[] { };

                UpdateCategories(newEntity, updateEntity);
                UpdateStudios(newEntity, updateEntity);

                UpdateSingleProperties(newEntity, updateEntity);

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

                deleteEntity.IsDeleted = true;
                deleteEntity.DeletedDate = DateTime.Now;

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

            updateEntity.ModifiedDate = DateTime.Now;
        }

        private void UpdateStudios(Animes newEntity, Animes updateEntity)
        {
            var oldStudioIds = updateEntity.Studios.Where(x => !x.IsDeleted).Select(s => s.Id).ToArray();
            var newStudioIds = newEntity.StudiosId;

            var removeStudioIds = oldStudioIds.Except(newStudioIds);
            var insertStudioIds = newStudioIds.Except(oldStudioIds);

            foreach (int studioId in removeStudioIds)
            {
                var removeStudio = updateEntity.Studios.FirstOrDefault(x => x.Id == studioId && !x.IsDeleted);
                if (removeStudio == null) continue;
                updateEntity.Studios.Remove(removeStudio);
            }
            foreach (int studioId in insertStudioIds)
            {
                var insertStudio = Context.Studios.FirstOrDefault(x => x.Id == studioId && !x.IsDeleted);
                if (insertStudio == null) continue;
                updateEntity.Studios.Add(insertStudio);
            }

        }



        void UpdateCategories(Animes newEntity, Animes updateEntity)
        {
            var oldCategoryIds = updateEntity.Categories.Where(x => !x.IsDeleted).Select(x => x.Id).ToArray();

            var newCategoryIds = newEntity.CategoriesId;

            var removeCategoryIds = oldCategoryIds.Except(newCategoryIds);
            var insertCategoryIds = newCategoryIds.Except(oldCategoryIds);

            foreach (int categoryId in removeCategoryIds)
            {
                var removeCategory = updateEntity.Categories.FirstOrDefault(x => x.Id == categoryId && !x.IsDeleted);
                if (removeCategory == null) continue;
                updateEntity.Categories.Remove(removeCategory);
            }

            foreach (int categoryId in insertCategoryIds)
            {
                var insertCategory = Context.Categories.FirstOrDefault(x => x.Id == categoryId && !x.IsDeleted);
                if (insertCategory == null) continue;
                updateEntity.Categories.Add(insertCategory);
            }
        }
    }
}
