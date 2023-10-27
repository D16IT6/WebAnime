using DataModels.EF.Identity;
using DataModels.Helpers;
using DataModels.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebAnime.MVC.Areas.Admin.Models;
using WebAnime.MVC.Helpers;

namespace WebAnime.MVC.Areas.Admin.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly UserManager _userManager;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly SignInManager<Users, int> _signInManager;

        public AuthController(IAuthenticationManager authenticationManager, SignInManager<Users, int> signInManager, UserManager userManager)
        {
            _authenticationManager = authenticationManager;
            _signInManager = signInManager;
            _userManager = userManager;

            var dataProtectorProvider = OwinConfig.DataProtectionProvider;

            var provider = dataProtectorProvider.Create("WebAnime.MVC.ResetPasswordToken");
            _userManager.UserTokenProvider = new DataProtectorTokenProvider<Users, int>(provider)
            {
                TokenLifespan = TimeSpan.FromHours(1)
            };
        }

        [HttpGet]
        public async Task<ActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            return await Task.FromResult<ActionResult>(View());
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
                        ModelState.AddModelError("Hint", @"Chưa có tài khoản? Hãy liên hệ admin để được cấp");
                        ModelState.AddModelError("AdminFb", @"Facebook: https://facebook.com/vuthemanh1707");
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

                        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }


                        return RedirectToAction("Index", "Home");
                    case SignInStatus.LockedOut:
                        ModelState.AddModelError("LockoutMessage",
                            $@"Tài khoản của bạn đã bị khóa do đăng nhập sai quá {AuthConstants.MaxFailedAccessAttemptsBeforeLockout} lần hoặc bị admin khóa.");
                        ModelState.AddModelError("LogoutHint", $@"Vui lòng thử lại sau {AuthConstants.LockoutMinutes} phút. hoặc liên hệ admin.");
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
        [Authorize]
        public async Task<ActionResult> LogOut()
        {
            _authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return await Task.FromResult<ActionResult>(RedirectToAction("Login"));
        }

        [HttpGet]
        public async Task<ActionResult> ForgotPassword()
        {
            return await Task.FromResult<ActionResult>(View());
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user.Id)))
                {
                    return View("ForgotPasswordConfirmation");
                }
                string resetCode = await _userManager.GeneratePasswordResetTokenAsync(user.Id);

                if (Request.Url != null)
                {
                    var callbackUrl = Url.Action("ResetPassword", "Auth", new { area = "Admin", userId = user.Id, resetCode },
                        protocol: Request.Url.Scheme);


                    StringBuilder bodyBuilder = new StringBuilder();
                    bodyBuilder.AppendLine(
                        $"<p>Xin chào <strong>{user.FullName}</strong>, bạn đã yêu cầu khôi phục mật khẩu!</p>");
                    bodyBuilder.AppendLine(
                        $"<p>Để khôi phục mật khẩu của bạn, vui lòng bấm vào <a href=\"{callbackUrl}\">Đây</a>");
                    bodyBuilder.AppendLine("<p>Thư sẽ hết hạn sau 1 giờ.</p>");
                    bodyBuilder.AppendLine("<h3>Cảm ơn bạn!</h3>");
                    bool isSendEmail = await EmailService.SendMailAsync(new IdentityMessage()
                    {
                        Body = bodyBuilder.ToString(),
                        Destination = user.Email,
                        Subject = "Xác nhận khôi phục mật khẩu"
                    });
                    if (isSendEmail)
                    {
                        return RedirectToAction("ForgotPasswordConfirmation", "Auth", new { area = "Admin", fromForgot = true });
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> ResetPassword(int userId, string resetCode)
        {
            if (resetCode == null)
                return new HttpNotFoundResult();

            return await Task.FromResult(View(new ResetPasswordViewModel()
            {
                UserId = userId,
                ResetCode = resetCode
            }));
        }

        [HttpPost]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!model.Password.Equals(model.ConfirmPassword))
                {
                    ModelState.AddModelError("PasswordError", @"Mật khẩu xác nhận không đúng, vui lòng thử lại");
                    return View(model);
                }
                IdentityResult result = await _userManager.ResetPasswordAsync(model.UserId, model.ResetCode, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Auth", new { area = "Admin" });
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error, error);
                }
                return View(model);
            }
            ModelState.AddModelError("TotalError", @"Có lỗi xảy ra, vui lòng thử lại");
            return View(model);
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> ForgotPasswordConfirmation(bool? fromForgot)
        {
            if (fromForgot.HasValue && fromForgot.Value)
                return await Task.FromResult(View());
            return RedirectToAction("NotFound", "Error");
        }
    }
}