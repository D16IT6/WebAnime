namespace DataModels.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class create_BlogCategories_Table : DbMigration
    {
        public override void Up()
        {
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

        }

        public override void Down()
        {
            DropForeignKey("dbo.BlogInCategories", "BlogCategoryId", "dbo.BlogCategories");
            DropForeignKey("dbo.BlogInCategories", "BlogId", "dbo.Blogs");
            DropIndex("dbo.BlogInCategories", new[] { "BlogCategoryId" });
            DropIndex("dbo.BlogInCategories", new[] { "BlogId" });
            DropTable("dbo.BlogInCategories");
            DropTable("dbo.BlogCategories");
        }
    }
}
