using System.Collections.Generic;

namespace DataModels.EF
{
    public class FavoriteStatuses
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public virtual ICollection<Favorites> Favorites { get; set; }
    }
}
