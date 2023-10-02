namespace DataModels.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UpdateField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AgeRatings", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AgeRatings", "CreatedBy", c => c.Int(nullable: false));
            AddColumn("dbo.AgeRatings", "ModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AgeRatings", "ModifiedBy", c => c.Int(nullable: false));
            AddColumn("dbo.AgeRatings", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.AgeRatings", "DeletedBy", c => c.Int());
            AddColumn("dbo.AgeRatings", "DeletedDate", c => c.DateTime());
            AddColumn("dbo.Animes", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Animes", "CreatedBy", c => c.Int(nullable: false));
            AddColumn("dbo.Animes", "ModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Animes", "ModifiedBy", c => c.Int(nullable: false));
            AddColumn("dbo.Animes", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Animes", "DeletedBy", c => c.Int());
            AddColumn("dbo.Animes", "DeletedDate", c => c.DateTime());
            AddColumn("dbo.Categories", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Categories", "CreatedBy", c => c.Int(nullable: false));
            AddColumn("dbo.Categories", "ModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Categories", "ModifiedBy", c => c.Int(nullable: false));
            AddColumn("dbo.Categories", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Categories", "DeletedBy", c => c.Int());
            AddColumn("dbo.Categories", "DeletedDate", c => c.DateTime());
            AddColumn("dbo.Countries", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Countries", "CreatedBy", c => c.Int(nullable: false));
            AddColumn("dbo.Countries", "ModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Countries", "ModifiedBy", c => c.Int(nullable: false));
            AddColumn("dbo.Countries", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Countries", "DeletedBy", c => c.Int());
            AddColumn("dbo.Countries", "DeletedDate", c => c.DateTime());
            AddColumn("dbo.Episodes", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Episodes", "CreatedBy", c => c.Int(nullable: false));
            AddColumn("dbo.Episodes", "ModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Episodes", "ModifiedBy", c => c.Int(nullable: false));
            AddColumn("dbo.Episodes", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Episodes", "DeletedBy", c => c.Int());
            AddColumn("dbo.Episodes", "DeletedDate", c => c.DateTime());
            AddColumn("dbo.Servers", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Servers", "CreatedBy", c => c.Int(nullable: false));
            AddColumn("dbo.Servers", "ModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Servers", "ModifiedBy", c => c.Int(nullable: false));
            AddColumn("dbo.Servers", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Servers", "DeletedBy", c => c.Int());
            AddColumn("dbo.Servers", "DeletedDate", c => c.DateTime());
            AddColumn("dbo.Statuses", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Statuses", "CreatedBy", c => c.Int(nullable: false));
            AddColumn("dbo.Statuses", "ModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Statuses", "ModifiedBy", c => c.Int(nullable: false));
            AddColumn("dbo.Statuses", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Statuses", "DeletedBy", c => c.Int());
            AddColumn("dbo.Statuses", "DeletedDate", c => c.DateTime());
            AddColumn("dbo.Studios", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Studios", "CreatedBy", c => c.Int(nullable: false));
            AddColumn("dbo.Studios", "ModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Studios", "ModifiedBy", c => c.Int(nullable: false));
            AddColumn("dbo.Studios", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Studios", "DeletedBy", c => c.Int());
            AddColumn("dbo.Studios", "DeletedDate", c => c.DateTime());
            AddColumn("dbo.Types", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Types", "CreatedBy", c => c.Int(nullable: false));
            AddColumn("dbo.Types", "ModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Types", "ModifiedBy", c => c.Int(nullable: false));
            AddColumn("dbo.Types", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Types", "DeletedBy", c => c.Int());
            AddColumn("dbo.Types", "DeletedDate", c => c.DateTime());
        }

        public override void Down()
        {
            DropColumn("dbo.Types", "DeletedDate");
            DropColumn("dbo.Types", "DeletedBy");
            DropColumn("dbo.Types", "IsDeleted");
            DropColumn("dbo.Types", "ModifiedBy");
            DropColumn("dbo.Types", "ModifiedDate");
            DropColumn("dbo.Types", "CreatedBy");
            DropColumn("dbo.Types", "CreatedDate");
            DropColumn("dbo.Studios", "DeletedDate");
            DropColumn("dbo.Studios", "DeletedBy");
            DropColumn("dbo.Studios", "IsDeleted");
            DropColumn("dbo.Studios", "ModifiedBy");
            DropColumn("dbo.Studios", "ModifiedDate");
            DropColumn("dbo.Studios", "CreatedBy");
            DropColumn("dbo.Studios", "CreatedDate");
            DropColumn("dbo.Statuses", "DeletedDate");
            DropColumn("dbo.Statuses", "DeletedBy");
            DropColumn("dbo.Statuses", "IsDeleted");
            DropColumn("dbo.Statuses", "ModifiedBy");
            DropColumn("dbo.Statuses", "ModifiedDate");
            DropColumn("dbo.Statuses", "CreatedBy");
            DropColumn("dbo.Statuses", "CreatedDate");
            DropColumn("dbo.Servers", "DeletedDate");
            DropColumn("dbo.Servers", "DeletedBy");
            DropColumn("dbo.Servers", "IsDeleted");
            DropColumn("dbo.Servers", "ModifiedBy");
            DropColumn("dbo.Servers", "ModifiedDate");
            DropColumn("dbo.Servers", "CreatedBy");
            DropColumn("dbo.Servers", "CreatedDate");
            DropColumn("dbo.Episodes", "DeletedDate");
            DropColumn("dbo.Episodes", "DeletedBy");
            DropColumn("dbo.Episodes", "IsDeleted");
            DropColumn("dbo.Episodes", "ModifiedBy");
            DropColumn("dbo.Episodes", "ModifiedDate");
            DropColumn("dbo.Episodes", "CreatedBy");
            DropColumn("dbo.Episodes", "CreatedDate");
            DropColumn("dbo.Countries", "DeletedDate");
            DropColumn("dbo.Countries", "DeletedBy");
            DropColumn("dbo.Countries", "IsDeleted");
            DropColumn("dbo.Countries", "ModifiedBy");
            DropColumn("dbo.Countries", "ModifiedDate");
            DropColumn("dbo.Countries", "CreatedBy");
            DropColumn("dbo.Countries", "CreatedDate");
            DropColumn("dbo.Categories", "DeletedDate");
            DropColumn("dbo.Categories", "DeletedBy");
            DropColumn("dbo.Categories", "IsDeleted");
            DropColumn("dbo.Categories", "ModifiedBy");
            DropColumn("dbo.Categories", "ModifiedDate");
            DropColumn("dbo.Categories", "CreatedBy");
            DropColumn("dbo.Categories", "CreatedDate");
            DropColumn("dbo.Animes", "DeletedDate");
            DropColumn("dbo.Animes", "DeletedBy");
            DropColumn("dbo.Animes", "IsDeleted");
            DropColumn("dbo.Animes", "ModifiedBy");
            DropColumn("dbo.Animes", "ModifiedDate");
            DropColumn("dbo.Animes", "CreatedBy");
            DropColumn("dbo.Animes", "CreatedDate");
            DropColumn("dbo.AgeRatings", "DeletedDate");
            DropColumn("dbo.AgeRatings", "DeletedBy");
            DropColumn("dbo.AgeRatings", "IsDeleted");
            DropColumn("dbo.AgeRatings", "ModifiedBy");
            DropColumn("dbo.AgeRatings", "ModifiedDate");
            DropColumn("dbo.AgeRatings", "CreatedBy");
            DropColumn("dbo.AgeRatings", "CreatedDate");
        }
    }
}
