using DataModels.EF;
using DataModels.Helpers;
using System.Data.Entity.Migrations;

namespace DataModels.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<WebAnimeDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(WebAnimeDbContext context)
        {
            context.LoadCountries();
            context.LoadAgeRatings();
            context.LoadStatuses();
            context.LoadTypes();
            context.LoadStudios();
            context.LoadCategories();
            context.LoadServers();
            context.LoadBlogCategories();
        }

    }
}