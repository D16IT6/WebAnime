using DataModels.EF;
using DataModels.Helpers;
using System.Collections.Generic;

namespace DataModels.Dto
{
    public class StudioDto : BaseDto, IRepository<Studios>
    {
        public Studios GetById(int id)
        {
            return Context.Studios.Find(id);
        }

        public IEnumerable<Studios> GetAll()
        {
            return Context.Studios;
        }

        public bool Add(Studios entity)
        {
            try
            {
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
                var updateEntity = Context.Studios.Find(entity.Id);
                if (updateEntity == null) return false;
                updateEntity.Name = entity.Name;
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
                var updateEntity = Context.Studios.Find(id);
                if (updateEntity == null) return false;
                Context.Studios.Remove(updateEntity);
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
