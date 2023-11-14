using DataModels.EF.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModels.EF
{
    public class BlogComments
    {
        public int Id { get; set; }
        [MaxLength(500)] public string Content { get; set; }


        [ForeignKey(nameof(Blog))] public int BlogId { get; set; }
        public Blogs Blog { get; set; }


        [DataType(DataType.DateTime)] public DateTime? CreatedDate { get; set; }
        [ForeignKey(nameof(User))]
        public int? CreatedBy { get; set; }
        public Users User { get; set; }


        [DataType(DataType.DateTime)] public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public bool IsDeleted { get; set; }
    }
}
