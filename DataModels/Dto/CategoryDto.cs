using DataModels.EF;
using DataModels.Helpers;
using System.Collections.Generic;

namespace DataModels.Dto
{
    public class CategoryDto : BaseDto, IRepository<Categories>
    {
        public Categories GetById(int id)
        {
            return Context.Categories.Find(id);
        }

        public IEnumerable<Categories> GetAll()
        {
            return Context.Categories;
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
