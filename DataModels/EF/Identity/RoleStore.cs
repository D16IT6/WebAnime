using Microsoft.AspNet.Identity.EntityFramework;

namespace DataModels.EF.Identity
{
    public class RoleStore : RoleStore<Roles, int, UserRoles>
    {
        public RoleStore(IdentityDbContext context) : base(context)
        {
        }
    }
}
