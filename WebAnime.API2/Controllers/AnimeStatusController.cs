using DataModels.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebAnime.API2.Controllers
{
    [Authorize]
    public class AnimeStatusController : ApiController
    {
        private readonly IStatusRepository _statusRepository;

        public AnimeStatusController(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            var data = await _statusRepository.GetAll();
            return Ok(data.Select(x => new { x.Id, x.Name }));
        }
    }
}
