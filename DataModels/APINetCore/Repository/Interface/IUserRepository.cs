using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels.EF.Identity;

namespace DataModels.APINetCore.Repository.Interface
{
    public interface IUserRepository : IRepositoryBase<Users, int>
    {
    }
}
