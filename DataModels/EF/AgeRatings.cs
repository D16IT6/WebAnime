using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataModels.EF
{
    public class AgeRatings
    {
        public AgeRatings()
        {
            Animes = new HashSet<Animes>();
        }

        public int Id { get; set; }

        [StringLength(50)] public string Name { get; set; }

        public virtual ICollection<Animes> Animes { get; set; }
    }
}