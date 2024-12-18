namespace PA_Proyecto_G3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modificacion : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Reservacions", name: "UsuarioId", newName: "Id");
            RenameIndex(table: "dbo.Reservacions", name: "IX_UsuarioId", newName: "IX_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Reservacions", name: "IX_Id", newName: "IX_UsuarioId");
            RenameColumn(table: "dbo.Reservacions", name: "Id", newName: "UsuarioId");
        }
    }
}
