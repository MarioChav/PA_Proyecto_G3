namespace PA_Proyecto_G3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cambiosT : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reservacion", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.Reservacion", new[] { "Id" });
            RenameColumn(table: "dbo.Reservacion", name: "Id", newName: "ApplicationUser_Id");
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        UsuarioId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 50),
                        Apellidos = c.String(nullable: false, maxLength: 100),
                        PasswordH = c.String(nullable: false, maxLength: 255),
                        Email = c.String(nullable: false, maxLength: 100),
                        Rol = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.UsuarioId);
            
            AddColumn("dbo.Reservacion", "UsuarioId", c => c.Int(nullable: false));
            AlterColumn("dbo.Reservacion", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Reservacion", "UsuarioId");
            CreateIndex("dbo.Reservacion", "ApplicationUser_Id");
            AddForeignKey("dbo.Reservacion", "UsuarioId", "dbo.Usuario", "UsuarioId", cascadeDelete: true);
            AddForeignKey("dbo.Reservacion", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservacion", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reservacion", "UsuarioId", "dbo.Usuario");
            DropIndex("dbo.Reservacion", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Reservacion", new[] { "UsuarioId" });
            AlterColumn("dbo.Reservacion", "ApplicationUser_Id", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Reservacion", "UsuarioId");
            DropTable("dbo.Usuario");
            RenameColumn(table: "dbo.Reservacion", name: "ApplicationUser_Id", newName: "Id");
            CreateIndex("dbo.Reservacion", "Id");
            AddForeignKey("dbo.Reservacion", "Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
