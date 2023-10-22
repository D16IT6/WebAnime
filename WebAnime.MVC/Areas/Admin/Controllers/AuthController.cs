using DataModels.EF.Identity;
using DataModels.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebAnime.MVC.Areas.Admin.Models;
using WebAnime.MVC.Helpers;
using WebAnime.MVC.Helpers.Session;

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
        }

        [HttpGet]
        public async Task<ActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return new HttpNotFoundResult("Aready login");
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
                        if (Session[SessionConstants.UserLogin] == null)
                        {
                            Session.Add(SessionConstants.UserLogin, new UserSession()
                            {
                                Id = user.Id,
                                FullName = user.FullName,
                                UserName = user.UserName,
                            });
                        }

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

        public ActionResult LogOut()
        {
            _authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            Session.Remove(SessionConstants.UserLogin);
            return RedirectToAction("Login");
        }
    }
}