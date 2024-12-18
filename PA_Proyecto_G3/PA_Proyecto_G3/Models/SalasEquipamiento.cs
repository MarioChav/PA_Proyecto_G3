using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PA_Proyecto_G3.Models
{
    public class SalasEquipamiento
    {
        [Key]
        public int SalaEquipamientoId { get; set; }

        [ForeignKey("Sala")]
        public int RoomId { get; set; }
        public Sala Sala { get; set; }

        [ForeignKey("Equipamiento")]
        public int EquipamientoId { get; set; }
        public Equipamiento Equipamiento { get; set; }
    }
}