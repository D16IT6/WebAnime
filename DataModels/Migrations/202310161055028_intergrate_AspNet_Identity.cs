namespace DataModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intergrate_AspNet_Identity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AgeRatings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.Int(),
                        DeletedDate = c.DateTime(),
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
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.Int(),
                        DeletedDate = c.DateTime(),
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
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.Int(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.Int(),
                        DeletedDate = c.DateTime(),
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
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.Int(),
                        DeletedDate = c.DateTime(),
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
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.Int(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Statuses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.Int(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Studios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.Int(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Types",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.Int(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        RoleId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Roles_Id = c.Int(),
                        Users_Id = c.Int(),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.Roles", t => t.Roles_Id)
                .ForeignKey("dbo.Users", t => t.Users_Id)
                .Index(t => t.Roles_Id)
                .Index(t => t.Users_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BirthDay = c.DateTime(),
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
                "dbo.UserLogins",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        Users_Id = c.Int(),
                    })
                .PrimaryKey(t => new { t.UserId, t.ProviderKey, t.LoginProvider })
                .ForeignKey("dbo.Users", t => t.Users_Id)
                .Index(t => t.Users_Id);
            
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
            DropForeignKey("dbo.UserRoles", "Users_Id", "dbo.Users");
            DropForeignKey("dbo.UserLogins", "Users_Id", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "Users_Id", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "Roles_Id", "dbo.Roles");
            DropForeignKey("dbo.Animes", "AgeRatingId", "dbo.AgeRatings");
            DropForeignKey("dbo.Animes", "TypeId", "dbo.Types");
            DropForeignKey("dbo.AnimeStudios", "StudioId", "dbo.Studios");
            DropForeignKey("dbo.AnimeStudios", "AnimeId", "dbo.Animes");
            DropForeignKey("dbo.Animes", "StatusId", "dbo.Statuses");
            DropForeignKey("dbo.Episodes", "AnimeId", "dbo.Animes");
            DropForeignKey("dbo.Episodes", "ServerId", "dbo.Servers");
            DropForeignKey("dbo.Animes", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.AnimeCategories", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.AnimeCategories", "AnimeId", "dbo.Animes");
            DropIndex("dbo.AnimeStudios", new[] { "StudioId" });
            DropIndex("dbo.AnimeStudios", new[] { "AnimeId" });
            DropIndex("dbo.AnimeCategories", new[] { "CategoryId" });
            DropIndex("dbo.AnimeCategories", new[] { "AnimeId" });
            DropIndex("dbo.UserLogins", new[] { "Users_Id" });
            DropIndex("dbo.UserClaims", new[] { "Users_Id" });
            DropIndex("dbo.UserRoles", new[] { "Users_Id" });
            DropIndex("dbo.UserRoles", new[] { "Roles_Id" });
            DropIndex("dbo.Episodes", new[] { "ServerId" });
            DropIndex("dbo.Episodes", new[] { "AnimeId" });
            DropIndex("dbo.Animes", new[] { "AgeRatingId" });
            DropIndex("dbo.Animes", new[] { "CountryId" });
            DropIndex("dbo.Animes", new[] { "TypeId" });
            DropIndex("dbo.Animes", new[] { "StatusId" });
            DropTable("dbo.AnimeStudios");
            DropTable("dbo.AnimeCategories");
            DropTable("dbo.UserLogins");
            DropTable("dbo.UserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Roles");
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
