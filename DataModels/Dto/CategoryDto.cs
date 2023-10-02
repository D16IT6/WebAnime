using DataModels.EF;
using DataModels.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace DataModels.Dto
{
    public class CategoryDto : BaseDto
    {
        public Categories GetById(int id)
        {
            return Context.Categories.FirstOrDefault(x => !x.IsDeleted && x.Id == id);
        }

        public IEnumerable<Categories> GetAll()
        {
            return Context.Categories.Where(x => !x.IsDeleted);
        }

        public bool Add(Categories entity)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(Categories entity)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
