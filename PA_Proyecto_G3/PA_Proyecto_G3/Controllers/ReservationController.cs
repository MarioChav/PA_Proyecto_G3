using PA_Proyecto_G3.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace PA_Proyecto_G3.Controllers
{
    public class ReservationController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        [Authorize(Roles = "Usuario")]
        // GET: Reservation
        public ActionResult Index()
        {
            var Reservas = context.Reservacion.ToList();
            return View(Reservas);
        }
        //---------------------------------------------------- Vista ------------------------------------------------------------


        [HttpGet]
        public ActionResult GetEvents()
        {
            var reservas = context.Reservacion
                .Include("Sala")
                .Include("ApplicationUser")
                .AsEnumerable()
                .Select(r => new {
                    id = r.ReservacionId,
                    title = $"{r.Sala.Nombre} - {r.ApplicationUser.Nombre} {r.ApplicationUser.Apellidos}",
                    start = r.FechaReservacion.ToString("yyyy-MM-dd") + "T" + r.TiempoInicio.ToString(@"hh\:mm\:ss"),
                    end = r.FechaReservacion.ToString("yyyy-MM-dd") + "T" + r.TiempoFinal.ToString(@"hh\:mm\:ss"),
                    color = r.Estatus == "Cancelada" ? "red" : (r.Estatus == "Aprobada" ? "green" : "orange")
                })
                .ToList();

            return Json(reservas, JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------- Crear ------------------------------------------------------------

        [HttpGet]
        public ActionResult Crear()
        {

            var usuarios = context.Users
             .Select(u => new
                 {
                     u.Id,
                     u.Nombre,
                     u.Apellidos
                 })
             .ToList(); 
            
            
            // Carga los datos en memoria
            var usuariosDescifrados = usuarios.Select(u => new
            {
                u.Id,
                Nombre = Decrypt(u.Nombre) + " " + Decrypt(u.Apellidos)
            }).ToList();

            ViewBag.Id = new SelectList(
             usuariosDescifrados,
             "Id",
             "Nombre"
            );

            ViewBag.RoomID = new SelectList(context.Sala, "RoomID", "Nombre");

            return View();

        
        }

        [HttpPost]
        public ActionResult Crear(Reservacion reservacion)
        {
            if (ModelState.IsValid)
            {
                // Verificar si la sala ya está ocupada en el mismo horario
                var reservaExistente = context.Reservacion
                    .FirstOrDefault(r =>
                        r.RoomId == reservacion.RoomId &&
                        r.FechaReservacion == reservacion.FechaReservacion &&
                        ((reservacion.TiempoInicio >= r.TiempoInicio && reservacion.TiempoInicio < r.TiempoFinal) ||
                         (reservacion.TiempoFinal > r.TiempoInicio && reservacion.TiempoFinal <= r.TiempoFinal) ||
                         (reservacion.TiempoInicio <= r.TiempoInicio && reservacion.TiempoFinal >= r.TiempoFinal)));

                if (reservaExistente != null)
                {
                    // Si la sala está ocupada, cambiar el estatus a "Pendiente"
                    reservacion.Estatus = "Pendiente";
                }
                else
                {
                    // Si la sala está disponible, marcar la reserva como "Aprobada"
                    reservacion.Estatus = "Aprobada";
                }

                // Guardar la reserva
                context.Reservacion.Add(reservacion);
                context.SaveChanges();

                // Redirigir a la vista de índice
                return RedirectToAction("Index");
            }

            // Si el modelo no es válido, regresar la vista con los errores
            return View(reservacion);
        }

        //-------------------------------------------------- Editar -------------------------------------------------------------------------------
        [HttpGet]
        public ActionResult Editar(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var reservacion = context.Reservacion.Find(id);
            if (reservacion == null)
            {
                return HttpNotFound();
            }

            ViewBag.RoomID = new SelectList(context.Sala, "RoomID", "Nombre", reservacion.RoomId);
            ViewBag.UsuarioID = new SelectList(context.ApplicationUser, "Id", "Nombre", reservacion.Id);
            return View(reservacion);
        }

        [HttpPost]
        public ActionResult Editar(Reservacion reservacion)
        {
            if (ModelState.IsValid)
            {
                context.Entry(reservacion).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoomID = new SelectList(context.Sala, "RoomID", "Nombre", reservacion.RoomId);
            var usuarios = context.Users
            .Select(u => new
            {
                u.Id,
                u.Nombre,
                u.Apellidos
            })
            .ToList();


            // Carga los datos en memoria
            var usuariosDescifrados = usuarios.Select(u => new
            {
                u.Id,
                Nombre = Decrypt(u.Nombre) + " " + Decrypt(u.Apellidos)
            }).ToList();

            ViewBag.Id = new SelectList(
             usuariosDescifrados,
             "Id",
             "Nombre"
            );
            return View(reservacion);
        }

        //-------------------------------------------------- Eliminar -----------------------------------------------------------------------------
        [HttpGet]
        public ActionResult Eliminar(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var reservacion = context.Reservacion.SingleOrDefault(l => l.ReservacionId == id);
            if (reservacion == null)
                return HttpNotFound();

            return View(reservacion);
        }

        [HttpPost, ActionName("Eliminar")]
        public ActionResult EliminarConfirmacion(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var reservacion = context.Reservacion.Find(id);
            if (reservacion == null)
                return HttpNotFound();

            context.Reservacion.Remove(reservacion);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        //-------------------------------------------------- Listado Admim ---------------------------------------------------------------------------------
        [HttpGet]
        public ActionResult MostraListado()
        {
            var Reservas = context.Reservacion.ToList();
            return View(Reservas);
        }

        //-------------------------------------------------- Modificación Admim ----------------------------------------------------------------------------
        [HttpGet]
        public ActionResult ModificarReserva(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var reservacion = context.Reservacion.Find(id);
            if (reservacion == null) return HttpNotFound();

            ViewBag.Salas = new SelectList(context.Sala, "RoomID", "Nombre", reservacion.RoomId);
            ViewBag.Usuarios = new SelectList(context.ApplicationUser, "Id", "Nombre", reservacion.Id);

            return View(reservacion);
        }

        [HttpPost]
        public ActionResult ModificarReserva(Reservacion reservacion)
        {
            if (!ModelState.IsValid) return View(reservacion);

            context.Entry(reservacion).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("MostraListado");
        }

        //-------------------------------------------------- Cancelación Admim ----------------------------------------------------------------------------
        [HttpGet]
        public ActionResult CancelarReserva(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var reservacion = context.Reservacion.Find(id);
            if (reservacion == null)
                return HttpNotFound();

            return View(reservacion);
        }


        [HttpPost]
        public ActionResult CancelarReserva(Reservacion reservacion)
        {
            if (!ModelState.IsValid)
                return View(reservacion);

            // Cargar la reservación actual desde la base de datos
            var existingReservacion = context.Reservacion.Find(reservacion.ReservacionId);
            if (existingReservacion == null)
                return HttpNotFound();

            // Actualizar solo el campo Estatus
            existingReservacion.Estatus = reservacion.Estatus;

            // Guardar cambios en la base de datos
            context.Entry(existingReservacion).State = EntityState.Modified;
            context.SaveChanges();

            return RedirectToAction("MostraListado");
        }

        public string Decrypt(string encryptedValue)
        {
            // Implementación de descifrado
            return encryptedValue; 
        }

    }
}


