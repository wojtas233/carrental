namespace CarRental.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fix_relation_vehicle_with_image : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Images", "Vehicle_Id", "dbo.Vehicles");
            DropIndex("dbo.Images", new[] { "Vehicle_Id" });
            CreateTable(
                "dbo.VehicleImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image_Id = c.Int(),
                        Vehicle_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Images", t => t.Image_Id)
                .ForeignKey("dbo.Vehicles", t => t.Vehicle_Id)
                .Index(t => t.Image_Id)
                .Index(t => t.Vehicle_Id);
            
            DropColumn("dbo.Images", "Vehicle_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Images", "Vehicle_Id", c => c.Int());
            DropForeignKey("dbo.VehicleImages", "Vehicle_Id", "dbo.Vehicles");
            DropForeignKey("dbo.VehicleImages", "Image_Id", "dbo.Images");
            DropIndex("dbo.VehicleImages", new[] { "Vehicle_Id" });
            DropIndex("dbo.VehicleImages", new[] { "Image_Id" });
            DropTable("dbo.VehicleImages");
            CreateIndex("dbo.Images", "Vehicle_Id");
            AddForeignKey("dbo.Images", "Vehicle_Id", "dbo.Vehicles", "Id");
        }
    }
}
