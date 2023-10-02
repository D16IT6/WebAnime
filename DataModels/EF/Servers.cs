namespace DataModels.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Servers
    {
        public Servers()
        {
            Episodes = new HashSet<Episodes>();
            CreatedDate = ModifiedDate = DateTime.Now;
            IsDeleted = false;
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Episodes> Episodes { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }

        [DataType(DataType.Date)]
        public DateTime ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }

        public bool IsDeleted { get; set; }
        public int? DeletedBy { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DeletedDate { get; set; }
    }
}
