namespace CarRental.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Change_table_name_AdditionalEquipments_to_Equipments : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AdditionalEquipments", newName: "Equipments");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Equipments", newName: "AdditionalEquipments");
        }
    }
}
