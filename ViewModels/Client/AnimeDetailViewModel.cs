using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ViewModels.Client
{
    public class AnimeDetailViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Tiêu đề")]
        public string Title { get; set; }

        [Display(Name = "Tiêu đề gốc")]
        public string OriginalTitle { get; set; }

        [Display(Name = "Nội dung")]
        [AllowHtml]
        public string Synopsis { get; set; }

        [Display(Name = "Ảnh bìa")]
        public string Poster { get; set; }

        [Display(Name = "Thời lượng(phút)")]
        public int Duration { get; set; }

        [Display(Name = "Ngày phát hành")]
        public DateTime? Release { get; set; } = DateTime.Now.Date;

        public string Type { get; set; }
        public string[] Studios { get; set; }
        public string[] Categories { get; set; }

        public string Status { get; set; }

        public float Score { get; set; }
        public int RateCount { get; set; }
        public int CommentCount { get; set; }
        public int ViewCount { get; set; }
    }
}
