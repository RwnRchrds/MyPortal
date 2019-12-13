namespace MyPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAddresses : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Communication_AddressPersons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AddressId = c.Int(nullable: false),
                        PersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Communication_Addresses", t => t.AddressId)
                .ForeignKey("dbo.People_Persons", t => t.AddressId)
                .Index(t => t.AddressId);
            
            CreateTable(
                "dbo.Communication_Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HouseNumber = c.String(),
                        HouseName = c.String(),
                        Apartment = c.String(),
                        Street = c.String(nullable: false, maxLength: 256),
                        District = c.String(maxLength: 256),
                        Town = c.String(nullable: false, maxLength: 256),
                        County = c.String(nullable: false, maxLength: 256),
                        Postcode = c.String(nullable: false, maxLength: 128),
                        Country = c.String(nullable: false, maxLength: 128),
                        Validated = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Communication_EmailAddresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        PersonId = c.Int(nullable: false),
                        Address = c.String(nullable: false, maxLength: 128),
                        Main = c.Boolean(nullable: false),
                        Primary = c.Boolean(nullable: false),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People_Persons", t => t.PersonId)
                .Index(t => t.PersonId);
            
            AddColumn("dbo.People_Staff", "NiNumber", c => c.String(maxLength: 128));
            AddColumn("dbo.People_Staff", "PostNominal", c => c.String(maxLength: 128));
            AddColumn("dbo.People_Staff", "TeachingStaff", c => c.Boolean(nullable: false));
            AddColumn("dbo.People_Persons", "MiddleName", c => c.String(maxLength: 256));
            AddColumn("dbo.People_Persons", "PhotoId", c => c.Int());
            AddColumn("dbo.People_Persons", "NhsNumber", c => c.String(maxLength: 256));
            AddColumn("dbo.People_Persons", "Deceased", c => c.DateTime(storeType: "date"));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Communication_EmailAddresses", "PersonId", "dbo.People_Persons");
            DropForeignKey("dbo.Communication_AddressPersons", "AddressId", "dbo.People_Persons");
            DropForeignKey("dbo.Communication_AddressPersons", "AddressId", "dbo.Communication_Addresses");
            DropIndex("dbo.Communication_EmailAddresses", new[] { "PersonId" });
            DropIndex("dbo.Communication_AddressPersons", new[] { "AddressId" });
            DropColumn("dbo.People_Persons", "Deceased");
            DropColumn("dbo.People_Persons", "NhsNumber");
            DropColumn("dbo.People_Persons", "PhotoId");
            DropColumn("dbo.People_Persons", "MiddleName");
            DropColumn("dbo.People_Staff", "TeachingStaff");
            DropColumn("dbo.People_Staff", "PostNominal");
            DropColumn("dbo.People_Staff", "NiNumber");
            DropTable("dbo.Communication_EmailAddresses");
            DropTable("dbo.Communication_Addresses");
            DropTable("dbo.Communication_AddressPersons");
        }
    }
}
