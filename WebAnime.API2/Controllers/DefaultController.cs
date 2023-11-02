using System.Web.Http;


namespace WebAnime.API2.Controllers
{

    public class DefaultController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetInfo()
        {
            return Ok(new
            {
                Message="Hello world"
            });
    }
    }
}