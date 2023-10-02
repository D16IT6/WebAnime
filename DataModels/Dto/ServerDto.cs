using DataModels.EF;
using DataModels.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataModels.Dto
{
    public class ServerDto : BaseDto
    {
        public Servers GetById(int id)
        {
            return Context.Servers.FirstOrDefault(x => !x.IsDeleted && x.Id == id);
        }

        public IEnumerable<Servers> GetAll()
        {
            return Context.Servers.Where(x => !x.IsDeleted);
        }
        public Servers GetFirst()
        {
            return Context.Servers.FirstOrDefault();
        }
        public bool Add(Servers entity)
        {
            try
            {
                entity.ModifiedDate = entity.CreatedDate = DateTime.Now;
                Context.Servers.Add(entity);
                Context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(Servers entity)
        {
            var updateEntity = GetById(entity.Id);
            if (updateEntity == null) return false;
            updateEntity.Name = entity.Name;
            updateEntity.Description = entity.Description;
            updateEntity.ModifiedDate = DateTime.Now;
            Context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            try
            {
                var deleteEntity = GetById(id);
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
