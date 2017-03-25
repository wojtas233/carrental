namespace CarRental.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_vehicle_type_entity_and_connection_changes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cities", "Country_Id", "dbo.Countries");
            DropForeignKey("dbo.Availabilities", "City_Id", "dbo.Cities");
            DropIndex("dbo.Availabilities", new[] { "City_Id" });
            DropIndex("dbo.Cities", new[] { "Country_Id" });
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountryName = c.String(),
                        CityName = c.String(),
                        StreetName = c.String(),
                        PostalCode = c.String(),
                        BuildingNumber = c.Int(nullable: false),
                        OfficeNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Availabilities", "Location_Id", c => c.Int());
            CreateIndex("dbo.Availabilities", "Location_Id");
            AddForeignKey("dbo.Availabilities", "Location_Id", "dbo.Locations", "Id");
            DropColumn("dbo.Availabilities", "City_Id");
            DropTable("dbo.Cities");
            DropTable("dbo.Countries");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Country_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Availabilities", "City_Id", c => c.Int());
            DropForeignKey("dbo.Availabilities", "Location_Id", "dbo.Locations");
            DropIndex("dbo.Availabilities", new[] { "Location_Id" });
            DropColumn("dbo.Availabilities", "Location_Id");
            DropTable("dbo.Locations");
            CreateIndex("dbo.Cities", "Country_Id");
            CreateIndex("dbo.Availabilities", "City_Id");
            AddForeignKey("dbo.Availabilities", "City_Id", "dbo.Cities", "Id");
            AddForeignKey("dbo.Cities", "Country_Id", "dbo.Countries", "Id");
        }
    }
}
