using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels.EF;
using DataModels.EF.Identity;
using DataModels.Repository.Interface;

namespace DataModels.Repository.Implement.EF6
{
    public class UserTokenRepository :IUserTokenRepository
    {
        private readonly WebAnimeDbContext _context;

        public UserTokenRepository(WebAnimeDbContext webAnimeDbContext)
        {
            _context = webAnimeDbContext;
        }

        public Task<IEnumerable<UserRefreshToken>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<UserRefreshToken> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Create(UserRefreshToken entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(UserRefreshToken entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id, int deletedBy = default)
        {
            throw new NotImplementedException();
        }

        public async Task SaveRefreshToken(int userId, Guid token)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId && !x.IsDeleted).ConfigureAwait(false);

            if (user == null)return;

            var userRefreshToken = user.RefreshToken ??= new UserRefreshToken();

            userRefreshToken.CreationTime = DateTimeOffset.UtcNow;
            userRefreshToken.ExpiredTime = DateTimeOffset.UtcNow.AddMonths(1);
            userRefreshToken.RefreshToken = token;

            await _context.SaveChangesAsync().ConfigureAwait(false);

        }
    }
}
