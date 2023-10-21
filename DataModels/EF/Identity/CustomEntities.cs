using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataModels.EF.Identity
{
    public class Users : IdentityUser<int, UserLogins, UserRoles, UserClaims>
    {
        public DateTime? BirthDay { get; set; }
        [MaxLength(250)]
        public string FullName { get; set; }
        [DataType(DataType.DateTime)] public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }

        [DataType(DataType.DateTime)] public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }

        [DataType(DataType.DateTime)] public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Ratings> Ratings { get; set; }
        public virtual ICollection<Comments> Comments { get; set; }
        public virtual ICollection<BlogComments> BlogComments { get; set; }
        public virtual ICollection<Favorites> Favorites { get; set; }

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
