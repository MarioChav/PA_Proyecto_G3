using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PA_Proyecto_G3.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //Tabla para la base de datos *Esto hace la conexión*
        public DbSet<Sala> Sala { get; set; }

        public DbSet<Reservacion> Reservacion { get; set; }

        public DbSet<Equipamiento> Equipamiento { get; set; }

        public DbSet<SalasEquipamiento> SalasEquipamiento { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Sala>().ToTable("Sala");
            modelBuilder.Entity<Reservacion>().ToTable("Reservacion");
            modelBuilder.Entity<Equipamiento>().ToTable("Equipamiento");
            modelBuilder.Entity<SalasEquipamiento>().ToTable("SalasEquipamiento");
            modelBuilder.Entity<Usuario>().ToTable("Usuario");
        }
    }
}