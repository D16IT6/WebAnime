using DataModels.EF;
using DataModels.Helpers;
using System;
using System.Collections.Generic;

namespace DataModels.Dto
{
    public class TypeDto : BaseDto, IRepository<Types>
    {
        public Types GetById(int id)
        {
            return Context.Types.Find(id);
        }

        public IEnumerable<Types> GetAll()
        {
            return Context.Types;
        }

        public bool Add(Types entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(Types entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
