using System.ComponentModel.DataAnnotations;

namespace ViewModels.Admin
{
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
        [Compare("Password", ErrorMessage = "{0} phải khớp với {1}")]
        public string ConfirmPassword { get; set; }
    }
}