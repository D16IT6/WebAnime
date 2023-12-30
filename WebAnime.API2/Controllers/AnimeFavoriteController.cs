using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using DataModels.EF;
using DataModels.Repository.Interface;

namespace WebAnime.API2.Controllers
{
    [RoutePrefix("api/AnimeFavorite")]
    [Authorize]
    public class AnimeFavoriteController : ApiController
    {
        private readonly IAnimeFavoriteRepository _animeFavoriteRepository;

        public AnimeFavoriteController(IAnimeFavoriteRepository animeFavoriteRepository)
        {
            _animeFavoriteRepository = animeFavoriteRepository;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetByUserId()
        {
            var check = int.TryParse(ClaimsPrincipal.Current.Claims
                .FirstOrDefault(x => x.Type == ClaimTypes.Sid)?.Value, out var userId);

            if (!check)
            {
                return Unauthorized();
            }

            var wrapper = await _animeFavoriteRepository.GetByUserIdAPI(userId);

            var data = wrapper
                .OrderByDescending(x => x.CreatedDate)
                .Include(x => x.Anime)
                .Include(x => x.Anime.Ratings)
                .Select(x => new
                {
                    x.Id,
                    AnimeId = x.Anime.Id,
                    x.Anime.Title,
                    x.Anime.Poster,
                    Rating = x.Anime.Ratings.Any()
                        ? Math.Round(x.Anime.Ratings.Sum(t => t.RatePoint) / (x.Anime.Ratings.Count * 1.0), 2)
                        : 0
                });

            return Ok(data);
        }

        [HttpPut]
        public async Task<IHttpActionResult> AddNew(int animeId)
        {
            var check = int.TryParse(ClaimsPrincipal.Current.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid)?.Value, out var userId);

            if (!ModelState.IsValid || !check) return BadRequest("Dữ liệu vào không hợp lệ");
            var result = await _animeFavoriteRepository.Create(new Favorites()
            {
                AnimeId = animeId,
                CreatedBy = userId
            });
            if (result)
            {
                return Ok("Thêm mới thành công");
            }
            return BadRequest("Lỗi không xác định, vui lòng thử lại");
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> DeleteItem(int id)
        {
            var check = int.TryParse(ClaimsPrincipal.Current.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid)?.Value, out var userId);
            if (!ModelState.IsValid || !check) return BadRequest("Dữ liệu vào không hợp lệ");

            if (!ModelState.IsValid) return BadRequest("Dữ liệu vào không hợp lệ");
            var result = await _animeFavoriteRepository.Delete(id, userId);
            if (result)
            {
                return Ok("Xoá thành công");
            }
            return BadRequest("Lỗi không xác định, vui lòng thử lại");
        }
    }
}
