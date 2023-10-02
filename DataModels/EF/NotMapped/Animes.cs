using System.ComponentModel.DataAnnotations.Schema;

namespace DataModels.EF
{
    public partial class Animes
    {
        [NotMapped] public int[] CategoriesId { get; set; }

        [NotMapped] public int[] StudiosId { get; set; }

        [NotMapped] public int[] EpisodesId { get; set; }
    }
}
