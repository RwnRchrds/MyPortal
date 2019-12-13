namespace MyPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeSenStatusTableName : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Sen_Statuses", newName: "Sen_Status");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Sen_Status", newName: "Sen_Statuses");
        }
    }
}
