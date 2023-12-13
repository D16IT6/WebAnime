using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace WebAnime.MVC.Components
{
    public class AdminAreaAuthorize : FilterAttribute, IAuthenticationFilter
    {
        private const string AdminRoleName = "Admin";
        private const string ManagerRoleName = "Manager";

        public void OnAuthentication(AuthenticationContext context)
        {
            var user = context.HttpContext.User;
            switch (user.Identity.IsAuthenticated)
            {
                case false:
                    context.Result = new HttpNotFoundResult();
                    break;
                case true when
                    (user.IsInRole(AdminRoleName) || user.IsInRole(ManagerRoleName)):
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