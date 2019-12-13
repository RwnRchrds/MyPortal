namespace MyPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SenStatusOptional : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.People_Students", "SenStatusId", "dbo.Sen_Status");
            DropIndex("dbo.People_Students", new[] { "SenStatusId" });
            AlterColumn("dbo.People_Students", "SenStatusId", c => c.Int());
            CreateIndex("dbo.People_Students", "SenStatusId");
            AddForeignKey("dbo.People_Students", "SenStatusId", "dbo.Sen_Status", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.People_Students", "SenStatusId", "dbo.Sen_Status");
            DropIndex("dbo.People_Students", new[] { "SenStatusId" });
            AlterColumn("dbo.People_Students", "SenStatusId", c => c.Int(nullable: false));
            CreateIndex("dbo.People_Students", "SenStatusId");
            AddForeignKey("dbo.People_Students", "SenStatusId", "dbo.Sen_Status", "Id", cascadeDelete: true);
        }
    }
}
