namespace PA_Proyecto_G3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tablas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Descripcion = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Nombre = c.String(),
                        Apellidos = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Reservacions",
                c => new
                    {
                        ReservacionId = c.Int(nullable: false, identity: true),
                        RoomId = c.Int(nullable: false),
                        UsuarioId = c.String(nullable: false, maxLength: 128),
                        FechaReservacion = c.DateTime(nullable: false),
                        TiempoInicio = c.Time(nullable: false, precision: 7),
                        TiempoFinal = c.Time(nullable: false, precision: 7),
                        Estatus = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.ReservacionId)
                .ForeignKey("dbo.AspNetUsers", t => t.UsuarioId, cascadeDelete: true)
                .ForeignKey("dbo.Salas", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.RoomId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.Salas",
                c => new
                    {
                        RoomId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 50),
                        Disponible = c.String(nullable: false, maxLength: 50),
                        Capacidad = c.Int(nullable: false),
                        Ubicacion = c.String(nullable: false, maxLength: 300),
                        HoraDisponible = c.Time(nullable: false, precision: 7),
                        HoraIndisponible = c.Time(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.RoomId);
            
            CreateTable(
                "dbo.SalasEquipamientoes",
                c => new
                    {
                        SalaEquipamientoId = c.Int(nullable: false, identity: true),
                        RoomId = c.Int(nullable: false),
                        EquipamientoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SalaEquipamientoId)
                .ForeignKey("dbo.Equipamientoes", t => t.EquipamientoId, cascadeDelete: true)
                .ForeignKey("dbo.Salas", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.RoomId)
                .Index(t => t.EquipamientoId);
            
            CreateTable(
                "dbo.Equipamientoes",
                c => new
                    {
                        EquipamientoId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.EquipamientoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reservacions", "RoomId", "dbo.Salas");
            DropForeignKey("dbo.SalasEquipamientoes", "RoomId", "dbo.Salas");
            DropForeignKey("dbo.SalasEquipamientoes", "EquipamientoId", "dbo.Equipamientoes");
            DropForeignKey("dbo.Reservacions", "UsuarioId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.SalasEquipamientoes", new[] { "EquipamientoId" });
            DropIndex("dbo.SalasEquipamientoes", new[] { "RoomId" });
            DropIndex("dbo.Reservacions", new[] { "UsuarioId" });
            DropIndex("dbo.Reservacions", new[] { "RoomId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.Equipamientoes");
            DropTable("dbo.SalasEquipamientoes");
            DropTable("dbo.Salas");
            DropTable("dbo.Reservacions");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}
