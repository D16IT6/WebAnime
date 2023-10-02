using DataModels.Migrations;
using System.Data.Entity;

namespace DataModels.EF
{
    public class WebAnimeDbContext : DbContext
    {
        public WebAnimeDbContext()
            : base("name=WebAnimeDbContext")
        {
            var init = new MigrateDatabaseToLatestVersion<WebAnimeDbContext, Configuration>();
            Database.SetInitializer(init);
        }

        public virtual DbSet<AgeRatings> AgeRatings { get; set; }
        public virtual DbSet<Animes> Animes { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Countries> Countries { get; set; }
        public virtual DbSet<Statuses> Statuses { get; set; }
        public virtual DbSet<Studios> Studios { get; set; }
        public virtual DbSet<Types> Types { get; set; }
        public virtual DbSet<Servers> Servers { get; set; }
        public virtual DbSet<Episodes> Episodes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AgeRatings>()
                .HasMany(e => e.Animes)
                .WithOptional(e => e.AgeRatings)
                .HasForeignKey(e => e.AgeRatingId);

            modelBuilder.Entity<Animes>()
                .HasMany(e => e.Categories)
                .WithMany(e => e.Animes)
                .Map(m => m.ToTable("AnimeCategory").MapLeftKey("AnimeId").MapRightKey("CategoryId"));

            modelBuilder.Entity<Animes>()
                .HasMany(e => e.Studios)
                .WithMany(e => e.Animes)
                .Map(m => m.ToTable("AnimeStudio").MapLeftKey("AnimeId").MapRightKey("StudioId"));

            modelBuilder.Entity<Countries>()
                .HasMany(e => e.Animes)
                .WithOptional(e => e.Countries)
                .HasForeignKey(e => e.CountryId);

            modelBuilder.Entity<Statuses>()
                .HasMany(e => e.Animes)
                .WithOptional(e => e.Statuses)
                .HasForeignKey(e => e.StatusId);

            modelBuilder.Entity<Types>()
                .HasMany(e => e.Animes)
                .WithOptional(e => e.Types)
                .HasForeignKey(e => e.TypeId);

            modelBuilder.Entity<Servers>()
                .HasMany(e => e.Episodes)
                .WithRequired(e => e.Servers)
                .HasForeignKey(e => e.ServerId);

            modelBuilder.Entity<Animes>()
                .HasMany(e => e.Episodes)
                .WithRequired(e => e.Animes)
                .HasForeignKey(e => e.AnimeId);


        }


    }
}