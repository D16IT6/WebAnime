using DataModels.EF;
using DataModels.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace DataModels.Dto
{
    public class CountryDto : BaseDto, IRepository<Countries>
    {
        public Countries GetById(int id) => Context.Countries.Find(id);

        public IEnumerable<Countries> GetAll() => Context.Countries.AsEnumerable();
        public bool Add(Countries entity)
        {
            try
            {
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
            var updateEntity = Context.Countries.Find(entity.Id);
            if (updateEntity == null) return false;
            updateEntity.Name = entity.Name;
            Context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            try
            {
                var deleteEntity = Context.Countries.Find(id);
                if (deleteEntity == null) return false;
                Context.Countries.Remove(deleteEntity);
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
