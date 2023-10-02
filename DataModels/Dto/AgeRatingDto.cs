using DataModels.EF;
using DataModels.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace DataModels.Dto
{
    public class AgeRatingDto : BaseDto
    {
        public AgeRatings GetById(int id)
        {
            return Context.AgeRatings.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
        }

        public IEnumerable<AgeRatings> GetAll()
        {
            return Context.AgeRatings.Where(x => !x.IsDeleted);
        }

        public bool Add(AgeRatings entity)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(AgeRatings entity)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
