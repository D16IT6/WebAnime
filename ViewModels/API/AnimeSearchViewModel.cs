

namespace ViewModels.API
{
    public class AnimeSearchViewModel
    {
        public string SearchTitle { get; set; }
        public int CountryId { get; set; }
        public int AgeRatingId { get; set; }
        public int StatusId { get; set; }
        public int TypeId { get; set; }
        public int[] CategoryIds { get; set; }
    }
}
