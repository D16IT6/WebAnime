using DataModels.EF;
using DataModels.Helpers;
using System;
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
            throw new NotImplementedException();
        }

        public bool Update(Studios entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
