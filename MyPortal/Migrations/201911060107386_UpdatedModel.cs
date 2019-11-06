namespace MyPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedModel : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.People_ContactTypes", newName: "People_RelationshipTypes");
            DropForeignKey("dbo.Attendance_Codes", "MeaningId", "dbo.Attendance_Meanings");
            DropIndex("dbo.Attendance_Codes", new[] { "MeaningId" });
            AddColumn("dbo.People_Students", "DateStarting", c => c.DateTime(storeType: "date"));
            AddColumn("dbo.People_Students", "DateLeaving", c => c.DateTime(storeType: "date"));
            AddColumn("dbo.Attendance_Codes", "Meaning", c => c.Int(nullable: false));
            CreateIndex("dbo.People_Staff", "Code", unique: true);
            DropColumn("dbo.Attendance_Codes", "MeaningId");
            DropTable("dbo.Attendance_Meanings");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Attendance_Meanings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 128),
                        Description = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Attendance_Codes", "MeaningId", c => c.Int(nullable: false));
            DropIndex("dbo.People_Staff", new[] { "Code" });
            DropColumn("dbo.Attendance_Codes", "Meaning");
            DropColumn("dbo.People_Students", "DateLeaving");
            DropColumn("dbo.People_Students", "DateStarting");
            CreateIndex("dbo.Attendance_Codes", "MeaningId");
            AddForeignKey("dbo.Attendance_Codes", "MeaningId", "dbo.Attendance_Meanings", "Id");
            RenameTable(name: "dbo.People_RelationshipTypes", newName: "People_ContactTypes");
        }
    }
}
