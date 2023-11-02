using AutoMapper;
using DataModels.APINetCore.Repository.Interface;
using DataModels.EF.Identity;
using Microsoft.AspNetCore.Mvc;
using ViewModels.Admin;

namespace WebAnime.APINetCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GettAllUsers()
        {
            var data = await _userRepository.GetAll();
            var dataViewModel = _mapper.Map<IEnumerable<Users>, IEnumerable<UserViewModel>>(data);
            return Ok(dataViewModel.Select(x => new
            {
                x.Id,
                x.UserName,
                x.AvatarUrl,
                x.BirthDay,
                x.FullName,
                x.Email,
                x.PhoneNumber,
                x.RoleList,
                x.RoleListIds
            }));
        }
    }
}
