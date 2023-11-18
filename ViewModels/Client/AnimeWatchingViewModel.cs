using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Admin;

namespace ViewModels.Client
{
    public class AnimeWatchingViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IQueryable<ServerClientViewModel> Server { get; set; }
        public int CommentCount { get; set; }

    }

}
