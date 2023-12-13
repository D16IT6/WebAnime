namespace DataModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRefreshTokenTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserRefreshTokens",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        RefreshToken = c.Guid(nullable: false),
                        ExpiredTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRefreshTokens", "Id", "dbo.Users");
            DropIndex("dbo.UserRefreshTokens", new[] { "Id" });
            DropTable("dbo.UserRefreshTokens");
        }
    }
}
