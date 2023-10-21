namespace DataModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Some_Tables : DbMigration
    {
        public override void Up()
        {
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
                "dbo.Schedules",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        DayOfWeek = c.Int(nullable: false),
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
            
            AddColumn("dbo.Episodes", "SortOrder", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "CreatedDate", c => c.DateTime());
            AddColumn("dbo.Users", "CreatedBy", c => c.Int());
            AddColumn("dbo.Users", "ModifiedDate", c => c.DateTime());
            AddColumn("dbo.Users", "ModifiedBy", c => c.Int());
            AddColumn("dbo.Users", "DeletedDate", c => c.DateTime());
            AddColumn("dbo.Users", "DeletedBy", c => c.Int());
            AddColumn("dbo.Users", "IsDeleted", c => c.Boolean(nullable: false));
            DropColumn("dbo.Episodes", "Order");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Episodes", "Order", c => c.Int(nullable: false));
            DropForeignKey("dbo.Schedules", "Id", "dbo.Animes");
            DropForeignKey("dbo.Comments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Ratings", "UserId", "dbo.Users");
            DropForeignKey("dbo.Ratings", "AnimeId", "dbo.Animes");
            DropForeignKey("dbo.Favorites", "UserId", "dbo.Users");
            DropForeignKey("dbo.Favorites", "StatusId", "dbo.FavoriteStatuses");
            DropForeignKey("dbo.Favorites", "AnimeId", "dbo.Animes");
            DropForeignKey("dbo.BlogComments", "UserId", "dbo.Users");
            DropForeignKey("dbo.BlogComments", "BlogId", "dbo.Blogs");
            DropForeignKey("dbo.Comments", "EpisodeId", "dbo.Episodes");
            DropForeignKey("dbo.Comments", "AnimeId", "dbo.Animes");
            DropIndex("dbo.Schedules", new[] { "Id" });
            DropIndex("dbo.Ratings", new[] { "AnimeId" });
            DropIndex("dbo.Ratings", new[] { "UserId" });
            DropIndex("dbo.Favorites", new[] { "AnimeId" });
            DropIndex("dbo.Favorites", new[] { "UserId" });
            DropIndex("dbo.Favorites", new[] { "StatusId" });
            DropIndex("dbo.BlogComments", new[] { "BlogId" });
            DropIndex("dbo.BlogComments", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "EpisodeId" });
            DropIndex("dbo.Comments", new[] { "AnimeId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropColumn("dbo.Users", "IsDeleted");
            DropColumn("dbo.Users", "DeletedBy");
            DropColumn("dbo.Users", "DeletedDate");
            DropColumn("dbo.Users", "ModifiedBy");
            DropColumn("dbo.Users", "ModifiedDate");
            DropColumn("dbo.Users", "CreatedBy");
            DropColumn("dbo.Users", "CreatedDate");
            DropColumn("dbo.Episodes", "SortOrder");
            DropTable("dbo.Schedules");
            DropTable("dbo.Ratings");
            DropTable("dbo.FavoriteStatuses");
            DropTable("dbo.Favorites");
            DropTable("dbo.Blogs");
            DropTable("dbo.BlogComments");
            DropTable("dbo.Comments");
        }
    }
}
