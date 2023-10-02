using DataModels.EF;
using DataModels.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataModels.Dto
{
    public class StudioDto : BaseDto
    {
        public Studios GetById(int id)
        {
            return Context.Studios.FirstOrDefault(x => !x.IsDeleted && x.Id == id);
        }

        public IEnumerable<Studios> GetAll()
        {
            return Context.Studios.Where(x => !x.IsDeleted);
        }

        public bool Add(Studios entity)
        {
            try
            {
                entity.IsDeleted = false;
                entity.CreatedDate = entity.ModifiedDate = DateTime.Now;
                Context.Studios.Add(entity);
                Context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(Studios entity)
        {
            try
            {
                var updateEntity = GetById(entity.Id);
                if (updateEntity == null) return false;
                updateEntity.Name = entity.Name;
                updateEntity.ModifiedDate = DateTime.Now;
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
                var updateEntity = GetById(id);
                if (updateEntity == null) return false;
                updateEntity.IsDeleted = true;
                updateEntity.DeletedDate = DateTime.Now;
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
