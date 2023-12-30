using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.API
{
    public class SignupViewModel
    {
        [StringLength(32)]
        public string UserName { get; set; }
        [StringLength(32)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [StringLength(32)]
        [EmailAddress]
        public string Email { get; set; }
    }
}
