using DataModels.EF;
using DataModels.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataModels.Dto
{
    public class CountryDto : BaseDto
    {
        public Countries GetById(int id) => Context.Countries.FirstOrDefault(x => !x.IsDeleted && x.Id == id);

        public IEnumerable<Countries> GetAll() => Context.Countries.Where(x => !x.IsDeleted);
        public bool Add(Countries entity)
        {
            try
            {
                entity.CreatedDate = entity.ModifiedDate = DateTime.Now;
                entity.IsDeleted = false;

                Context.Countries.Add(entity);
                Context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool Update(Countries entity)
        {
            var updateEntity = Context.Countries.FirstOrDefault(x => !x.IsDeleted && entity.Id == x.Id);
            if (updateEntity == null) return false;
            updateEntity.Name = entity.Name;
            entity.ModifiedDate = DateTime.Now;
            Context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            try
            {
                var deleteEntity = Context.Countries.FirstOrDefault(x => !x.IsDeleted && x.Id == id);
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
    }
}
