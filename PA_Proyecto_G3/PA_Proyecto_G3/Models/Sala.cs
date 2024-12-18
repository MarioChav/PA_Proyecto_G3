using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PA_Proyecto_G3.Models
{
    public class Sala
    {
        [Key]
        public int RoomId { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string Disponible { get; set; } // Valores posibles: "Disponible", "Indisponible"

        [Required]
        public int Capacidad { get; set; }

        [Required]
        [StringLength(300)]
        public string Ubicacion { get; set; }

        [Required]
        public TimeSpan HoraDisponible { get; set; } // Hora de inicio de disponibilidad.

        [Required]
        public TimeSpan HoraIndisponible { get; set; } // Hora de fin de disponibilidad.

        // Relación con SalasEquipamiento
        public ICollection<SalasEquipamiento> SalasEquipamientos { get; set; } = new List<SalasEquipamiento>();

        // Relación con Reservaciones
        public ICollection<Reservacion> Reservaciones { get; set; } = new List<Reservacion>();
    
}
}