
using System.Threading.Tasks;
using System.Web.Mvc;
using DataModels.EF.Identity;
using Microsoft.AspNet.Identity;

namespace WebAnime.MVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class HomeController : Controller
    {
        private readonly UserManager _userManager;
        public HomeController(UserManager userManager)
        {
            _userManager = userManager;
            OwinConfig.RegisterTokenService(_userManager);
        }

        // GET: Admin/Home
        public async Task<ActionResult> Index()
        {
            var userId = int.Parse(User.Identity.GetUserId());
            var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(userId);
            var changePhoneNumberToken = await _userManager.GenerateChangePhoneNumberTokenAsync(userId, "0123456789");
            var passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(userId);

            ViewBag.EmailConfirmationToken = emailConfirmationToken;
            ViewBag.ChangePhoneNumberToken = changePhoneNumberToken;
            ViewBag.PasswordResetToken = passwordResetToken;

            return View();
        }
    }
}
