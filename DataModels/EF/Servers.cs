namespace DataModels.EF
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Servers
    {
        public Servers()
        {
            Episodes = new HashSet<Episodes>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Episodes> Episodes { get; set; }
    }
}
