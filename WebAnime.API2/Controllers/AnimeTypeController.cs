using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using DataModels.Repository.Interface;


namespace WebAnime.API2.Controllers
{
    [Authorize]
    public class AnimeTypeController : ApiController
    {
        private readonly ITypeRepository _typeRepository;

        public AnimeTypeController(ITypeRepository typeRepository)
        {
            this._typeRepository = typeRepository;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            var data = await _typeRepository.GetAll();
            return Ok(data.Select(x => new { x.Id, x.Name }));
        }
    }
}