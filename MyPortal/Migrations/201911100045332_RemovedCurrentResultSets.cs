namespace MyPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedCurrentResultSets : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Assessment_ResultSets", "IsCurrent");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Assessment_ResultSets", "IsCurrent", c => c.Boolean(nullable: false));
        }
    }
}
