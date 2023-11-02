using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ViewModels.Admin
{
    public class BlogViewModel
    {
        public int Id { get; set; }

        [StringLength(250, ErrorMessage = "{0} không dài quá {2} tới {1} ký tự", MinimumLength = 3)]
        [Display(Name = "Tiêu đề")]
        [Required(ErrorMessage = "{0} là bắt buộc")]
        public string Title { get; set; }

        [StringLength(250, ErrorMessage = "{0} không dài quá {2} tới {1} ký tự", MinimumLength = 3)]
        [RegularExpression("^[a-zA-Z0-9-]+$", ErrorMessage = "{0} chỉ chứa chữ cái, số và gạch ngang")]
        [Display(Name = "Đường dẫn")]
        [Required(ErrorMessage = "{0} là bắt buộc")]
        public string Slug { get; set; }

        [Required(ErrorMessage = "{0} là bắt buộc")]
        [Display(Name = "Nội dung")]
        [AllowHtml]
        public string Content { get; set; }

        [StringLength(250, ErrorMessage = "{0} không dài quá {2} tới {1} ký tự", MinimumLength = 3)]
        [Display(Name = "Đường dẫn ảnh")]
        [Required(ErrorMessage = "{0} là bắt buộc")]
        [RegularExpression("^(https?://(localhost|[\\w\\-]+(\\.[\\w\\-]+)+)(:\\d+)?(/[\\w\\-]+)+|/[\\w\\-]+)+\\.[a-zA-Z]{2,4}$", ErrorMessage = "{0} không hợp lệ, chỉ cho phép url hoặc đường dẫn ảnh trực tiếp")]
        public string ImageUrl { get; set; }

        public int? CreatedBy { get; set; }

        public int? ModifiedBy { get; set; }

        public int? DeletedBy { get; set; }
        public bool IsDeleted { get; set; }

        [Display(Name = "Danh mục")]
        [Required(ErrorMessage = "{0} là bắt buộc")]
        public int[] BlogCategoryIds { get; set; }
    }
}