namespace CarRental.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_price_to_vehicle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vehicles", "PricePerHour", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vehicles", "PricePerHour");
        }
    }
}
