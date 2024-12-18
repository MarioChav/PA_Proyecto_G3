using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PA_Proyecto_G3;
using PA_Proyecto_G3.Models;

namespace PA_Proyecto_G3.Controllers
{
    public class RoomController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Room
        public ActionResult Index(int? capacidad, string equipamiento)
        {
            // Empezamos con todas las salas
            var salas = db.Sala.AsQueryable();

            // Filtrar por capacidad
            if (capacidad.HasValue)
            {
                salas = salas.Where(s => s.Capacidad >= capacidad.Value);
            }

            // Filtrar por equipamiento
            if (!string.IsNullOrEmpty(equipamiento))
            {
                // Buscamos salas que tengan el equipamiento especificado
                salas = salas.Where(s => s.SalasEquipamientos
                                          .Any(se => se.Equipamiento.Nombre.Contains(equipamiento)));
            }

            return View(salas.ToList());
        }


        // GET: Room/Detalles/5
        public ActionResult Detalles(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sala sala = db.Sala.Find(id);
            if (sala == null)
            {
                return HttpNotFound();
            }
            return View(sala);
        }

        // GET: Room/Crear
        public ActionResult Crear()
        {
            return View();
        }

        // POST: Room/Crear
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear([Bind(Include = "RoomId,Nombre,Disponible,Capacidad,Ubicacion,HoraDisponible,HoraIndisponible")] Sala sala)
        {
            if (ModelState.IsValid)
            {
                db.Sala.Add(sala);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sala);
        }

        // GET: Room/Editar/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sala sala = db.Sala.Find(id);
            if (sala == null)
            {
                return HttpNotFound();
            }
            return View(sala);
        }

        // POST: Room/Editar/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "RoomId,Nombre,Disponible,Capacidad,Ubicacion,HoraDisponible,HoraIndisponible")] Sala sala)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sala).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sala);
        }

        // GET: Room/Eliminar/5
        public ActionResult Eliminar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sala sala = db.Sala.Find(id);
            if (sala == null)
            {
                return HttpNotFound();
            }
            return View(sala);
        }

        // POST: Room/Eliminar/5
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sala sala = db.Sala.Find(id);
            db.Sala.Remove(sala);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
