namespace DataModels.EF
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Episodes
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AnimeId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ServerId { get; set; }
        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Order { get; set; }
        [StringLength(255)]
        public string Url { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        public virtual Animes Animes { get; set; }
        public virtual Servers Servers { get; set; }

    }
}
