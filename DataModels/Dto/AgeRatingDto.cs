using DataModels.EF;
using DataModels.Helpers;
using System.Collections.Generic;

namespace DataModels.Dto
{
    public class AgeRatingDto : BaseDto, IRepository<AgeRatings>
    {
        public AgeRatings GetById(int id)
        {
            return Context.AgeRatings.Find(id);
        }

        public IEnumerable<AgeRatings> GetAll()
        {
            return Context.AgeRatings;
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
