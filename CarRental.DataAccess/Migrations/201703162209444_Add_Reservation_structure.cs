namespace CarRental.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Reservation_structure : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReservationEquipments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Equipment_Id = c.Int(),
                        Reservation_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Equipments", t => t.Equipment_Id)
                .ForeignKey("dbo.Reservations", t => t.Reservation_Id)
                .Index(t => t.Equipment_Id)
                .Index(t => t.Reservation_Id);
            
            AddColumn("dbo.Equipments", "Reservation_Id", c => c.Int());
            CreateIndex("dbo.Equipments", "Reservation_Id");
            AddForeignKey("dbo.Equipments", "Reservation_Id", "dbo.Reservations", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReservationEquipments", "Reservation_Id", "dbo.Reservations");
            DropForeignKey("dbo.ReservationEquipments", "Equipment_Id", "dbo.Equipments");
            DropForeignKey("dbo.Equipments", "Reservation_Id", "dbo.Reservations");
            DropIndex("dbo.ReservationEquipments", new[] { "Reservation_Id" });
            DropIndex("dbo.ReservationEquipments", new[] { "Equipment_Id" });
            DropIndex("dbo.Equipments", new[] { "Reservation_Id" });
            DropColumn("dbo.Equipments", "Reservation_Id");
            DropTable("dbo.ReservationEquipments");
        }
    }
}
