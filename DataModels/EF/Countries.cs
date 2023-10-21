using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataModels.EF
{
    public class Countries
    {
        public Countries()
        {
            Animes = new HashSet<Animes>();
            CreatedDate = ModifiedDate = DateTime.Now;
            IsDeleted = false;
        }

        public int Id { get; set; }

        [StringLength(50)] public string Name { get; set; }

        public virtual ICollection<Animes> Animes { get; set; }

        [DataType(DataType.DateTime)] public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }

        [DataType(DataType.DateTime)] public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }

        [DataType(DataType.DateTime)] public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public bool IsDeleted { get; set; }
    }
}