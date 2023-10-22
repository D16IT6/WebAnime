using DataModels.EF.Identity;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
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

        public static IEnumerable<int> GetRoleIdsFromUser(this RoleManager roleManager, UserManager userManager, int userId)
        {
            var roleList = userManager.GetRoles(userId);
            foreach (var roleName in roleList)
            {
                var role = roleManager.FindByName(roleName);
                if (role != null)
                {
                    yield return role.Id;
                }
            }
        }
    }
}
