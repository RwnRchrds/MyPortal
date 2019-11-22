namespace MyPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSubjectRoles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Curriculum_SubjectStaffMemberRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Curriculum_SubjectStaff", "RoleId", c => c.Int(nullable: false));
            CreateIndex("dbo.Curriculum_SubjectStaff", "RoleId");
            AddForeignKey("dbo.Curriculum_SubjectStaff", "RoleId", "dbo.Curriculum_SubjectStaffMemberRoles", "Id");
            DropColumn("dbo.Curriculum_Subjects", "LeaderId");
            DropColumn("dbo.Curriculum_SubjectStaff", "Role");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Curriculum_SubjectStaff", "Role", c => c.String(nullable: false, maxLength: 256));
            AddColumn("dbo.Curriculum_Subjects", "LeaderId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Curriculum_SubjectStaff", "RoleId", "dbo.Curriculum_SubjectStaffMemberRoles");
            DropIndex("dbo.Curriculum_SubjectStaff", new[] { "RoleId" });
            DropColumn("dbo.Curriculum_SubjectStaff", "RoleId");
            DropTable("dbo.Curriculum_SubjectStaffMemberRoles");
        }
    }
}
