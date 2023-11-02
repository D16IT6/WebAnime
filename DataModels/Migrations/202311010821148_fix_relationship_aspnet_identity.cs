namespace DataModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix_relationship_aspnet_identity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AgeRatings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.Int(),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.Int(),
                        DeletedDate = c.DateTime(),
                        DeletedBy = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
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
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.Int(),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.Int(),
                        DeletedDate = c.DateTime(),
                        DeletedBy = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
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
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.Int(),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.Int(),
                        DeletedDate = c.DateTime(),
                        DeletedBy = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(maxLength: 500),
                        UserId = c.Int(nullable: false),
                        AnimeId = c.Int(nullable: false),
                        EpisodeId = c.Int(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.Int(),
                        DeletedDate = c.DateTime(),
                        DeletedBy = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Animes", t => t.AnimeId, cascadeDelete: true)
                .ForeignKey("dbo.Episodes", t => t.EpisodeId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.AnimeId)
                .Index(t => t.EpisodeId);
            
            CreateTable(
                "dbo.Episodes",
                c => new
                    {
                        SortOrder = c.Int(nullable: false),
                        AnimeId = c.Int(nullable: false),
                        ServerId = c.Int(nullable: false),
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(maxLength: 255),
                        Title = c.String(maxLength: 50),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.Int(),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.Int(),
                        DeletedDate = c.DateTime(),
                        DeletedBy = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
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
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.Int(),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.Int(),
                        DeletedDate = c.DateTime(),
                        DeletedBy = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AvatarUrl = c.String(maxLength: 200),
                        BirthDay = c.DateTime(),
                        FullName = c.String(maxLength: 250),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.Int(),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.Int(),
                        DeletedDate = c.DateTime(),
                        DeletedBy = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BlogComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(maxLength: 500),
                        UserId = c.Int(nullable: false),
                        BlogId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.Int(),
                        DeletedDate = c.DateTime(),
                        DeletedBy = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Blogs", t => t.BlogId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.BlogId);
            
            CreateTable(
                "dbo.Blogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 250),
                        Slug = c.String(maxLength: 250),
                        ImageUrl = c.String(maxLength: 250),
                        Content = c.String(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.Int(),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.Int(),
                        DeletedDate = c.DateTime(),
                        DeletedBy = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BlogCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.Int(),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.Int(),
                        DeletedDate = c.DateTime(),
                        DeletedBy = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        Users_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Users_Id)
                .Index(t => t.Users_Id);
            
            CreateTable(
                "dbo.Favorites",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateAdd = c.DateTime(nullable: false),
                        StatusId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        AnimeId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.Int(),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.Int(),
                        DeletedDate = c.DateTime(),
                        DeletedBy = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Animes", t => t.AnimeId, cascadeDelete: true)
                .ForeignKey("dbo.FavoriteStatuses", t => t.StatusId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.StatusId)
                .Index(t => t.UserId)
                .Index(t => t.AnimeId);
            
            CreateTable(
                "dbo.FavoriteStatuses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.ProviderKey, t.LoginProvider })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RatePoint = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        AnimeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Animes", t => t.AnimeId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.AnimeId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        RoleId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.Roles", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.Int(),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.Int(),
                        DeletedDate = c.DateTime(),
                        DeletedBy = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        DayOfWeek = c.Int(nullable: false),
                        Time = c.DateTime(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.Int(),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.Int(),
                        DeletedDate = c.DateTime(),
                        DeletedBy = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Animes", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Statuses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.Int(),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.Int(),
                        DeletedDate = c.DateTime(),
                        DeletedBy = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Studios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.Int(),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.Int(),
                        DeletedDate = c.DateTime(),
                        DeletedBy = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Types",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.Int(),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.Int(),
                        DeletedDate = c.DateTime(),
                        DeletedBy = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AnimeCategories",
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
                "dbo.BlogInCategories",
                c => new
                    {
                        BlogId = c.Int(nullable: false),
                        BlogCategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BlogId, t.BlogCategoryId })
                .ForeignKey("dbo.Blogs", t => t.BlogId, cascadeDelete: true)
                .ForeignKey("dbo.BlogCategories", t => t.BlogCategoryId, cascadeDelete: true)
                .Index(t => t.BlogId)
                .Index(t => t.BlogCategoryId);
            
            CreateTable(
                "dbo.AnimeStudios",
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
            DropForeignKey("dbo.AnimeStudios", "StudioId", "dbo.Studios");
            DropForeignKey("dbo.AnimeStudios", "AnimeId", "dbo.Animes");
            DropForeignKey("dbo.Animes", "StatusId", "dbo.Statuses");
            DropForeignKey("dbo.Schedules", "Id", "dbo.Animes");
            DropForeignKey("dbo.Episodes", "AnimeId", "dbo.Animes");
            DropForeignKey("dbo.Animes", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Comments", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Roles");
            DropForeignKey("dbo.Ratings", "UserId", "dbo.Users");
            DropForeignKey("dbo.Ratings", "AnimeId", "dbo.Animes");
            DropForeignKey("dbo.UserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.Favorites", "UserId", "dbo.Users");
            DropForeignKey("dbo.Favorites", "StatusId", "dbo.FavoriteStatuses");
            DropForeignKey("dbo.Favorites", "AnimeId", "dbo.Animes");
            DropForeignKey("dbo.UserClaims", "Users_Id", "dbo.Users");
            DropForeignKey("dbo.BlogComments", "UserId", "dbo.Users");
            DropForeignKey("dbo.BlogComments", "BlogId", "dbo.Blogs");
            DropForeignKey("dbo.BlogInCategories", "BlogCategoryId", "dbo.BlogCategories");
            DropForeignKey("dbo.BlogInCategories", "BlogId", "dbo.Blogs");
            DropForeignKey("dbo.Comments", "EpisodeId", "dbo.Episodes");
            DropForeignKey("dbo.Episodes", "ServerId", "dbo.Servers");
            DropForeignKey("dbo.Comments", "AnimeId", "dbo.Animes");
            DropForeignKey("dbo.AnimeCategories", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.AnimeCategories", "AnimeId", "dbo.Animes");
            DropIndex("dbo.AnimeStudios", new[] { "StudioId" });
            DropIndex("dbo.AnimeStudios", new[] { "AnimeId" });
            DropIndex("dbo.BlogInCategories", new[] { "BlogCategoryId" });
            DropIndex("dbo.BlogInCategories", new[] { "BlogId" });
            DropIndex("dbo.AnimeCategories", new[] { "CategoryId" });
            DropIndex("dbo.AnimeCategories", new[] { "AnimeId" });
            DropIndex("dbo.Schedules", new[] { "Id" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.Ratings", new[] { "AnimeId" });
            DropIndex("dbo.Ratings", new[] { "UserId" });
            DropIndex("dbo.UserLogins", new[] { "UserId" });
            DropIndex("dbo.Favorites", new[] { "AnimeId" });
            DropIndex("dbo.Favorites", new[] { "UserId" });
            DropIndex("dbo.Favorites", new[] { "StatusId" });
            DropIndex("dbo.UserClaims", new[] { "Users_Id" });
            DropIndex("dbo.BlogComments", new[] { "BlogId" });
            DropIndex("dbo.BlogComments", new[] { "UserId" });
            DropIndex("dbo.Episodes", new[] { "ServerId" });
            DropIndex("dbo.Episodes", new[] { "AnimeId" });
            DropIndex("dbo.Comments", new[] { "EpisodeId" });
            DropIndex("dbo.Comments", new[] { "AnimeId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.Animes", new[] { "AgeRatingId" });
            DropIndex("dbo.Animes", new[] { "CountryId" });
            DropIndex("dbo.Animes", new[] { "TypeId" });
            DropIndex("dbo.Animes", new[] { "StatusId" });
            DropTable("dbo.AnimeStudios");
            DropTable("dbo.BlogInCategories");
            DropTable("dbo.AnimeCategories");
            DropTable("dbo.Types");
            DropTable("dbo.Studios");
            DropTable("dbo.Statuses");
            DropTable("dbo.Schedules");
            DropTable("dbo.Countries");
            DropTable("dbo.Roles");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Ratings");
            DropTable("dbo.UserLogins");
            DropTable("dbo.FavoriteStatuses");
            DropTable("dbo.Favorites");
            DropTable("dbo.UserClaims");
            DropTable("dbo.BlogCategories");
            DropTable("dbo.Blogs");
            DropTable("dbo.BlogComments");
            DropTable("dbo.Users");
            DropTable("dbo.Servers");
            DropTable("dbo.Episodes");
            DropTable("dbo.Comments");
            DropTable("dbo.Categories");
            DropTable("dbo.Animes");
            DropTable("dbo.AgeRatings");
        }
    }
}
