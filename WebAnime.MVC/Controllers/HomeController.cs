using System.Threading.Tasks;
using System.Web.Mvc;

namespace WebAnime.MVC.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            return await Task.FromResult(View());
        }
    }
}