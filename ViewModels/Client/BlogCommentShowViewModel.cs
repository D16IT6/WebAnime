using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Client
{
    public class BlogCommentShowViewModel
    {
        public DateTime CreatedDate { get; set; }
        public string AvatarUrl { get; set; }
        public string FullName { get; set; }
    }
}
