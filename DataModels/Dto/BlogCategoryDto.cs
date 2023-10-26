using DataModels.EF;
using DataModels.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataModels.Dto
{
    public class BlogCategoryDto : BaseDto
    {
        public async Task<IEnumerable<BlogCategories>> GetAll()
        {
            return await Task.FromResult(Context.BlogCategories.Where(x => !x.IsDeleted));
        }
    }
}
