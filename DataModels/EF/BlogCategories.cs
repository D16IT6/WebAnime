using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModels.EF
{
    public class BlogCategories
    {
        public int Id { get; set; }
        [StringLength(50)]
        [Column(TypeName = "nvarchar")]
        public string Name { get; set; }

        [ForeignKey("BlogId")]
        public virtual ICollection<Blogs> Blogs { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CreatedDate { get; set; }

        public int? CreatedBy { get; set; }

        [DataType(DataType.DateTime)] public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }

        [DataType(DataType.DateTime)] public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public bool IsDeleted { get; set; }

    }
}
