namespace MyPortal.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedColourCodeToObservationOutcome : DbMigration
    {
        public override void Up()
        {
            AddColumn("personnel.ObservationOutcome", "ColourCode", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("personnel.ObservationOutcome", "ColourCode");
        }
    }
}
