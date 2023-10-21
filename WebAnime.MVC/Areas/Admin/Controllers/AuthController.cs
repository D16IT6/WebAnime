using DataModels.EF.Identity;
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
                SignInStatus signInStatus =
                    await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, true);

                switch (signInStatus)
                {
                    case SignInStatus.Success:
                        Users user = await _userManager.FindAsync(model.UserName, model.Password);

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
                        return View("Lockout");
                    case SignInStatus.Failure:
                    default:
                        ModelState.AddModelError("", @"Đăng nhập thất bại, vui lòng thử lại");
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
            return RedirectToAction("Login");
        }
    }
}