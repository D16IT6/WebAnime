using System;
using System.Collections.Generic;


namespace ViewModels.Client
{
    public class BlogViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Slug { get; set; }

        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedBy { get; set; }

        public IEnumerable<BlogCategoryViewModel> BlogCategories { get; set; }
        public IEnumerable<BlogCommentViewModel> BlogComments { get; set; }
    }
}

