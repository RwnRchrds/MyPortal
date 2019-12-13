namespace MyPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtendedAttendanceMarks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Attendance_Marks", "Comments", c => c.String(maxLength: 256));
            AddColumn("dbo.Attendance_Marks", "MinutesLate", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Attendance_Marks", "MinutesLate");
            DropColumn("dbo.Attendance_Marks", "Comments");
        }
    }
}
