using DataModels.EF.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using System;

namespace WebAnime.API2
{

    public class OwinConfig
    {

        private static IDataProtectionProvider DataProtectionProvider { get; set; }
        public static void RegisterTokenService(UserManager userManager)
        {
            var dataProtectorProvider = DataProtectionProvider;

            var provider = dataProtectorProvider.Create("WebAnime.MVC.ResetTokenKey.Abc!@#)(&");
            userManager.UserTokenProvider = new DataProtectorTokenProvider<Users, int>(provider)
            {
                TokenLifespan = TimeSpan.FromHours(1)
            };
        }
        public static void AuthConfig(IAppBuilder app)
        {
            DataProtectionProvider = app.GetDataProtectionProvider();
            // Configure the db context, user manager and signin manager to use a single instance per request
            //Ninject cover it

            //app.CreatePerOwinContext(WebAnimeDbContext.Create);
            //app.CreatePerOwinContext(UserManager.Create);
            //app.CreatePerOwinContext<RoleManager>(RoleManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Admin/Auth/Login"),
                ExpireTimeSpan = TimeSpan.FromDays(7)
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(15));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);


            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "358603550031804",
            //   appSecret: "90c2fc87b53ede3b985e5a002cdbbe85");


            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});

        }
    }
}