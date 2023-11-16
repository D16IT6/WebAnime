using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ViewModels.Client
{
    public class ExternalLoginConfirmationViewModel
    {

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
        [Compare("Password", ErrorMessage = "{0} không khớp với {1}")]
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

        [Display(Name = "Ảnh đại diện")]
        [Required(ErrorMessage = "{0} là bắt buộc")]
        [RegularExpression("^(https?://(localhost|[\\w\\-]+(\\.[\\w\\-]+)+)(:\\d+)?(/[\\w\\-]+)+|/[\\w\\-]+)+\\.[a-zA-Z]{2,4}$", ErrorMessage = "{0} không hợp lệ, chỉ cho phép url hoặc đường dẫn ảnh trực tiếp")]
        [MaxLength(200, ErrorMessage = "{0} dài tối đa {1} ký tự")]
        public string AvatarUrl { get; set; }

        [Display(Name = "Quyền")]
        [Required(ErrorMessage = "{0} là bắt buộc")]
        public int[] RoleListIds { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }


    public class RegisterViewModel
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
        [Compare("Password", ErrorMessage = "{0} không khớp với {1}")]
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

        [Display(Name = "Ảnh đại diện")]
        [Required(ErrorMessage = "{0} là bắt buộc")]
        [MaxLength(200, ErrorMessage = "{0} dài tối đa {1} ký tự")]
        public string AvatarUrl { get; set; }

        [Display(Name = "Quyền")]
        [Required(ErrorMessage = "{0} là bắt buộc")]
        public int[] RoleListIds { get; set; }

    }

    public class ResetPasswordViewModel
    {
        public int UserId { get; set; }
        public string ResetCode { get; set; }

        [Required(ErrorMessage = "{0} là bắt buộc")]
        [StringLength(32, ErrorMessage = "{0} chỉ dài từ {2} tới {1}", MinimumLength = 2)]
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password, ErrorMessage = "{0} không phù hợp")]
        public string Password { get; set; }

        [Required(ErrorMessage = "{0} là bắt buộc")]
        [StringLength(32, ErrorMessage = "Mật khẩu chỉ dài từ {2} tới {1}", MinimumLength = 2)]
        [Display(Name = "Mật khẩu xác nhận")]
        [DataType(DataType.Password, ErrorMessage = "{0} không phù hợp")]
        public string ConfirmPassword { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "{0} là bắt buộc")]
        [EmailAddress(ErrorMessage = "{0} không hợp lệ")]
        [StringLength(32, ErrorMessage = "{0} chỉ dài từ {2} tới {1} ký tự", MinimumLength = 6)]
        [Display(Name = "Địa chỉ email")]
        public string Email { get; set; }
    }
}
