using DataModels.EF.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModels.EF
{
    public class Favorites
    {
        public int Id { get; set; }

        [ForeignKey(nameof(FavoriteStatus))] public int StatusId { get; set; }
        public virtual FavoriteStatuses FavoriteStatus { get; set; }

        [ForeignKey(nameof(Anime))] public int AnimeId { get; set; }
        public Animes Anime { get; set; }

        [DataType(DataType.DateTime)] public DateTime? CreatedDate { get; set; }

        [ForeignKey(nameof(User))]
        public int CreatedBy { get; set; }
        public Users User { get; set; }

        [DataType(DataType.DateTime)] public DateTime? DeletedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
