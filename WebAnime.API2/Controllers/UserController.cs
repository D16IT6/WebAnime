using AutoMapper;
using DataModels.EF.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;
using ViewModels.Admin;

namespace WebAnime.API2.Controllers
{
    public class UserController : ApiController
    {
        private readonly UserManager _userManager;
        private readonly IMapper _mapper;
        public UserController(UserManager userManager, IMapper mapper)
        {
            _userManager = userManager;
            OwinConfig.RegisterTokenService(_userManager);
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IHttpActionResult> Get()
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
        public async Task<IHttpActionResult> GetAbc(int id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);

            var userId = user?.Id ?? 1;
            var userViewModel =
                _mapper.Map<UserViewModel>(user);
            var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(userId);
            var changePhoneNumberToken = await _userManager.GenerateChangePhoneNumberTokenAsync(userId, "0123456789");
            var passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(userId);

            return Ok(new
            {
                success = true,
                user = new
                {
                    data = userViewModel,
                    emailConfirmationToken,
                    changePhoneNumberToken,
                    passwordResetToken
                }
            });
        }
    }
}
