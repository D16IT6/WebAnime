using Microsoft.AspNet.Identity;

namespace DataModels.EF.Identity
{
    public class UserManager : UserManager<Users, int>
    {
        public UserManager(IUserStore<Users, int> store) : base(store)
        {

        }
    }
}
