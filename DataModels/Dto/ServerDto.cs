using DataModels.EF;
using DataModels.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace DataModels.Dto
{
    public class ServerDto : BaseDto, IRepository<Servers>
    {
        public Servers GetById(int id)
        {
            return Context.Servers.Find(id);
        }

        public IEnumerable<Servers> GetAll()
        {
            return Context.Servers;
        }
        public Servers GetFirst()
        {
            return Context.Servers.FirstOrDefault();
        }
        public bool Add(Servers entity)
        {
            try
            {
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
            var updateEntity = Context.Servers.Find(entity.Id);
            if (updateEntity == null) return false;
            updateEntity.Name = entity.Name;
            updateEntity.Description = entity.Description;
            Context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            try
            {
                var deleteEntity = Context.Servers.Find(id);
                if (deleteEntity == null) return false;
                Context.Servers.Remove(deleteEntity);
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
