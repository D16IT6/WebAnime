using AutoMapper;
using DataModels.EF.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;
using ViewModels.Admin;


namespace WebAnime.API2.Controllers
{
    [RoutePrefix("api/User")]
    [Authorize]
    public class UserController : ApiController
    {
        private readonly UserManager _userManager;
        private readonly IMapper _mapper;
        public UserController(UserManager userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IHttpActionResult> GetAllUsers()
        {
            var userList = await _userManager.Users.AsNoTracking().ToListAsync();

            var userViewModelList =
                _mapper.Map<List<Users>, IEnumerable<UserViewModel>>(userList);

            return Ok(new
            {
                success = true,
                userList = userViewModelList
            });
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetUserById(int id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null) return BadRequest("Cannot find user");
            return Ok(new
            {
                user.Id,
                user.FullName,
                user.Email,
                user.AvatarUrl,
                user.PhoneNumber,
                user.BirthDay,
            });
        }


    }
}
