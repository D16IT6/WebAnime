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
    public class StatusRepository : IStatusRepository
    {
        public WebAnimeDbContext Context { get; set; }
        public StatusRepository(WebAnimeDbContext context)
        {
            Context = context;
        }


        public async Task<IEnumerable<Statuses>> GetAll()
        {
            return await Context.Statuses
                .Where(x => !x.IsDeleted)
                .ToListAsync();
        }

        public Task<Statuses> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Create(Statuses entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Statuses entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id, int deletedBy = default)
        {
            throw new NotImplementedException();
        }
    }
}
