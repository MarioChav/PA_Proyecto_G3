using PA_Proyecto_G3.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PA_Proyecto_G3.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        [Authorize(Roles = "Administrador")]
        // GET: Admin
        public ActionResult Index()
        {
            return RedirectToAction("SalaUso");
        }

        //Estadísticas 
        public ActionResult GetRoomUsageData()
        {
            var roomUsage = context.Reservacion
                .GroupBy(r => r.Sala.Nombre)
                .Select(g => new {
                    Sala = g.Key,
                    TotalHoras = g.Sum(r => DbFunctions.DiffHours(r.TiempoInicio, r.TiempoFinal)), // Total de horas reservadas por sala
                    Reservas = g.Count() // Total de reservaciones
                })
                .ToList();

            return Json(roomUsage, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult SalaUso()
        {
            return View();
        }

    }
}