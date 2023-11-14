using DataModels.EF.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModels.EF
{
    public class Comments
    {
        public int Id { get; set; }
        [MaxLength(500)] public string Content { get; set; }

        [ForeignKey(nameof(User))] public int UserId { get; set; }
        public Users User { get; set; }

        [ForeignKey(nameof(Anime))] public int AnimeId { get; set; }
        public Animes Anime { get; set; }

        [ForeignKey(nameof(Episode))] public int? EpisodeId { get; set; }
        public virtual Episodes Episode { get; set; }

        [DataType(DataType.DateTime)] public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }

        [DataType(DataType.DateTime)] public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public bool IsDeleted { get; set; }


    }
}
