using AutoMapper;
using DataModels.EF.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ViewModels.Admin;
using Microsoft.Owin.Security;
using System.Security.Claims;

namespace WebAnime.API2.Controllers
{
    public class UserController : ApiController
    {
        private readonly UserManager _userManager;
        private readonly IMapper _mapper;
        private readonly SignInManager<Users, int> _signInManager;
        public UserController(UserManager userManager, IMapper mapper, SignInManager<Users, int> signInManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
        }

        public async Task<IHttpActionResult> GetAllUsers()
        {
            var userList = await _userManager.Users.AsNoTracking().ToListAsync();

            var userViewModelList =
                _mapper.Map<List<Users>, IEnumerable<UserViewModel>>(userList);

            return Ok(new
            {
                success = true,
                userList = userViewModelList
            });
        }

        [Authorize]
        [HttpGet]
        public async Task<IHttpActionResult> GetUserById(int id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);

            var userId = user?.Id ?? 1;
            var userViewModel =
                _mapper.Map<UserViewModel>(user);
            var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(userId);
            var changePhoneNumberToken = await _userManager.GenerateChangePhoneNumberTokenAsync(userId, "0123456789");
            var passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(userId);

            return Ok(new
            {
                success = true,
                user = new
                {
                    data = userViewModel,
                    emailConfirmationToken,
                    changePhoneNumberToken,
                    passwordResetToken
                }
            });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("User/Login")]
        public async Task<IHttpActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return await Task.FromResult(BadRequest("Dữ liệu không hợp lệ"));


            var result = await _signInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, false, false);

            if (result != SignInStatus.Success) return await Task.FromResult(BadRequest("Dữ liệu không hợp lệ"));

            var currentUser = await _userManager.FindByNameAsync(loginViewModel.UserName);

            var identity =
                await _userManager.CreateIdentityAsync(currentUser, DefaultAuthenticationTypes.ExternalBearer);
            var claimsIdentity = new ClaimsIdentity(identity);

            // Tạo đối tượng AuthenticationTicket từ ClaimsIdentity
            var ticket = new AuthenticationTicket(claimsIdentity, new AuthenticationProperties());

            // Tạo AccessToken từ AuthenticationTicket
            var accessToken = OwinConfig.OAuthAuthorizationOptions.AccessTokenFormat.Protect(ticket);

            // Trả về token
            return Ok(new { AccessToken = accessToken });
            
        }

    }
}
