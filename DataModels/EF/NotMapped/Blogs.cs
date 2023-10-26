using System.ComponentModel.DataAnnotations.Schema;

namespace DataModels.EF
{
    public partial class Blogs
    {
        [NotMapped] public int[] BlogCategoryIds { get; set; }

    }
}
