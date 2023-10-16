using Microsoft.AspNet.Identity.EntityFramework;
namespace DataModels.EF.Identity
{

    public class UserStore : UserStore<Users, Roles, int, UserLogins, UserRoles, UserClaims>
    {
        public UserStore(IdentityDbContext context) : base(context)
        {
        }
    }
}
