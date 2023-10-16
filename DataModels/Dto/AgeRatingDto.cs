using DataModels.EF;
using DataModels.Helpers;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DataModels.Dto
{
    public class AgeRatingDto : BaseDto
    {
        public async Task<List<AgeRatings>> GetAll()
        {
            return await Context.AgeRatings
                .Where(x => !x.IsDeleted)
                .ToListAsync();
        }
    }
}