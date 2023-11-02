using System.ComponentModel.DataAnnotations;

namespace ViewModels.Admin
{
    public class StudioViewModel
    {
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "{0} phải dài từ {2} tới {1} ký tự", MinimumLength = 2)]
        [Display(Name = "Tên Studio")]
        [Required(ErrorMessage = "Tên không được để trống", AllowEmptyStrings = false)]
        public string Name { get; set; }
    }
}