using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels.EF;
using DataModels.Repository.Interface;
using static Dapper.SqlMapper;

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

        public async Task<bool> Create(Favorites entity)
        {
            try
            {
                var find = await _context.Favorites.FirstOrDefaultAsync(x =>
                    !x.IsDeleted && x.AnimeId == entity.AnimeId && x.CreatedBy == entity.CreatedBy);
                if (find != null)
                {
                    find.CreatedDate = DateTime.Now;
                    find.IsDeleted = false;
                    await _context.SaveChangesAsync();
                    return true;
                }

                entity.CreatedDate = DateTime.Now;
                entity.IsDeleted = false;
                entity.StatusId = 1;
                _context.Favorites.Add(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Task<bool> Update(Favorites entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(int id, int deletedBy = default)
        {
            try
            {
                var find = await _context.Favorites.FirstOrDefaultAsync(x =>
                    !x.IsDeleted && x.Id == id);
                if (find == null)
                {
                    return false;
                }

                find.DeletedDate = DateTime.Now;
                find.IsDeleted = true;
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Task<IQueryable<Favorites>> GetByUserIdAPI(int userId)
        {
            return Task.FromResult(_context.Favorites.Where(x => x.CreatedBy == userId && !x.IsDeleted));
        }
    }
}
