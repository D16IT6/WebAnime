using System.ComponentModel.DataAnnotations;

namespace WebAnime.MVC.Areas.Admin.Models
{
    public class EpisodeViewModel
    {
        public int Id { get; set; }
        public int AnimeId { get; set; }
        public int ServerId { get; set; }


        [StringLength(255, ErrorMessage = "{0} phải dài từ {2} tới {1} ký tự", MinimumLength = 2)]
        [Required(ErrorMessage = "{0} là bắt buộc")]
        [Display(Name = "Url")]
        public string Url { get; set; }

        [Display(Name = "Thứ tự")]
        [Required(ErrorMessage = "{0} là bắt buộc")]
        [Range(1, 9999, ErrorMessage = "{0} phải từ {1} tới {2}")]
        public int Order { get; set; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = "{0} phải dài từ {2} tới {1} ký tự")]
        [Required(ErrorMessage = "{0} là bắt buộc")]
        [Display(Name = "Tên tập")]
        public string Title { get; set; }
    }
}