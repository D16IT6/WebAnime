﻿using DataModels.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;

namespace DataModels.EF.Identity
{
    public class UserManager : UserManager<Users, int>
    {
        public UserManager(IUserStore<Users, int> store) : base(store)
        {
            ResigterAuth(this);
        }
        private static void ResigterAuth(UserManager userManager)
        {
            userManager.UserValidator = new UserValidator<Users, int>(userManager)
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

            var dataProtectionProvider = new DpapiDataProtectionProvider("WebAnime.DpapiDataProtectionProvider.Talon.123!@#");
            ;

            userManager.UserTokenProvider =
                new DataProtectorTokenProvider<Users, int>(
                    dataProtectionProvider.Create("WebAnime.MVC.ResetTokenKey.Abc!@#)(&"));

            userManager.UserLockoutEnabledByDefault = true;
            userManager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(AuthConstants.LockoutMinutes);
            userManager.MaxFailedAccessAttemptsBeforeLockout = AuthConstants.MaxFailedAccessAttemptsBeforeLockout;


            //implement later
            //userManager.EmailService = new EmailService();
            //userManager.SmsService = new Abc();


            //userManager.RegisterTwoFactorProvider("PhoneCode",
            //    new PhoneNumberTokenProvider<Users, int>
            //    {
            //        MessageFormat = "Mã xác thực của bạn: {0}"
            //    });

            //userManager.RegisterTwoFactorProvider("EmailCode",
            //    new EmailTokenProvider<Users, int>
            //    {
            //        Subject = "Security Code",
            //        BodyFormat = "Your security code is: {0}"
            //    });

        }

        public class SignInManager : SignInManager<Users, int>
        {
            public SignInManager(UserManager<Users, int> userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager)
            {
            }
        }

    }
}