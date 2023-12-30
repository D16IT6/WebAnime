using System;
using DataModels.EF.Identity;
using System.Data.Entity;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

using System.Linq;
using System.Security.Claims;

using ViewModels.API;

namespace WebAnime.API2.Controllers
{
    [RoutePrefix("api/User")]
    [Authorize]
    public class UserController : ApiController
    {
        private readonly UserManager _userManager;

        public UserController(UserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetUserById(int id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null) return BadRequest("Cannot find user");
            return Ok(new
            {
                user.Id,
                user.FullName,
                user.Email,
                user.AvatarUrl,
                user.PhoneNumber,
                user.BirthDay,
            });
        }


        [HttpPost]
        public async Task<HttpResponseMessage> UpdateProfile(UserPutViewModel model)
        {
            var check = int.TryParse(ClaimsPrincipal.Current.Claims
                .FirstOrDefault(x => x.Type == ClaimTypes.Sid)?.Value, out var userId);

            if (!check)
            {
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return new HttpResponseMessage(HttpStatusCode.BadRequest);

            if (!String.IsNullOrEmpty(model.FullName)) user.FullName = model.FullName;
            if (!String.IsNullOrEmpty(model.Email)) user.Email = model.Email;
            if (!String.IsNullOrEmpty(model.PhoneNumber)) user.PhoneNumber = model.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded ? new HttpResponseMessage(HttpStatusCode.OK) : new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
    }
}
