using System;
using System.ComponentModel.DataAnnotations;

namespace WebAnime.MVC.Areas.Admin.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [StringLength(32, ErrorMessage = "{0} chỉ dài từ {2} tới {1}", MinimumLength = 2)]
        [Display(Name = "Tên tài khoản")]
        [Required(ErrorMessage = "{0} là bắt buộc")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0} là bắt buộc")]
        [StringLength(32, ErrorMessage = "{0} chỉ dài từ {2} tới {1}", MinimumLength = 2)]
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password, ErrorMessage = "{0} không phù hợp")]
        public string Password { get; set; }

        [Required(ErrorMessage = "{0} là bắt buộc")]
        [StringLength(32, ErrorMessage = "Mật khẩu chỉ dài từ {2} tới {1}", MinimumLength = 2)]
        [Display(Name = "Nhập lại mật khẩu")]
        [DataType(DataType.Password, ErrorMessage = "{0} không phù hợp")]
        public string ReTypePassword { get; set; }

        [StringLength(50, ErrorMessage = "{0} chỉ dài từ {2} tới {1}", MinimumLength = 2)]
        [Display(Name = "Tên đầy đủ")]
        [Required(ErrorMessage = "{0} là bắt buộc")]
        public string FullName { get; set; }

        [Display(Name = "Ngày sinh")]
        [Required(ErrorMessage = "{0} là bắt buộc")]
        public DateTime BirthDay { get; set; }

        [StringLength(50, ErrorMessage = "{0} chỉ dài từ {2} tới {1}", MinimumLength = 2)]
        [Display(Name = "Địa chỉ email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "{0} phải là email")]
        [Required(ErrorMessage = "{0} là bắt buộc")]
        public string Email { get; set; }

        [StringLength(50, ErrorMessage = "{0} chỉ dài từ {2} tới {1}", MinimumLength = 2)]
        [Display(Name = "Số điện thoại")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "{0} phải là số điện thoại")]
        [Required(ErrorMessage = "{0} là bắt buộc")]

        public string PhoneNumber { get; set; }

        [Display(Name = "Bị khóa")]
        public bool LockoutEnabled { get; set; }

        [Display(Name = "Bị khóa tới")]
        public DateTime? LockoutEndDateUtc { get; set; }

        [Display(Name = "Quyền")]
        [Required(ErrorMessage = "{0} là bắt buộc")]
        public int[] RoleListIds { get; set; }

        [Display(Name = "Quyền")]
        public string[] RoleList { get; set; }
    }
}