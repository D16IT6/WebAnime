using System.ComponentModel.DataAnnotations;

namespace ViewModels.Admin
{
    public class CountryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} là bắt buộc")]
        [StringLength(50, ErrorMessage = "{0} phải từ {2} tới {1} ký tự", MinimumLength = 2)]
        [Display(Name = "Tên quốc gia")]
        public string Name { get; set; }

    }
}