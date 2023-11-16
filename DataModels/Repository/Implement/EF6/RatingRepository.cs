using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels.EF;
using DataModels.Repository.Interface;

namespace DataModels.Repository.Implement.EF6
{
    public class RatingRepository : IRatingRepository
    {
        private readonly WebAnimeDbContext _context;

        public RatingRepository(WebAnimeDbContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<Ratings>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Ratings> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Create(Ratings entity)
        {
            try
            {
                var rate = _context.Ratings.FirstOrDefault(x =>
                    x.AnimeId == entity.AnimeId && x.UserId == entity.UserId && !x.Anime.IsDeleted &&
                    !x.User.IsDeleted);

                if (rate != null) return false;

                _context.Ratings.Add(entity);
                await _context.SaveChangesAsync();
                return entity.Id > 0;


            }
            catch
            {
                return false;
            }
        }

        public Task<bool> Update(Ratings entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id, int deletedBy = default)
        {
            throw new NotImplementedException();
        }
    }
}
