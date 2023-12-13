using DataModels.EF.Identity;
using DataModels.Migrations;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace DataModels.EF
{
    public class WebAnimeDbContext : IdentityDbContext<Users, Roles, int, UserLogins, UserRoles, UserClaims>
    {
        public WebAnimeDbContext()
            : base("name=WebAnimeDbContext")
        {
            var init = new MigrateDatabaseToLatestVersion<WebAnimeDbContext, Configuration>();
            Database.SetInitializer(init);
        }

        public virtual DbSet<AgeRatings> AgeRatings { get; set; }
        public virtual DbSet<Animes> Animes { get; set; }
        public virtual DbSet<BlogComments> BlogComments { get; set; }
        public virtual DbSet<Blogs> Blogs { get; set; }
        public virtual DbSet<BlogCategories> BlogCategories { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Countries> Countries { get; set; }
        public virtual DbSet<Episodes> Episodes { get; set; }
        public virtual DbSet<Favorites> Favorites { get; set; }
        public virtual DbSet<FavoriteStatuses> FavoriteStatuses { get; set; }
        public virtual DbSet<Ratings> Ratings { get; set; }
        public virtual DbSet<Schedules> Schedules { get; set; }
        public virtual DbSet<Servers> Servers { get; set; }

        public virtual DbSet<Statuses> Statuses { get; set; }
        public virtual DbSet<Studios> Studios { get; set; }
        public virtual DbSet<Types> Types { get; set; }
        public virtual DbSet<UserRefreshToken> UserRefreshTokens { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Users>().ToTable("Users");
            modelBuilder.Entity<Roles>().ToTable("Roles");
            modelBuilder.Entity<UserRoles>().ToTable("UserRoles");
            modelBuilder.Entity<UserLogins>().ToTable("UserLogins");
            modelBuilder.Entity<UserClaims>().ToTable("UserClaims");

            modelBuilder.Entity<UserRoles>().HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Entity<UserLogins>().HasKey(x => new { x.UserId, x.ProviderKey, x.LoginProvider });




            modelBuilder.Entity<AgeRatings>()
                .HasMany(e => e.Animes)
                .WithOptional(e => e.AgeRatings)
                .HasForeignKey(e => e.AgeRatingId);

            modelBuilder.Entity<Animes>()
                .HasMany(e => e.Categories)
                .WithMany(e => e.Animes)
                .Map(m => m.ToTable("AnimeInCategories").MapLeftKey("AnimeId").MapRightKey("CategoryId"));

            modelBuilder.Entity<Blogs>()
                .HasMany(e => e.BlogCategories)
                .WithMany(e => e.Blogs)
                .Map(m => m.ToTable("BlogInCategories").MapLeftKey("BlogId").MapRightKey("BlogCategoryId"));

            modelBuilder.Entity<Animes>()
                .HasMany(e => e.Studios)
                .WithMany(e => e.Animes)
                .Map(m => m.ToTable("AnimeInStudios").MapLeftKey("AnimeId").MapRightKey("StudioId"));



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