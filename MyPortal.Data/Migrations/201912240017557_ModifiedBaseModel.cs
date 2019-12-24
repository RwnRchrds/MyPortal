namespace MyPortal.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifiedBaseModel : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "person.People", newName: "Person");
            RenameTable(name: "document.Type", newName: "DocumentType");
            RenameTable(name: "communication.Type", newName: "CommunicationType");
            MoveTable(name: "document.PersonDietaryRequirement", newSchema: "medical");
            AlterColumn("communication.EmailAddressType", "Description", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("personnel.ObservationOutcome", "Description", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("personnel.TrainingCertificateStatus", "Description", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("attendance.CodeMeaning", "Code", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("attendance.CodeMeaning", "Description", c => c.String(nullable: false, maxLength: 256));
        }
        
        public override void Down()
        {
            AlterColumn("attendance.CodeMeaning", "Description", c => c.String());
            AlterColumn("attendance.CodeMeaning", "Code", c => c.String());
            AlterColumn("personnel.TrainingCertificateStatus", "Description", c => c.String());
            AlterColumn("personnel.ObservationOutcome", "Description", c => c.String());
            AlterColumn("communication.EmailAddressType", "Description", c => c.String());
            MoveTable(name: "medical.PersonDietaryRequirement", newSchema: "document");
            RenameTable(name: "communication.CommunicationType", newName: "Type");
            RenameTable(name: "document.DocumentType", newName: "Type");
            RenameTable(name: "person.Person", newName: "People");
        }
    }
}
