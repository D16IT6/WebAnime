using Microsoft.Owin.Security;

using System.Web;
using System.Web.Mvc;

namespace WebAnime.MVC.Components
{

    internal class ChallengeResult : HttpUnauthorizedResult
    {

        private const string XsrfKey = "XsrfId";
        public ChallengeResult(string provider, string redirectUri)
            : this(provider, redirectUri, null)
        {
        }

        public ChallengeResult(string provider, string redirectUri, string userId)
        {
            LoginProvider = provider;
            RedirectUri = redirectUri;
            UserId = userId;
        }

        public string LoginProvider { get; set; }
        public string RedirectUri { get; set; }
        public string UserId { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
            if (UserId != null)
            {
                properties.Dictionary[XsrfKey] = UserId;
            }

            var authentication = NinjectConfig.GetService<IAuthenticationManager>();

            authentication.Challenge(properties, LoginProvider);
        }
    }
}