using DataModels.EF;
using DataModels.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataModels.Dto
{
    public class TypeDto : BaseDto
    {
        public Types GetById(int id)
        {
            return Context.Types.FirstOrDefault(x => !x.IsDeleted && x.Id == id);
        }

        public IEnumerable<Types> GetAll()
        {
            return Context.Types.Where(x => !x.IsDeleted);
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
