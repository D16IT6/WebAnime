using Microsoft.Owin;
using Owin;
using static WebAnime.MVC.OwinConfig;
[assembly: OwinStartup(typeof(WebAnime.MVC.Startup))]

namespace WebAnime.MVC
{
    public class Startup
    {
        //private RoleManager _roleManager;
        //private UserManager _userManager;
        public void Configuration(IAppBuilder app)
        {
            AuthConfig(app);

            //RegisterServices();
            //CreateDefaultRoles();
            //CreateDefaultUsers();
        }
        //private void RegisterServices()
        //{
        //    _roleManager = NinjectConfig.GetService<RoleManager>();
        //    _userManager = NinjectConfig.GetService<UserManager>();
        //}
        //public void CreateDefaultRoles()
        //{
        //    _roleManager
        //        .CreateRoleIfNotExist("adMIN")
        //        .CreateRoleIfNotExist("MANAgeR")
        //        .CreateRoleIfNotExist("User");
        //}

        //public void CreateDefaultUsers()
        //{
        //    var adminUser = new Users()
        //    {
        //        UserName = "talonezio",
        //        BirthDay = new DateTime(2003, 7, 17),
        //        Email = "vuthemanh1707@gmail.com",
        //        PhoneNumber = "0988344814"
        //    };
        //    var managerUser = new Users()
        //    {
        //        UserName = "vuthemanh1707",
        //        BirthDay = new DateTime(2003, 7, 17),
        //        Email = "vuthemanh1707@gmail.com",
        //        PhoneNumber = "0988344814"
        //    };

        //    var defaultPassword = "manhngu123";

        //    _userManager
        //        .CreateUserIfNotExist(adminUser, defaultPassword, "Admin")
        //        .CreateUserIfNotExist(managerUser, defaultPassword, "Manager");
        //}
    }
}
