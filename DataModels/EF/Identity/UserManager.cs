using DataModels.Helpers;
using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;

namespace DataModels.EF.Identity
{
    public class UserManager : UserManager<Users, int>
    {
        public UserManager(IUserStore<Users, int> store) : base(store)
        {
            ResigterAuth(this);
        }

        public override Task<Users> FindByNameAsync(string userName)
        {
            return base.FindByNameAsync(userName);
        }

        private void ResigterAuth(UserManager userManager)
        {
            //register user validator
            this.UserValidator = new UserValidator<Users, int>(userManager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true,

            };

            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            userManager.UserLockoutEnabledByDefault = true;
            userManager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(AuthConstants.LockoutMinutes);
            userManager.MaxFailedAccessAttemptsBeforeLockout = AuthConstants.MaxFailedAccessAttemptsBeforeLockout;

            //implement later
            //userManager.EmailService = new EmailService();
            //userManager.SmsService = new Abc();
        }
    }
}
