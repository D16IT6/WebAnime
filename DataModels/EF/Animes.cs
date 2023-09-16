using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModels.EF
{
    public class Animes
    {
        public Animes()
        {
            Categories = new HashSet<Categories>();
            Studios = new HashSet<Studios>();
        }

        public int Id { get; set; }

        [StringLength(255)] public string Title { get; set; }

        [StringLength(255)] public string OriginalTitle { get; set; }

        public string Synopsis { get; set; }

        [StringLength(250)] public string Poster { get; set; }

        [Range(1, Int32.MaxValue)] public int Duration { get; set; }

        [Column(TypeName = "date")] public DateTime? Release { get; set; } = DateTime.Now.Date;

        [StringLength(50)] public string Trailer { get; set; }

        public int? ViewCount { get; set; } = 0;

        public int? TotalEpisodes { get; set; } = 12;

        public int? StatusId { get; set; }

        public int? TypeId { get; set; }

        public int? CountryId { get; set; }

        public int? AgeRatingId { get; set; }

        public virtual AgeRatings AgeRatings { get; set; }

        public virtual Countries Countries { get; set; }

        public virtual Statuses Statuses { get; set; }

        public virtual Types Types { get; set; }

        public virtual ICollection<Categories> Categories { get; set; }

        public virtual ICollection<Studios> Studios { get; set; }

        public virtual ICollection<Episodes> Episodes { get; set; }


        [NotMapped] public int[] CategoriesId { get; set; }

        [NotMapped] public int[] StudiosId { get; set; }

        [NotMapped] public int[] EpisodesId { get; set; }
    }
}