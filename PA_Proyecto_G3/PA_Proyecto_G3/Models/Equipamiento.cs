using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PA_Proyecto_G3.Models
{
    public class Equipamiento
    {
        [Key]
        public int EquipamientoId { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; } // Ejemplo: "Proyector", "Pizarra", "Monitor"

        // Relación con SalasEquipamiento
        public ICollection<SalasEquipamiento> SalasEquipamientos { get; set; } = new List<SalasEquipamiento>();
    
}
}