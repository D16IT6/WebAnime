using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DataModels.EF
{
    public class Studios
    {
        public Studios()
        {
            Animes = new HashSet<Animes>();
        }

        public int Id { get; set; }

        [StringLength(50)] public string Name { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Animes> Animes { get; set; }
    }
}