namespace MyPortal.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelIntegration : DbMigration
    {
        public override void Up()
        {
            AddColumn("assessment.Result", "GradeId", c => c.Int(nullable: false));
            CreateIndex("assessment.Result", "GradeId");
            AddForeignKey("assessment.Result", "GradeId", "assessment.Grade", "Id");
            DropColumn("assessment.Result", "Grade");
        }
        
        public override void Down()
        {
            AddColumn("assessment.Result", "Grade", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("assessment.Result", "GradeId", "assessment.Grade");
            DropIndex("assessment.Result", new[] { "GradeId" });
            DropColumn("assessment.Result", "GradeId");
        }
    }
}
