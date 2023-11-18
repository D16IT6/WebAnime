using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Client
{
    public class CommentShowViewModel
    {
        public int Id { get; set; }
        public int AnimeId { get; set; }
        public string Content { get; set; }
        public int CreatedBy { get; set; }
        public string UserFullName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string AvatarUrl { get; set; }
        public string EpisodeTitle { get; set; }
    }
}
