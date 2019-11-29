namespace MyPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedStudentContacts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.People_StudentContacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContactTypeId = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                        ContactId = c.Int(nullable: false),
                        Correspondence = c.Boolean(nullable: false),
                        ParentalResponsibility = c.Boolean(nullable: false),
                        PupilReport = c.Boolean(nullable: false),
                        CourtOrder = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People_Contacts", t => t.ContactId)
                .ForeignKey("dbo.People_Students", t => t.StudentId)
                .Index(t => t.StudentId)
                .Index(t => t.ContactId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.People_StudentContacts", "StudentId", "dbo.People_Students");
            DropForeignKey("dbo.People_StudentContacts", "ContactId", "dbo.People_Contacts");
            DropIndex("dbo.People_StudentContacts", new[] { "ContactId" });
            DropIndex("dbo.People_StudentContacts", new[] { "StudentId" });
            DropTable("dbo.People_StudentContacts");
        }
    }
}
