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
    public class TypeRepository : ITypeRepository
    {
        public WebAnimeDbContext Context { get; set; }

        public TypeRepository(WebAnimeDbContext context)
        {
            Context = context;
        }
        public async Task<IEnumerable<Types>> GetAll()
        {
            return await Context.Types
                .Where(x => !x.IsDeleted)
                .ToListAsync();
        }

        public Task<Types> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Create(Types entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Types entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id, int deletedBy = default)
        {
            throw new NotImplementedException();
        }
    }
}
