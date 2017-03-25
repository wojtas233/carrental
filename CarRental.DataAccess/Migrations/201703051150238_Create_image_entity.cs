namespace CarRental.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_image_entity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Path = c.String(),
                        Vehicle_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Vehicles", t => t.Vehicle_Id)
                .Index(t => t.Vehicle_Id);
            
            AddColumn("dbo.VehicleTypes", "Image_Id", c => c.Int());
            CreateIndex("dbo.VehicleTypes", "Image_Id");
            AddForeignKey("dbo.VehicleTypes", "Image_Id", "dbo.Images", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VehicleTypes", "Image_Id", "dbo.Images");
            DropForeignKey("dbo.Images", "Vehicle_Id", "dbo.Vehicles");
            DropIndex("dbo.VehicleTypes", new[] { "Image_Id" });
            DropIndex("dbo.Images", new[] { "Vehicle_Id" });
            DropColumn("dbo.VehicleTypes", "Image_Id");
            DropTable("dbo.Images");
        }
    }
}
