using DataModels.EF.Identity;
using Microsoft.AspNet.Identity;
using System.Globalization;

namespace DataModels.Helpers
{
    public static class RoleManagerExtensions
    {
        public static RoleManager CreateRoleIfNotExist(this RoleManager roleManager, string roleName)
        {
            string formattedRoleName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(roleName.ToLower());

            if (!roleManager.RoleExists(formattedRoleName))
            {
                roleManager.Create(new Roles()
                {
                    Name = formattedRoleName
                });
            }

            return roleManager;
        }
    }
}
