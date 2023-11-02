using AutoMapper;
using DataModels.APINetCore.Repository.Interface;
using DataModels.EF;
using DataModels.EF.Identity;
using Microsoft.AspNetCore.Mvc;
using ViewModels.Admin;

namespace WebAnime.APINetCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnimeController : Controller
    {
        private readonly IAnimeRepository _animeRepository;
        private readonly IMapper _mapper;
        public AnimeController(IAnimeRepository animeRepository, IMapper mapper)
        {
            _animeRepository = animeRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAnime()
        {
            var data = await _animeRepository.GetAll();
            var dataViewModel = _mapper.Map<IEnumerable<Animes>, IEnumerable<AnimeViewModel>>(data);

            return Ok(dataViewModel);
        }
    }
}
