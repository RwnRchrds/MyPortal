namespace MyPortal.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMoreColourCodeColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("profile.LogNoteType", "ColourCode", c => c.String(maxLength: 128));
            AddColumn("personnel.TrainingCertificateStatus", "ColourCode", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("personnel.TrainingCertificateStatus", "ColourCode");
            DropColumn("profile.LogNoteType", "ColourCode");
        }
    }
}
