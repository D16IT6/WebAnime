using DataModels.EF.Identity;
using Microsoft.AspNet.Identity;
using System.Globalization;

namespace DataModels.Helpers
{
    public static class UserManagerExtensions
    {
        public static UserManager CreateUserIfNotExist(this UserManager userManager, Users user, string password, string roleName)
        {
            string formattedRoleName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(roleName);
            if (userManager.FindByName(user.UserName) == null)
            {

                var result = userManager.Create(user, password);
                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, formattedRoleName);
                }
            }
            return userManager;
        }

    }
}
