
using System.Web.Mvc;

namespace WebAnime.API2.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return Redirect("swagger");
        }
    }
}