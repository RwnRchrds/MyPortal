namespace MyPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifiedSystemClasses : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Assessment_GradeSets", "System", c => c.Boolean(nullable: false));
            AddColumn("dbo.Profile_CommentBanks", "InUse", c => c.Boolean(nullable: false));
            DropColumn("dbo.Profile_LogTypes", "System");
            DropColumn("dbo.Attendance_Codes", "System");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Attendance_Codes", "System", c => c.Boolean(nullable: false));
            AddColumn("dbo.Profile_LogTypes", "System", c => c.Boolean(nullable: false));
            DropColumn("dbo.Profile_CommentBanks", "InUse");
            DropColumn("dbo.Assessment_GradeSets", "System");
        }
    }
}
