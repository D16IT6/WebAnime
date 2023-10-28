using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebAnime.MVC.Areas.Admin.Models
{
    public class EpisodeViewModel
    {

        public int Id { get; set; }
        [JsonProperty(nameof(AnimeId))]

        public int AnimeId { get; set; }
        [JsonProperty(nameof(ServerId))]

        public int ServerId { get; set; }

        [StringLength(255, ErrorMessage = "{0} phải dài từ {2} tới {1} ký tự", MinimumLength = 1)]
        [Required(ErrorMessage = "{0} là bắt buộc")]
        [Display(Name = "Url")]
        [JsonProperty(nameof(Url))]

        public string Url { get; set; }

        [JsonProperty(nameof(SortOrder))]
        [Display(Name = "Thứ tự")]
        [Required(ErrorMessage = "{0} là bắt buộc")]
        [Range(1, 9999, ErrorMessage = "{0} phải từ {1} tới {2}")]
        public int SortOrder { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = "{0} phải dài từ {2} tới {1} ký tự")]
        [Required(ErrorMessage = "{0} là bắt buộc")]
        [Display(Name = "Tên tập")]
        [JsonProperty(nameof(Title))]
        public string Title { get; set; }

        [JsonProperty(nameof(CreatedBy))]

        public int CreatedBy { get; set; }
        [JsonProperty(nameof(CreatedDate))]

        public DateTime CreatedDate { get; set; }
    }
}