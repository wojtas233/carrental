namespace CarRental.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_IsEnabled_property_to_VehicleType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VehicleTypes", "IsEnabled", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.VehicleTypes", "IsEnabled");
        }
    }
}
