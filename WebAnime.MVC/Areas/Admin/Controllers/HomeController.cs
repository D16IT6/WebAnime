
using System.Web.Mvc;

namespace WebAnime.MVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}