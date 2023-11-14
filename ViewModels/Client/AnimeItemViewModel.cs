using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Client
{
    public class AnimeItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Poster { get; set; }
        public int CommentCount { get; set; }
        public int ViewCount { get; set; }
        public int CurrentEpisode { get; set; }
        public int? TotalEpisode { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
    }
}
