using System.ComponentModel.DataAnnotations;

namespace ViewModels.Admin
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "{0} là bắt buộc")]
        [EmailAddress(ErrorMessage = "{0} không hợp lệ")]
        [StringLength(32, ErrorMessage = "{0} chỉ dài từ {2} tới {1} ký tự", MinimumLength = 6)]
        [Display(Name = "Địa chỉ email")]
        public string Email { get; set; }
    }
}