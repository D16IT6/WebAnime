using System;
using System.Linq;
using System.Web.Mvc;

namespace WebAnime.MVC.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult NotFound()
        {
            return View();
        }
    }
}