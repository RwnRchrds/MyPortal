namespace MyPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLEAs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.System_LocalAuthorities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LeaCode = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 128),
                        Website = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.System_Schools", "LocalAuthorityId", c => c.Int(nullable: false));
            CreateIndex("dbo.System_Schools", "LocalAuthorityId");
            AddForeignKey("dbo.System_Schools", "LocalAuthorityId", "dbo.System_LocalAuthorities", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.System_Schools", "LocalAuthorityId", "dbo.System_LocalAuthorities");
            DropIndex("dbo.System_Schools", new[] { "LocalAuthorityId" });
            AlterColumn("dbo.System_Schools", "LocalAuthorityId", c => c.Int());
            DropTable("dbo.System_LocalAuthorities");
        }
    }
}
