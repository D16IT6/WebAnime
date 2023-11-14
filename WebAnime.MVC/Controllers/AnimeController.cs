using System.Threading.Tasks;
using System.Web.Mvc;
using DataModels.Repository.Interface;

namespace WebAnime.MVC.Controllers
{
    public class AnimeController : Controller
    {
        private readonly IAnimeRepository _animeRepository;

        public AnimeController(IAnimeRepository animeRepository)
        {
            _animeRepository = animeRepository;
        }

        public async Task<ActionResult> Index()
        {
            return await Task.FromResult(View());
        }

        public async Task<ActionResult> Detail(int id)
        {
            var anime = await _animeRepository.GetAnimeDetail(id);
            return await Task.FromResult(View(anime));
        }

        [ChildActionOnly]
        public async Task<ActionResult> TrendingPartial()
        {
            var animeTrending = await _animeRepository.GetAnimeTrending();
            return await Task.FromResult(PartialView(animeTrending));
        }

        [ChildActionOnly]
        public async Task<ActionResult> RecenlyPartial()
        {
            var animeRecenly = await _animeRepository.GetAnimeRecenly();
            return await Task.FromResult(PartialView(animeRecenly));

        }

        public ActionResult Watch(int animeId, int serverId)
        {
            return View();
        }

}
}