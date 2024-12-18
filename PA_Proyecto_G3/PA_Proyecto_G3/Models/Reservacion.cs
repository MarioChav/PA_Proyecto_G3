using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PA_Proyecto_G3.Models
{
    public class Reservacion
    {
        [Key]
        public int ReservacionId { get; set; }

        [ForeignKey("Sala")]
        public int RoomId { get; set; }
        public Sala Sala { get; set; }

        [ForeignKey("Usuario")]  
        public int UsuarioId { get; set; }

        [Required]
        public DateTime FechaReservacion { get; set; }

        [Required]
        public TimeSpan TiempoInicio { get; set; }

        [Required]
        public TimeSpan TiempoFinal { get; set; }

        [Required]
        [StringLength(20)]
        public string Estatus { get; set; } // Ejemplo: "Confirmada", "Cancelada"
    
}
}