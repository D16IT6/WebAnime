using DataModels.EF.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModels.EF
{
    public class Ratings
    {
        public int Id { get; set; }
        [Range(0, 10)]
        public int RatePoint { get; set; }
        [ForeignKey(nameof(User))] public int UserId { get; set; }
        public Users User { get; set; }
        [ForeignKey(nameof(Anime))] public int AnimeId { get; set; }
        public Animes Anime { get; set; }


    }
}
