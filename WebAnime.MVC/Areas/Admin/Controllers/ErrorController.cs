using System.Web.Mvc;

namespace WebAnime.MVC.Areas.Admin.Controllers
{
    [AllowAnonymous]
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