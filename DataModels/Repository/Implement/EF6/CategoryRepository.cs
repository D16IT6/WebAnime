using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels.EF;
using DataModels.Repository.Interface;

namespace DataModels.Repository.Implement.EF6
{
    public class CategoryRepository : ICategoryRepository
    {
        public WebAnimeDbContext Context { get; set; }
        public CategoryRepository(WebAnimeDbContext context)
        {
            Context = context;
        }
        public async Task<IEnumerable<Categories>> GetAll()
        {
            return await Context.Categories
                .Where(x => !x.IsDeleted)
                .ToListAsync();
        }

        public Task<Categories> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Create(Categories entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Categories entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id, int deletedBy = default)
        {
            throw new NotImplementedException();
        }
    }
}
