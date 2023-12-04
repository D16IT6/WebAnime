namespace DataModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateScheduleTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Schedules", "AiringDate", c => c.DateTime(nullable: false, storeType: "date"));
            AddColumn("dbo.Schedules", "AiringTime", c => c.Time(nullable: false, precision: 7));
            //DropColumn("dbo.Schedules", "DayOfWeek");
            //DropColumn("dbo.Schedules", "Time");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.Schedules", "Time", c => c.DateTime(nullable: false));
            //AddColumn("dbo.Schedules", "DayOfWeek", c => c.Int(nullable: false));
            DropColumn("dbo.Schedules", "AiringTime");
            DropColumn("dbo.Schedules", "AiringDate");
        }
    }
}
