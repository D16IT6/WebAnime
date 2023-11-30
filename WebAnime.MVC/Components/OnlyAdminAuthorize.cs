using System.Web.Mvc.Filters;
using System.Web.Mvc;

namespace WebAnime.MVC.Components
{
    public class OnlyAdminAuthorizeAttribute : FilterAttribute, IAuthenticationFilter
    {
        private const string AdminRoleName = "Admin";

        public void OnAuthentication(AuthenticationContext context)
        {
            var user = context.HttpContext.User;
            switch (user.Identity.IsAuthenticated)
            {
                case false:
                    context.Result = new HttpNotFoundResult();
                    break;
                case true when
                    (user.IsInRole(AdminRoleName)):
                    break;
                default:
                    context.Result = new HttpUnauthorizedResult();
                    break;
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext context)
        {
            var returnUrl = context.HttpContext.Request.Url?.AbsolutePath ?? string.Empty;
            if (context.Result == null || context.Result is HttpUnauthorizedResult)
            {
                context.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary(
                        new
                        {
                            action = "NotFound",
                            controller = "Error",
                            area = "Admin",
                        }
                    )
                );
            }
            if (context.Result == null || context.Result is HttpNotFoundResult)
            {
                context.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary(
                        new
                        {
                            action = "Login",
                            controller = "Auth",
                            area = "Admin",
                            returnUrl
                        }
                    )
                );
            }
        }
    }
}