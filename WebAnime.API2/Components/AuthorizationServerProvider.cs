using System.Threading.Tasks;
using DataModels.EF.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.OAuth;

namespace WebAnime.API2.Components
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            if (context.TryGetBasicCredentials(out var clientId, out var clientSecret))
            {
                // validate the client Id and secret against database or from configuration file.
                context.Validated();
            }
            else
            {
                context.SetError("invalid_client", "Client credentials could not be retrieved from the Authorization header");
                context.Rejected();
            }
            context.Validated();

            return Task.CompletedTask;
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userManager = NinjectConfig.GetService<UserManager>();

            Users user;
            try
            {
                user = await userManager.FindAsync(context.UserName, context.Password);
            }
            catch
            {
                context.SetError("server_error");
                context.Rejected();
                return;
            }
            if (user != null)
            {
                var identity = await userManager.CreateIdentityAsync(
                    user,
                    DefaultAuthenticationTypes.ExternalBearer);
                context.Validated(identity);
            }
            else
            {
                context.SetError("invalid_grant", "Tài khoản hoặc mật khẩu không đúng");
                context.Rejected();
            }
        }
    }
}