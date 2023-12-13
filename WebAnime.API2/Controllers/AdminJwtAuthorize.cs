using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebAnime.API2.Controllers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AdminJwtAuthorize : Attribute, IAuthorizationFilter
    {
        private const string AdminRoleName = "Admin";
        private const string ManagerRoleName = "Manager";

        public async Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken,
            Func<Task<HttpResponseMessage>> continuation)
        {
            var principal = ClaimsPrincipal.Current;

            if (principal == null || !principal.Identity.IsAuthenticated)
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);

            var roles = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (roles != null && (roles.Contains(AdminRoleName) || roles.Contains(ManagerRoleName)))
            {
                return await continuation();
            }

            return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }

        public bool AllowMultiple { get; set; }
    }

}