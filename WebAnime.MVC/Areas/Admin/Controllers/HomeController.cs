
using System.Threading.Tasks;
using System.Web.Mvc;
using DataModels.EF.Identity;
using Microsoft.AspNet.Identity;

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
