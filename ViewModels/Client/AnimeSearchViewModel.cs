using System;
using System.ComponentModel.DataAnnotations;

namespace ViewModels.Client
{
    public class AnimeSearchViewModel
    {
        [Display(Name = "Tiêu đề phim")]
        public string SearchTitle { get; set; }

        [Display(Name = "Từ ngày")]
        public DateTime? From { get; set; } = DateTime.MinValue;

        [Display(Name = "Tới ngày")]
        public DateTime? To { get; set; } = DateTime.Now;

        [Display(Name = "Lượt xem hơn")]
        public int ViewCountHigher { get; set; }

        [Display(Name = "Đánh giá hơn")]
        public int RatingHigher { get; set; }

        [Display(Name = "Thể loại")]
        public int[] CategoryIds { get; set; }

        [Display(Name = "Quốc gia")]
        public int CountryId { get; set; }

        [Display(Name = "Trạng thái")]
        public int StatusId { get; set; }

        [Display(Name = "Độ tuổi")]
        public int AgeRatingId { get; set; }

        [Display(Name = "Kiểu")]
        public int TypeId { get; set; }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
