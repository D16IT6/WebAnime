using System;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Web.Http;
using ViewModels.Client;
using DataModels.EF.Identity;
using DataModels.Repository.Interface;
using WebAnime.API2.Components;
using System.Net.Http;
using System.Net;
using ViewModels.API;

namespace WebAnime.API2.Controllers
{
    [RoutePrefix("api/Auth")]
    public class AuthController : ApiController
    {
        private readonly UserManager _userManager;
        private readonly SignInManager<Users, int> _signInManager;
        private readonly IUserTokenRepository _userTokenRepository;

        public AuthController(SignInManager<Users, int> signInManager, UserManager userManager, IUserTokenRepository userTokenRepository)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userTokenRepository = userTokenRepository;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public async Task<IHttpActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return await Task.FromResult(BadRequest("Dữ liệu không hợp lệ"));

            var result = await _signInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, loginViewModel.RememberMe, false);

            if (result != SignInStatus.Success) return await Task.FromResult(BadRequest("Dữ liệu không hợp lệ"));

            var currentUser = await _userManager.FindByNameAsync(loginViewModel.UserName);

            var roleList = (await _userManager.GetRolesAsync(currentUser.Id));


            var userRefreshToken = currentUser.RefreshToken;
            if (userRefreshToken != null && userRefreshToken.ExpiredTime > DateTimeOffset.UtcNow)
                return Ok(new
                {
                    AccessToken = JwtProvider.GenerateJwt(currentUser, roleList),
                    userRefreshToken.RefreshToken
                });

            var refreshToken = Guid.NewGuid();
            await SaveRefreshToken(currentUser, refreshToken, true);
            return Ok(new
            {
                AccessToken = JwtProvider.GenerateJwt(currentUser, roleList),
                RefreshToken = refreshToken
            });

        }

        [HttpPut]
        public async Task<IHttpActionResult> SignUp(SignupViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Dữ liệu không hợp lệ");
            }

            var result = await _userManager.CreateAsync(new Users()
            {
                AvatarUrl = "/Uploads/images/AvatarUsers/DefaultAvatar.jpg",
                BirthDay = DateTime.Now.AddYears(-18),
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                UserName = model.UserName,
                Email = model.Email
            }, model.Password);
            if (result.Succeeded)
            {
                return Ok(new
                {
                    Message = "Tạo tài khoản thành công, vui lòng đăng nhập"
                });
            }

            return BadRequest(String.Join("\n", result.Errors));
        }



        [HttpPost]
        [Route("Refresh-Token")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> RefreshToken(RefreshTokenRequest refreshTokenRequest)
        {

            var user = await _userManager.FindByIdAsync(refreshTokenRequest.UserId);

            if (user?.RefreshToken == null)
            {
                return BadRequest("Invalid user");
            }

            if (string.IsNullOrEmpty(refreshTokenRequest.RefreshToken)
                || !user.RefreshToken.RefreshToken.ToString().ToLower().Equals(refreshTokenRequest.RefreshToken.ToLower()))
            {
                return BadRequest("Invalid refresh token request");
            }

            if (user.RefreshToken.ExpiredTime <= DateTimeOffset.UtcNow)
            {
                var msg = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                throw new HttpResponseException(msg);
            }


            var roleList = await _userManager.GetRolesAsync(user.Id);

            var newAccessToken = JwtProvider.GenerateJwt(user, roleList);
            var newRefreshToken = Guid.NewGuid();

            await SaveRefreshToken(user, newRefreshToken, false);
            return Ok(new
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }

        private Task SaveRefreshToken(Users currentUser, Guid refreshToken, bool shouldUpdateExpirationTime)
        {
            return _userTokenRepository.SaveRefreshToken(currentUser.Id, refreshToken, shouldUpdateExpirationTime);
        }

        //private async Task<string> GenerateOAuthToken(Users user)
        //{
        //    var identity =
        //        await _userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ExternalBearer);
        //    var claimsIdentity = new ClaimsIdentity(identity);

        //    var ticket = new AuthenticationTicket(claimsIdentity, new AuthenticationProperties());

        //    var accessToken = OwinConfig.OAuthAuthorizationOptions.AccessTokenFormat.Protect(ticket);
        //    return accessToken;
        //}
    }
}