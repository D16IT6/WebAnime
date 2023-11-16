using DataModels.EF.Identity;
using DataModels.Helpers;
using Microsoft.Owin;
using Owin;
using System;
using static WebAnime.MVC.OwinConfig;
[assembly: OwinStartup(typeof(WebAnime.MVC.Startup))]

namespace WebAnime.MVC
{
    public class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            AuthConfig(app);

            RegisterServices();
            //CreateDefaultRoles();
            //CreateDefaultUsers();
        }


        RoleManager _roleManager;
        UserManager _userManager;
        void RegisterServices()
        {
            _roleManager = NinjectConfig.GetService<RoleManager>();
            _userManager = NinjectConfig.GetService<UserManager>();
        }
        void CreateDefaultRoles()
        {
            _roleManager
                .CreateRoleIfNotExist("adMIN")
                .CreateRoleIfNotExist("MANAgeR")
                .CreateRoleIfNotExist("User");
        }

        void CreateDefaultUsers()
        {
            var adminUser = new Users()
            {
                UserName = "talonezio",
                BirthDay = new DateTime(2003, 7, 17),
                Email = "vuthemanh1707@gmail.com",
                PhoneNumber = "0988344814",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            var managerUser = new Users()
            {
                UserName = "vuthemanh1707",
                BirthDay = new DateTime(2003, 7, 17),
                Email = "vuthemanh333@gmail.com",
                PhoneNumber = "0988344814",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var defaultPassword = "manhngu123";

            _userManager
                .CreateUserIfNotExist(adminUser, defaultPassword, "Admin")
                .CreateUserIfNotExist(managerUser, defaultPassword, "Manager");
        }
    }
}
