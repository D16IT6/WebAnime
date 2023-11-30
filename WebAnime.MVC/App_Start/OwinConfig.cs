using DataModels.EF.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security.Facebook;

namespace WebAnime.MVC
{
    public class OwinConfig
    {

        public static void AuthConfig(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Admin/Auth/Login"),
                ExpireTimeSpan = TimeSpan.FromDays(7),
                Provider = new CookieAuthenticationProvider()
                {
                    OnValidateIdentity = SecurityStampValidator
                        .OnValidateIdentity<UserManager, Users, int>(
                            validateInterval: TimeSpan.FromMinutes(30),
                            regenerateIdentityCallback: (manager, user) =>
                                GenerateUserIdentityAsync(user, manager),
                            getUserIdCallback: (id) => (id.GetUserId<int>()))
                }
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

            app.UseFacebookAuthentication(new FacebookAuthenticationOptions()
            {
                AppId = "358603550031804",
                AppSecret = "90c2fc87b53ede3b985e5a002cdbbe85",
                SignInAsAuthenticationType = DefaultAuthenticationTypes.ExternalBearer,
            });


            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "1054362713534-ihmibg6jq40g5v4f78dn734ore89fe5p.apps.googleusercontent.com",
                ClientSecret = "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"
            });

        }
        public static async Task<ClaimsIdentity> GenerateUserIdentityAsync(Users user, UserManager manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
}