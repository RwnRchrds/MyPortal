namespace MyPortal.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedHouseColourCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("pastoral.House", "ColourCode", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("pastoral.House", "ColourCode");
        }
    }
}
