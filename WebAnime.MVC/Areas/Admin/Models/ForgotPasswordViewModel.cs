using System.ComponentModel.DataAnnotations;

namespace WebAnime.MVC.Areas.Admin.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [StringLength(32, ErrorMessage = "{0} chỉ dài từ {2} tới {1} ký tự", MinimumLength = 6)]
        [Display(Name = "Địa chỉ email")]
        public string Email { get; set; }
    }
}