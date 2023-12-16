using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels.EF;
using DataModels.Repository.Interface;

namespace DataModels.Repository.Implement.EF6
{
    public class AnimeFavoriteRepository : IAnimeFavoriteRepository
    {
        private readonly WebAnimeDbContext _context;

        public AnimeFavoriteRepository(WebAnimeDbContext context)
        {
            _context = context;
        }


        public Task<IEnumerable<Favorites>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Favorites> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Create(Favorites entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Favorites entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id, int deletedBy = default)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<Favorites>> GetByUserIdAPI(int userId)
        {
            return Task.FromResult(_context.Favorites.Where(x => x.CreatedBy == userId && !x.IsDeleted));
        }
    }
}
