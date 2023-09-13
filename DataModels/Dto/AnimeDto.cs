using DataModels.EF;
using DataModels.Helpers;
using System;
using System.Collections.Generic;

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

        public bool Update(Animes entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
