using System.Web.Mvc;
using WebAnime.MVC.Components;

namespace WebAnime.MVC.Controllers
{
    [UserAuthorize]

    public class HomeController : Controller
    {
        // GET: Home

        public ActionResult Index()
        {
            return View();
        }
    }
}