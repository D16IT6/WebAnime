using System.ComponentModel.DataAnnotations.Schema;

namespace DataModels.EF
{
    using System.ComponentModel.DataAnnotations;

    public partial class Episodes
    {
        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        public string Url { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [Column(Order = 4)]
        public int Order { get; set; }

        [Column(Order = 5)]
        public int AnimeId { get; set; }

        [Column(Order = 6)]
        public int ServerId { get; set; }

        public virtual Animes Animes { get; set; }

        public virtual Servers Servers { get; set; }

    }
}
