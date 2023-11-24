using System.Web.Mvc;
using WebAnime.MVC.Components;

namespace WebAnime.MVC.Areas.Admin.Controllers
{
    [AdminAreaAuthorize]

    public class HomeController : Controller
    {

        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}
