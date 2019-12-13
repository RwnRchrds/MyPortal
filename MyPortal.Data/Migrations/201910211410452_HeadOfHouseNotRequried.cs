namespace MyPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HeadOfHouseNotRequried : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Pastoral_Houses", new[] { "HeadId" });
            AlterColumn("dbo.Pastoral_Houses", "HeadId", c => c.Int());
            CreateIndex("dbo.Pastoral_Houses", "HeadId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Pastoral_Houses", new[] { "HeadId" });
            AlterColumn("dbo.Pastoral_Houses", "HeadId", c => c.Int(nullable: false));
            CreateIndex("dbo.Pastoral_Houses", "HeadId");
        }
    }
}
