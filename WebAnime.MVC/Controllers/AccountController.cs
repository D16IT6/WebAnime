using AutoMapper;
using DataModels.EF.Identity;
using DataModels.Helpers;
using DataModels.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ViewModels.Client;
using WebAnime.MVC.Components;

namespace WebAnime.MVC.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager _userManager;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly SignInManager<Users, int> _signInManager;
        private readonly RoleManager _roleManager;
        private readonly IMapper _mapper;

        public AccountController(IAuthenticationManager authenticationManager, SignInManager<Users, int> signInManager, UserManager userManager, RoleManager roleManager, IMapper mapper)
        {
            _authenticationManager = authenticationManager;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("User"))
            {
                return new HttpUnauthorizedResult("Already login, please go back");
            }
            return await Task.FromResult(View());
        }

        [UserAuthorize]
        public async Task<ActionResult> Info()
        {
            var userEmail = await _userManager.GetEmailAsync(User.Identity.GetUserId<int>());
            ViewBag.UserEmail = userEmail;
            return await Task.FromResult(View());
        }

        [HttpPost]
        public async Task<ActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                _authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                return await Task.FromResult<ActionResult>(RedirectToAction("Login"));
            }
            return await Task.FromResult(RedirectToAction("NotFound", "Error"));
        }
        [HttpGet]
        public async Task<ActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return await Task.FromResult(RedirectToAction("NotFound", "Error"));
            }
            return await Task.FromResult(View());

        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            string returnUrl = Request.QueryString["returnUrl"] ?? string.Empty;

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
                    int loginFailCount = (int)(Session[CommonConstants.LoginFailCount] ?? 0);

                    if (loginFailCount == AuthConstants.MaxFailedAccessAttemptsBeforeLockout - 1)
                    {
                        ModelState.AddModelError("FakeLogin", @"Bạn đang cố đăng nhập vì điều gì?");
                        ModelState.AddModelError("Hint", @"Chưa có tài khoản? Hãy tạo tài khoản mới");
                        ModelState.AddModelError("AdminFb", @"Liên hệ facebook: https://facebook.com/vuthemanh1707");
                        Session.Remove(CommonConstants.LoginFailCount);

                        return View();
                    }
                    ModelState.AddModelError(string.Empty,
                        $@"Đăng nhập thất bại, vui lòng thử lại (còn {AuthConstants.MaxFailedAccessAttemptsBeforeLockout - 1 - loginFailCount} lượt)");

                    loginFailCount++;
                    Session[CommonConstants.LoginFailCount] = loginFailCount;

                    return View(model);

                }
                SignInStatus signInStatus =
                    await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, true);

                switch (signInStatus)
                {
                    case SignInStatus.Success:
                        Session.Remove(CommonConstants.LoginFailCount);
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
                        return View(model);

                    case SignInStatus.RequiresVerification:
                        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, model.RememberMe });

                    case SignInStatus.Failure:
                    default:
                        ModelState.AddModelError(string.Empty,
                            $@"Đăng nhập thất bại, vui lòng thử lại (còn {AuthConstants.MaxFailedAccessAttemptsBeforeLockout - 1 - user.AccessFailedCount} lượt)");
                        return View(model);
                }

            }
            ModelState.AddModelError(string.Empty, @"Đầu vào chưa hợp lệ");
            return View(model);
        }

        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            if (!await _signInManager.HasBeenVerifiedAsync())
            {
                return RedirectToAction("NotFound", "Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await _signInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError(string.Empty, @"Invalid code.");
                    return View(model);
            }
        }


        [AllowAnonymous]
        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                _authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                return RedirectToAction("Login");
            }
            var userRole = _roleManager.Roles.FirstOrDefault(x => x.Name.ToLower().Equals("user"))?.Id ?? 3;

            return View(new RegisterViewModel()
            {
                RoleListIds = new[] { userRole }
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var uploadImage = Request.Files["AvatarFile"];
                model.AvatarUrl = HandleFile(uploadImage);
                var user = _mapper.Map<Users>(model);

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {

                    await _signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    
                    string code = await _userManager.GenerateEmailConfirmationTokenAsync(user.Id);


                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code },
                        protocol: Request.Url?.Scheme ?? "http");

                    StringBuilder bodyBuilder = new StringBuilder();
                    bodyBuilder.AppendLine(
                        $"<p>Xin chào thành viên mới, bạn đã yêu cầu tạo tài khoản!</p>");
                    bodyBuilder.AppendLine(
                        $"<p>Để xác thực tài khoản mới, vui lòng bấm vào <a href=\"{callbackUrl}\"><strong>Đây</strong></a>");
                    bodyBuilder.AppendLine("<p>Thư sẽ hết hạn sau 1 giờ.</p>");
                    bodyBuilder.AppendLine("<h3>Cảm ơn bạn!</h3>");

                    bool isSendEmail = await EmailService.SendMailAsync(new IdentityMessage()
                    {
                        Body = bodyBuilder.ToString(),
                        Destination = user.Email,
                        Subject = "Xác nhận tài khoản"
                    });

                    if (isSendEmail)
                    {
                        return RedirectToAction("VerifyEmailConfirmation", "Account");
                    }

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> VerifyEmailConfirmation()
        {
            return await Task.FromResult(View());
        }

        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(int userId, string code)
        {
            if (code == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            var result = await _userManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        [AllowAnonymous]
        public ActionResult ForgotPassword(string email)
        {

            return View(new ForgotPasswordViewModel() { Email = email ?? "" });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    //return View("ForgotPasswordConfirmation");
                    ModelState.AddModelError("NotFoundUser", @"Không thấy user");
                    return View(model);
                }

                if (!(await _userManager.IsEmailConfirmedAsync(user.Id)))
                {
                    ModelState.AddModelError("NoConfirmEmail", @"Email chưa được xác nhận");
                    return View(model);
                }

                if (user.IsDeleted)
                {
                    ModelState.AddModelError("DeletedAccount",
                        @"Tài khoản đã bị xóa khỏi hệ thống, vui lòng liên hệ admin để cập nhật");
                    return View(model);
                }

                string resetCode = await _userManager.GeneratePasswordResetTokenAsync(user.Id);

                if (Request.Url != null)
                {
                    var callbackUrl = Url.Action("ResetPassword", "Account",
                        new { userId = user.Id, resetCode },
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
                        return RedirectToAction("ForgotPasswordConfirmation", "Account",
                            new { fromForgot = true });
                    }
                }
            }

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
                    return RedirectToAction("Login", "Home");
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
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { returnUrl }));
        }

        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await _signInManager.GetVerifiedUserIdAsync();
            if (userId == 0)
            {
                return RedirectToAction("NotFound", "Error");
            }
            var userFactors = await _userManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!await _signInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return RedirectToAction("NotFound", "Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, model.ReturnUrl, model.RememberMe });
        }


        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await _authenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await _signInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;

                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel()
                    {
                        UserName = loginInfo.Email,
                        FullName = loginInfo.DefaultUserName,
                        Email = loginInfo.Email,
                        AvatarUrl = CommonConstants.DefaultAvatarUrl,
                        Password = "1234567",//Fake
                        ReTypePassword = "1234567"
                    });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            var userRole = _roleManager.Roles.FirstOrDefault(x => x.Name.ToLower().Equals("user"))?.Id ?? 3;
            model.RoleListIds = new[] { userRole };

            if (ModelState.IsValid)
            {
                var info = await _authenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }

                var uploadImage = Request.Files["AvatarFile"];
                model.AvatarUrl = HandleFile(uploadImage);

                var user = _mapper.Map<Users>(model);
                var result = await _userManager.CreateAsync(user);

                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            _authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }
        }

        private string HandleFile(HttpPostedFileBase uploadImage)
        {
            if (uploadImage is { ContentLength: > 0 })
            {
                var folderPath = Server.MapPath("~/Uploads/images/AvatarUsers");
                uploadImage.SaveAs(Path.Combine(folderPath, uploadImage.FileName));
                return ("\\" + Path.Combine("Uploads", "Images", "AvatarUsers", uploadImage.FileName)).Replace('\\', '/');
            }
            return CommonConstants.DefaultAvatarUrl;
        }
    }
}