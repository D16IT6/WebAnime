
using System;

namespace ViewModels.Client
{
    public class BlogCommentViewModel
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public string Content { get; set; }
        public int CreatedBy { get; set; }
        public string UserFullname { get; set; }
        public DateTime CreatedDate { get; set; }
        public string AvatarUrl { get; set; }

    }
}
