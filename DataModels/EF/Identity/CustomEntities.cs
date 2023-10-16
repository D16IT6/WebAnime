using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace DataModels.EF.Identity
{
    public class Users : IdentityUser<int, UserLogins, UserRoles, UserClaims>
    {
        public DateTime? BirthDay { get; set; }

    }

    public class Roles : IdentityRole<int, UserRoles> { }

    public class UserLogins : IdentityUserLogin<int>
    {

    }

    public class UserRoles : IdentityUserRole<int>
    {

    }

    public class UserClaims : IdentityUserClaim<int>
    {

    }
}
