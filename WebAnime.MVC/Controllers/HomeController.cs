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
        public async Task<ActionResult> Contact()
        {
            return await Task.FromResult(View());
        }
    }
}