using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Client
{
    public class UserChangePasswordViewModel
    {
        public int Id { get; set; }

        [StringLength(32, ErrorMessage = "{0} chỉ dài từ {2} tới {1}", MinimumLength = 2)]
        [Display(Name = "Tên tài khoản")]
        [Required(ErrorMessage = "{0} là bắt buộc")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0} là bắt buộc")]
        [StringLength(32, ErrorMessage = "{0} chỉ dài từ {2} tới {1}", MinimumLength = 2)]
        [Display(Name = "Mật khẩu cũ")]
        [DataType(DataType.Password, ErrorMessage = "{0} không phù hợp")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "{0} là bắt buộc")]
        [StringLength(32, ErrorMessage = "{0} chỉ dài từ {2} tới {1}", MinimumLength = 2)]
        [Display(Name = "Mật khẩu mới")]
        [DataType(DataType.Password, ErrorMessage = "{0} không phù hợp")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "{0} là bắt buộc")]
        [StringLength(32, ErrorMessage = "Mật khẩu chỉ dài từ {2} tới {1}", MinimumLength = 2)]
        [Display(Name = "Nhập lại mật khẩu mới")]
        [DataType(DataType.Password, ErrorMessage = "{0} không phù hợp")]
        [Compare(nameof(NewPassword), ErrorMessage = "{0} không khớp với {1}")]
        public string ReTypePassword { get; set; }

        [StringLength(50, ErrorMessage = "{0} chỉ dài từ {2} tới {1}", MinimumLength = 2)]
        [Display(Name = "Tên đầy đủ")]
        [Required(ErrorMessage = "{0} là bắt buộc")]
        public string FullName { get; set; }
        
        [StringLength(50, ErrorMessage = "{0} chỉ dài từ {2} tới {1}", MinimumLength = 2)]
        [Display(Name = "Địa chỉ email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "{0} phải là email")]
        [Required(ErrorMessage = "{0} là bắt buộc")]
        public string Email { get; set; }
    }
}
