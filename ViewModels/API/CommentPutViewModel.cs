
namespace ViewModels.API
{
    public class CommentPutViewModel
    {
        public int AnimeId { get; set; }
        public int UserId { get; set; }
        public int? EpisodeId { get; set; }
        public string Content { get; set; }

    }
}
