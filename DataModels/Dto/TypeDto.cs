using DataModels.EF;
using DataModels.Helpers;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DataModels.Dto
{
    public class TypeDto : BaseDto
    {

        public async Task<IEnumerable<Types>> GetAll()
        {
            return await Context.Types
                .Where(x => !x.IsDeleted)
                .ToListAsync();
        }
    }
}