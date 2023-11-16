using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc.Filters;
using System.Web.Mvc;

namespace WebAnime.MVC.Components
{
    public class AdminAreaAuthorize : FilterAttribute, IAuthenticationFilter
    {
        private const string AdminRoleName = "Admin";
        private const string ManagerRoleName = "Manager";

        public void OnAuthentication(AuthenticationContext context)
        {
            var user = context.HttpContext.User;
            if (user.Identity.IsAuthenticated &&
                (user.IsInRole(AdminRoleName) || user.IsInRole(ManagerRoleName))
                )
            {
            }
            else
            {
                context.Result = new HttpUnauthorizedResult();
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