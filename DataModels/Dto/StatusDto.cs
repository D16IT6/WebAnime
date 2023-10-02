using DataModels.EF;
using DataModels.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataModels.Dto
{
    public class StatusDto : BaseDto
    {
        public Statuses GetById(int id)
        {
            return Context.Statuses.FirstOrDefault(x => !x.IsDeleted && x.Id == id);
        }

        public IEnumerable<Statuses> GetAll()
        {
            return Context.Statuses.Where(x => !x.IsDeleted);
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
