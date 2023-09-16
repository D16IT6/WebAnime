namespace DataModels.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class update_duration_field : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Animes", "Duration", c => c.Int(nullable: false));
        }

        public override void Down()
        {
            AlterColumn("dbo.Animes", "Duration", c => c.String(maxLength: 50));
        }
    }
}
