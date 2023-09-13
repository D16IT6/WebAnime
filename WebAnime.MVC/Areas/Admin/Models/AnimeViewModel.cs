using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace WebAnime.MVC.Areas.Admin.Models
{
    public class AnimeViewModel
    {
        public int Id { get; set; }

        [StringLength(255, ErrorMessage = "{0} phải dài từ {2} tới {1} ký tự", MinimumLength = 2)]
        [Required(ErrorMessage = "{0} là bắt buộc")]
        [Display(Name = "Tiêu đề")]
        public string Title { get; set; }

        [Required(ErrorMessage = "{0} là bắt buộc")]
        [StringLength(255, ErrorMessage = "{0} phải dài từ {2} tới {1} ký tự", MinimumLength = 2)]
        [Display(Name = "Tiêu đề gốc")]
        public string OriginalTitle { get; set; }

        [Display(Name = "Nội dung")]
        [AllowHtml]
        [Required(ErrorMessage = "{0} là bắt buộc")]
        public string Synopsis { get; set; }

        [Display(Name = "Ảnh bìa")]
        [Required(ErrorMessage = "{0} là bắt buộc")]
        [StringLength(250)] public string Poster { get; set; }

        [Display(Name = "Thời lượng(phút)")]
        [Required(ErrorMessage = "{0} là bắt buộc")]
        [StringLength(50)] public string Duration { get; set; }

        [Display(Name = "Ngày phát hành")]
        [Required(ErrorMessage = "{0} là bắt buộc")]
        [Column(TypeName = "date")]
        public DateTime? Release { get; set; } = DateTime.Now.Date;

        [StringLength(50, ErrorMessage = "{0} phải dài từ {2} tới {1} ký tự", MinimumLength = 2)]
        [Display(Name = "Trailer(Link youtube)")]
        [Required(ErrorMessage = "{0} là bắt buộc")]
        public string Trailer { get; set; }

        [Range(1, 100000, ErrorMessage = "{0} phải trong khoảng {1} tới {2}")]
        [Display(Name = "Tổng số tập(dự kiến)")]
        [Required(ErrorMessage = "{0} là bắt buộc")]
        public int? TotalEpisodes { get; set; } = 12;

        [Display(Name = "Trạng thái")]
        [Required(ErrorMessage = "{0} là bắt buộc")]
        public int? StatusId { get; set; }
        [Display(Name = "Kiểu anime")]
        [Required(ErrorMessage = "{0} là bắt buộc")]
        public int? TypeId { get; set; }
        [Display(Name = "Quốc gia")]
        [Required(ErrorMessage = "{0} là bắt buộc")]
        public int? CountryId { get; set; }
        [Display(Name = "Độ tuổi")]
        [Required(ErrorMessage = "{0} là bắt buộc")]
        public int? AgeRatingId { get; set; }

        [Display(Name = "Thể loại")]
        [Required(ErrorMessage = "{0} là bắt buộc")]
        public int[] CategoriesId { get; set; }

        [Display(Name = "Nhà sản xuất")]
        [Required(ErrorMessage = "{0} là bắt buộc")]
        public int[] StudiosId { get; set; }
    }
}