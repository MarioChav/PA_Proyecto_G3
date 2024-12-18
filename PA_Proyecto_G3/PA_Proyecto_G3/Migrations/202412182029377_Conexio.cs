namespace PA_Proyecto_G3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Conexio : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Reservacions", newName: "Reservacion");
            RenameTable(name: "dbo.Salas", newName: "Sala");
            RenameTable(name: "dbo.SalasEquipamientoes", newName: "SalasEquipamiento");
            RenameTable(name: "dbo.Equipamientoes", newName: "Equipamiento");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Equipamiento", newName: "Equipamientoes");
            RenameTable(name: "dbo.SalasEquipamiento", newName: "SalasEquipamientoes");
            RenameTable(name: "dbo.Sala", newName: "Salas");
            RenameTable(name: "dbo.Reservacion", newName: "Reservacions");
        }
    }
}
