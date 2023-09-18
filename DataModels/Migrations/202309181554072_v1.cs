namespace DataModels.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AgeRatings",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(maxLength: 50),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Animes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Title = c.String(maxLength: 255),
                    OriginalTitle = c.String(maxLength: 255),
                    Synopsis = c.String(),
                    Poster = c.String(maxLength: 250),
                    Duration = c.Int(nullable: false),
                    Release = c.DateTime(storeType: "date"),
                    Trailer = c.String(maxLength: 50),
                    ViewCount = c.Int(),
                    TotalEpisodes = c.Int(),
                    StatusId = c.Int(),
                    TypeId = c.Int(),
                    CountryId = c.Int(),
                    AgeRatingId = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .ForeignKey("dbo.Statuses", t => t.StatusId)
                .ForeignKey("dbo.Types", t => t.TypeId)
                .ForeignKey("dbo.AgeRatings", t => t.AgeRatingId)
                .Index(t => t.StatusId)
                .Index(t => t.TypeId)
                .Index(t => t.CountryId)
                .Index(t => t.AgeRatingId);

            CreateTable(
                "dbo.Categories",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(maxLength: 50),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Countries",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(maxLength: 50),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Episodes",
                c => new
                {
                    Order = c.Int(nullable: false),
                    AnimeId = c.Int(nullable: false),
                    ServerId = c.Int(nullable: false),
                    Id = c.Int(nullable: false, identity: true),
                    Url = c.String(maxLength: 255),
                    Title = c.String(maxLength: 50),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Servers", t => t.ServerId, cascadeDelete: true)
                .ForeignKey("dbo.Animes", t => t.AnimeId, cascadeDelete: true)
                .Index(t => t.AnimeId)
                .Index(t => t.ServerId);

            CreateTable(
                "dbo.Servers",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(maxLength: 50),
                    Description = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Statuses",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(maxLength: 50),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Studios",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(maxLength: 50),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Types",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(maxLength: 50),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.AnimeCategory",
                c => new
                {
                    AnimeId = c.Int(nullable: false),
                    CategoryId = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.AnimeId, t.CategoryId })
                .ForeignKey("dbo.Animes", t => t.AnimeId, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.AnimeId)
                .Index(t => t.CategoryId);

            CreateTable(
                "dbo.AnimeStudio",
                c => new
                {
                    AnimeId = c.Int(nullable: false),
                    StudioId = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.AnimeId, t.StudioId })
                .ForeignKey("dbo.Animes", t => t.AnimeId, cascadeDelete: true)
                .ForeignKey("dbo.Studios", t => t.StudioId, cascadeDelete: true)
                .Index(t => t.AnimeId)
                .Index(t => t.StudioId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Animes", "AgeRatingId", "dbo.AgeRatings");
            DropForeignKey("dbo.Animes", "TypeId", "dbo.Types");
            DropForeignKey("dbo.AnimeStudio", "StudioId", "dbo.Studios");
            DropForeignKey("dbo.AnimeStudio", "AnimeId", "dbo.Animes");
            DropForeignKey("dbo.Animes", "StatusId", "dbo.Statuses");
            DropForeignKey("dbo.Episodes", "AnimeId", "dbo.Animes");
            DropForeignKey("dbo.Episodes", "ServerId", "dbo.Servers");
            DropForeignKey("dbo.Animes", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.AnimeCategory", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.AnimeCategory", "AnimeId", "dbo.Animes");
            DropIndex("dbo.AnimeStudio", new[] { "StudioId" });
            DropIndex("dbo.AnimeStudio", new[] { "AnimeId" });
            DropIndex("dbo.AnimeCategory", new[] { "CategoryId" });
            DropIndex("dbo.AnimeCategory", new[] { "AnimeId" });
            DropIndex("dbo.Episodes", new[] { "ServerId" });
            DropIndex("dbo.Episodes", new[] { "AnimeId" });
            DropIndex("dbo.Animes", new[] { "AgeRatingId" });
            DropIndex("dbo.Animes", new[] { "CountryId" });
            DropIndex("dbo.Animes", new[] { "TypeId" });
            DropIndex("dbo.Animes", new[] { "StatusId" });
            DropTable("dbo.AnimeStudio");
            DropTable("dbo.AnimeCategory");
            DropTable("dbo.Types");
            DropTable("dbo.Studios");
            DropTable("dbo.Statuses");
            DropTable("dbo.Servers");
            DropTable("dbo.Episodes");
            DropTable("dbo.Countries");
            DropTable("dbo.Categories");
            DropTable("dbo.Animes");
            DropTable("dbo.AgeRatings");
        }
    }
}
