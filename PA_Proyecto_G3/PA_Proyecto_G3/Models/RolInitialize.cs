using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PA_Proyecto_G3.Models
{
    public class RolInitialize
    {
        public static void Inicializar()
        {
            var rolManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));


            //Roles Preterminados 
            List<String> roles = new List<String>();
            roles.Add("Administrador");
            roles.Add("Usuario");

            foreach (var role in roles)
            {
                if (!rolManager.RoleExists(role))
                {
                    rolManager.Create(new IdentityRole(role));
                }
            }

            //usuario por defecto
            var adminUser = new ApplicationUser { UserName = "admin@workspaces.ac.cr", Email = "admin@workspaces.ac.cr" };
            String contra = "Admin123";

            if (userManager.FindByEmail(adminUser.Email) == null)
            {
                var creacion = userManager.Create(adminUser, contra);
                if (creacion.Succeeded)
                {
                    userManager.AddToRole(adminUser.Id, "Administrador");
                }
            }
        }
    }
}