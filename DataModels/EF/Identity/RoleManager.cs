using Microsoft.AspNet.Identity;

namespace DataModels.EF.Identity
{
    public class RoleManager : RoleManager<Roles, int>
    {
        public RoleManager(IRoleStore<Roles, int> store) : base(store)
        {
        }
    }
}
