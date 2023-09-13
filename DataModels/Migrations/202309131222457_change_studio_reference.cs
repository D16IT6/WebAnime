namespace DataModels.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class change_studio_reference : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Animes", "StudioId");
        }

        public override void Down()
        {
            AddColumn("dbo.Animes", "StudioId", c => c.Int());
        }
    }
}
