namespace MyPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSubjectMultipleStaff : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Curriculum_Subjects", "LeaderId", "dbo.People_Staff");
            DropIndex("dbo.Curriculum_Subjects", new[] { "LeaderId" });
            CreateTable(
                "dbo.Curriculum_SubjectStaff",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubjectId = c.Int(nullable: false),
                        StaffMemberId = c.Int(nullable: false),
                        Role = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Curriculum_Subjects", t => t.SubjectId)
                .ForeignKey("dbo.People_Staff", t => t.StaffMemberId)
                .Index(t => t.SubjectId)
                .Index(t => t.StaffMemberId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Curriculum_SubjectStaff", "StaffMemberId", "dbo.People_Staff");
            DropForeignKey("dbo.Curriculum_SubjectStaff", "SubjectId", "dbo.Curriculum_Subjects");
            DropIndex("dbo.Curriculum_SubjectStaff", new[] { "StaffMemberId" });
            DropIndex("dbo.Curriculum_SubjectStaff", new[] { "SubjectId" });
            DropTable("dbo.Curriculum_SubjectStaff");
            CreateIndex("dbo.Curriculum_Subjects", "LeaderId");
            AddForeignKey("dbo.Curriculum_Subjects", "LeaderId", "dbo.People_Staff", "Id");
        }
    }
}
