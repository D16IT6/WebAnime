
using System;
using System.Threading.Tasks;
using DataModels.EF.Identity;

namespace DataModels.Repository.Interface
{
    public interface IUserTokenRepository : IRepositoryBase<UserRefreshToken,int>
    {
        public Task SaveRefreshToken(int userId,Guid token,bool shouldUpdateExpirationTime);
    }
}
