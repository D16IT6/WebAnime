using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModels.EF
{
    public class Schedules
    {
        [ForeignKey(nameof(Anime))]
        public int Id { get; set; }
        [Range(1, 7)]
        public int DayOfWeek { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime Time { get; set; }
        public Animes Anime { get; set; }
        [DataType(DataType.DateTime)] public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }

        [DataType(DataType.DateTime)] public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }

        [DataType(DataType.DateTime)] public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public bool IsDeleted { get; set; }
    }
}
