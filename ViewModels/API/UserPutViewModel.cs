using System.IO;
using System.Net.Http;
using System.Web;
namespace ViewModels.API
{
    public class UserPutViewModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
