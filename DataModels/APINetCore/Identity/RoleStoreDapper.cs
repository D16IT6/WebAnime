using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels.EF.Identity;
using Microsoft.AspNet.Identity;

namespace DataModels.APINetCore.Identity
{
    public class RoleStoreDapper : IRoleStore<Roles,int>
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(Roles role)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Roles role)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Roles role)
        {
            throw new NotImplementedException();
        }

        public Task<Roles> FindByIdAsync(int roleId)
        {
            throw new NotImplementedException();
        }

        public Task<Roles> FindByNameAsync(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}
