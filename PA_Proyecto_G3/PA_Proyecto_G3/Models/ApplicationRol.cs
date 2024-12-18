using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PA_Proyecto_G3.Models
{
    public class ApplicationRol : IdentityRole
    {
        public string Descripcion { get; set; }

        public ApplicationRol() : base() { }
    }
}