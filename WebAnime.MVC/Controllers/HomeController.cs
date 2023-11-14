using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using DataModels.Repository.Interface;
using WebAnime.MVC.Components;

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