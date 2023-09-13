using DataModels.EF;
using DataModels.Helpers;
using System;
using System.Collections.Generic;

namespace DataModels.Dto
{
    public class StatusDto : BaseDto, IRepository<Statuses>
    {
        public Statuses GetById(int id)
        {
            return Context.Statuses.Find(id);
        }

        public IEnumerable<Statuses> GetAll()
        {
            return Context.Statuses;
        }

        public bool Add(Statuses entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(Statuses entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
