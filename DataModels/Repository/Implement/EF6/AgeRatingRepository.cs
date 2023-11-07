using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DataModels.EF;
using DataModels.Repository.Interface;

namespace DataModels.Repository.Implement.EF6
{
    public class AgeRatingRepository : IAgeRatingRepository
    {
        public AgeRatingRepository(WebAnimeDbContext context)
        {
            Context = context;
        }
        public WebAnimeDbContext Context { get; set; }
        public async Task<IEnumerable<AgeRatings>> GetAll()
        {
            return await Context.AgeRatings
                .Where(x => !x.IsDeleted)
                .ToListAsync();
        }

        public Task<AgeRatings> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Create(AgeRatings entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(AgeRatings entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id, int deletedBy = default)
        {
            throw new NotImplementedException();
        }
    }
}
