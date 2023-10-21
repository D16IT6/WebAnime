using DataModels.EF.Identity;
using DataModels.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
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
                        //user.LockoutEnabled = true;
                        //user.LockoutEndDateUtc = DateTime.Now.AddMinutes(AuthConstants.LockoutMinutes);

                        await _userManager.SetLockoutEnabledAsync(user.Id, true);
                        await _userManager.SetLockoutEndDateAsync(user.Id, DateTime.Now.AddMinutes(AuthConstants.LockoutMinutes));
                        ModelState.AddModelError("", $@"Tài khoản của bạn đã bị khóa do đăng nhập sai quá {AuthConstants.MaxFailedAccessAttemptsBeforeLockout} lần. 
Vui lòng thử lại sau {AuthConstants.LockoutMinutes} phút.");
                        return View();

                    }
                }

                SignInStatus signInStatus =
                    await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, true);

                switch (signInStatus)
                {
                    case SignInStatus.Success:

                        if (Session[SessionConstants.USER_LOGIN] == null)
                        {
                            Session.Add(SessionConstants.USER_LOGIN, new UserSession()
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
                        ModelState.AddModelError("", @"Tài khoản của bạn đã bị khóa do đăng nhập sai quá nhiều lần. Vui lòng thử lại sau 5 phút.");

                        return View("Lockout");
                    case SignInStatus.Failure:
                    default:
                        ModelState.AddModelError("", $@"Đăng nhập thất bại, vui lòng thử lại (còn {AuthConstants.MaxFailedAccessAttemptsBeforeLockout - 1 - user.AccessFailedCount} lượt)");
                        return View(model);
                }

            }
            ModelState.AddModelError("", @"Đầu vào chưa hợp lệ");
            return View(model);
        }

        public ActionResult LockOut()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            _authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            Session.Remove(SessionConstants.USER_LOGIN);
            return RedirectToAction("Login");
        }
    }
}