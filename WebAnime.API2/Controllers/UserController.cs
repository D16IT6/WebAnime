
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using DataModels.EF.Identity;
using Microsoft.AspNet.Identity;
using ViewModels.Admin;

namespace WebAnime.API2.Controllers
{
    public class UserController : ApiController
    {
        private readonly UserManager _userManager;
        private readonly IMapper _mapper;

        public UserController()
        {
            _userManager = NinjectConfig.GetService<UserManager>();
            _mapper = NinjectConfig.GetService<IMapper>();
        }
        public UserController(UserManager userManager)
        {
            _userManager = userManager;
            OwinConfig.RegisterTokenService(_userManager);
        }
        [HttpGet]
        public IHttpActionResult Get()
        {
            var userList = _userManager.Users;
            var userViewModelList =
                _mapper.Map<IQueryable<Users>, IEnumerable<UserViewModel>>(userList);
            return Ok(new
            {
                success = true,
                userList = userViewModelList
            });
        }
    }
}
