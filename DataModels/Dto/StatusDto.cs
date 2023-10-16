using DataModels.EF;
using DataModels.Helpers;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DataModels.Dto
{
    public class StatusDto : BaseDto
    {
        public async Task<IEnumerable<Statuses>> GetAll()
        {
            return await Context.Statuses
                .Where(x => !x.IsDeleted)
                .ToListAsync();
        }
    }
}