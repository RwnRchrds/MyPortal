namespace MyPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.People_Contacts", "PersonId", "dbo.People_Persons");
            AddForeignKey("dbo.People_Contacts", "PersonId", "dbo.People_Persons", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.People_Contacts", "PersonId", "dbo.People_Persons");
            AddForeignKey("dbo.People_Contacts", "PersonId", "dbo.People_Persons", "Id", cascadeDelete: true);
        }
    }
}
