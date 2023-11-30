using System.Web.Mvc;

namespace WebAnime.MVC.Areas.Admin.Controllers
{
    [Authorize]
    public class ErrorController : Controller
    {
        // GET: Admin/Error
        [HandleError]
        public ActionResult NotFound()
        {
            return View();
        }
    }
}