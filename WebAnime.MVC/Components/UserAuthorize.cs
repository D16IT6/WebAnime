using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebAnime.MVC.Components
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class UserAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var user = filterContext.HttpContext.User;
            if (!user.Identity.IsAuthenticated)
            {
                HandleUnauthorizedRequest(filterContext);
                return;
            }

            if (!string.IsNullOrEmpty(Roles) && !user.IsInRole(Roles))
            {
                HandleUnauthorizedRequest(filterContext);
                return;
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var returnUrl = filterContext.HttpContext.Request.Url?.AbsolutePath ?? string.Empty;

            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary(
                    new
                    {
                        action = "Login",
                        controller = "Account",
                        returnUrl
                    })
            );
        }
    }


}