using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PA_Proyecto_G3.Models
{
    public class Usuario
    {
        [Key]  
        public int UsuarioId { get; set; }

        [Required]  
        [StringLength(50)]  
        public string Nombre { get; set; }

        [Required]
        [StringLength(100)]  
        public string Apellidos { get; set; }

        [Required]
        [StringLength(255)]  
        public string PasswordH { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]  
        public string Email { get; set; }

        [StringLength(20)]  
        public string Rol { get; set; }

        // Relación con Reservacion
        public ICollection<Reservacion> Reservaciones { get; set; } 
    }

}