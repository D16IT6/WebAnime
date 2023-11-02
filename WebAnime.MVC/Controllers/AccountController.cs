using DataModels.EF.Identity;
using DataModels.Helpers;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebAnime.MVC.Components;
using WebAnime.MVC.Helpers;
using WebAnime.MVC.Models;

namespace WebAnime.MVC.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager _userManager;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly SignInManager<Users, int> _signInManager;

        public AccountController(IAuthenticationManager authenticationManager, SignInManager<Users, int> signInManager, UserManager userManager)
        {
            _authenticationManager = authenticationManager;
            _signInManager = signInManager;
            _userManager = userManager;

            OwinConfig.RegisterTokenService(userManager);
        }

        public async Task<ActionResult> Index()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("User"))
            {
                return new HttpUnauthorizedResult("Already login, please go back");
            }
            return await Task.FromResult(View());
        }
        [HttpGet]
        public async Task<ActionResult> Login()
        {
            return await Task.FromResult(View());

        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            string returnUrl = Request.QueryString["returnUrl"] ?? "";

            if (ModelState.IsValid)
            {
                Users user = await _userManager.FindByNameAsync(model.UserName);

                if (user != null)
                {
                    if (user.AccessFailedCount == AuthConstants.MaxFailedAccessAttemptsBeforeLockout - 1)
                    {
                        await _userManager.SetLockoutEnabledAsync(user.Id, true);
                    }
                }
                else
                {
                    int loginFailCount = (int)(Session[SessionConstants.LoginFailCount] ?? 0);

                    if (loginFailCount == AuthConstants.MaxFailedAccessAttemptsBeforeLockout - 1)
                    {
                        ModelState.AddModelError("FakeLogin", @"Bạn đang cố đăng nhập vì điều gì?");
                        ModelState.AddModelError("Hint", @"Chưa có tài khoản? Hãy tạo tài khoản mới");
                        ModelState.AddModelError("AdminFb", @"Liên hệ facebook: https://facebook.com/vuthemanh1707");
                        Session.Remove(SessionConstants.LoginFailCount);

                        return View();
                    }
                    ModelState.AddModelError("",
                        $@"Đăng nhập thất bại, vui lòng thử lại (còn {AuthConstants.MaxFailedAccessAttemptsBeforeLockout - 1 - loginFailCount} lượt)");

                    loginFailCount++;
                    Session[SessionConstants.LoginFailCount] = loginFailCount;

                    return View(model);

                }
                SignInStatus signInStatus =
                    await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, true);

                switch (signInStatus)
                {

                    case SignInStatus.Success:
                        Session.Remove(SessionConstants.LoginFailCount);
                        await _userManager.SetLockoutEnabledAsync(user.Id, false);
                        await _userManager.ResetAccessFailedCountAsync(user.Id);
                        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }


                        return RedirectToAction("Index", "Home");
                    case SignInStatus.LockedOut:
                        ModelState.AddModelError("LockoutMessage",
                            $@"Tài khoản của bạn đã bị khóa do đăng nhập sai quá {AuthConstants.MaxFailedAccessAttemptsBeforeLockout} lần hoặc bị admin khóa.");
                        ModelState.AddModelError("LogoutHint", $@"Vui lòng thử lại sau {AuthConstants.LockoutMinutes} phút hoặc liên hệ admin.");
                        return View();

                    case SignInStatus.Failure:
                    default:
                        ModelState.AddModelError("",
                            $@"Đăng nhập thất bại, vui lòng thử lại (còn {AuthConstants.MaxFailedAccessAttemptsBeforeLockout - 1 - user.AccessFailedCount} lượt)");
                        return View(model);
                }

            }
            ModelState.AddModelError("", @"Đầu vào chưa hợp lệ");
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> LoginFacebook()
        {
            var loginInfo = await _authenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                var authenticationProperties = new AuthenticationProperties { RedirectUri = "/login-facebook" }; // Điều chỉnh RedirectUri theo ứng dụng của bạn
                var authenticationType = "Facebook"; // Điều chỉnh tên phương thức đăng nhập theo cấu hình của bạn
                var facebookLoginUrl = Url.Action("ExternalLogin", "Account", new { provider = authenticationType, returnUrl = authenticationProperties.RedirectUri });

                // Sử dụng facebookLoginUrl để tạo một liên kết đến trang đăng nhập Facebook.
            }

            return View();
        }

    }
}