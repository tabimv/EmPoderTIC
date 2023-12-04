using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EmPoderTIC.Models;
using System.Web.Configuration;
using System.Globalization;

namespace EmPoderTIC.Controllers
{
    public class AsistenciaController : Controller
    {
        private EmPoderTIC_Conexion_Oficial_WEB db = new EmPoderTIC_Conexion_Oficial_WEB();

        // GET: Asistencia
        public async Task<ActionResult> Index()
        {
            var aSISTENCIA = db.ASISTENCIA.Include(a => a.EVENTO).Include(a => a.USUARIO);
            return View(await aSISTENCIA.ToListAsync());
        }

        public async Task<ActionResult> EventoLista()
        {
            var eventos = await db.EVENTO
                        .OrderBy(e => e.evento_id)
                        .ToListAsync();

            return View(eventos);
        }

        public async Task<ActionResult> EventDetails(int eventoId)
        {
            var asistentes = await db.ASISTENCIA
                .Where(a => a.EVENTO_evento_id == eventoId)
                .Include(a => a.USUARIO)
                .OrderBy(a => a.fecha_registro_asistencia)
                .ToListAsync();

            return View(asistentes);
        }


        // GET: Asistencia/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ASISTENCIA aSISTENCIA = await db.ASISTENCIA.FindAsync(id);
            if (aSISTENCIA == null)
            {
                return HttpNotFound();
            }
            return View(aSISTENCIA);
        }

        // GET: Asistencia/Create
        public ActionResult Create()
        {
            ViewBag.EVENTO_evento_id = new SelectList(db.EVENTO, "evento_id", "nombre");
            ViewBag.USUARIO_rut = new SelectList(db.USUARIO, "rut", "nombre");
            return View();
        }

        // POST: Asistencia/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "registro_asistencia_evento,fecha_registro_asistencia,USUARIO_rut,EVENTO_evento_id")] ASISTENCIA aSISTENCIA)
        {
            if (ModelState.IsValid)
            {
                db.ASISTENCIA.Add(aSISTENCIA);
                await db.SaveChangesAsync();
                return RedirectToAction("EventoLista");
            }

            ViewBag.EVENTO_evento_id = new SelectList(db.EVENTO, "evento_id", "nombre", aSISTENCIA.EVENTO_evento_id);
            ViewBag.USUARIO_rut = new SelectList(db.USUARIO, "rut", "nombre", aSISTENCIA.USUARIO_rut);
            return View(aSISTENCIA);
        }

        // GET: Asistencia/Edit/5
        public async Task<ActionResult> Edit(string USUARIO_rut, int? EVENTO_evento_id)
        {
            if (string.IsNullOrEmpty(USUARIO_rut) || EVENTO_evento_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Buscar el registro de asistencia en función del usuario y el evento
            ASISTENCIA aSISTENCIA = await db.ASISTENCIA.FirstOrDefaultAsync(a =>
                a.USUARIO_rut == USUARIO_rut && a.EVENTO_evento_id == EVENTO_evento_id);

            if (aSISTENCIA == null)
            {
                return HttpNotFound();
            }

            ViewBag.EVENTO_evento_id = new SelectList(db.EVENTO, "evento_id", "nombre", aSISTENCIA.EVENTO_evento_id);
            ViewBag.USUARIO_rut = new SelectList(db.USUARIO, "rut", "nombre", aSISTENCIA.USUARIO_rut);

            return View(aSISTENCIA);
        }

        // POST: Asistencia/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "registro_asistencia_evento,fecha_registro_asistencia,USUARIO_rut,EVENTO_evento_id")] ASISTENCIA aSISTENCIA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aSISTENCIA).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("EventoLista");
            }
            ViewBag.EVENTO_evento_id = new SelectList(db.EVENTO, "evento_id", "nombre", aSISTENCIA.EVENTO_evento_id);
            ViewBag.USUARIO_rut = new SelectList(db.USUARIO, "rut", "nombre", aSISTENCIA.USUARIO_rut);
            return View(aSISTENCIA);
        }

        // GET: Asistencia/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ASISTENCIA aSISTENCIA = await db.ASISTENCIA.FindAsync(id);
            if (aSISTENCIA == null)
            {
                return HttpNotFound();
            }
            return View(aSISTENCIA);
        }

        // POST: Asistencia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            ASISTENCIA aSISTENCIA = await db.ASISTENCIA.FindAsync(id);
            db.ASISTENCIA.Remove(aSISTENCIA);
            await db.SaveChangesAsync();
            return RedirectToAction("EventoLista");
        }

        public ActionResult Crear()
        {
            ViewBag.Eventos = new SelectList(db.EVENTO.ToList(), "evento_id", "nombre");
            ViewBag.Usuarios = new SelectList(db.USUARIO.ToList(), "rut", "rut");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GuardarRegistros(FormCollection form)
        {
            if (ModelState.IsValid)
            {
               
                int eventoID = int.Parse(form["eventoID"]);

              
                for (int i = 0; i < form.Count / 4; i++) // Suponiendo que por cada registro hay 3 campos (fecha, usuario y evento)
                {
                    var nuevoItem = new ASISTENCIA
                    {
                        registro_asistencia_evento = Convert.ToBoolean(form[$"asistencia[{i}].registro_asistencia_evento"]),
                        fecha_registro_asistencia = DateTime.Parse(form[$"asistencia[{i}].fecha_registro_asistencia"]),
                        USUARIO_rut = form[$"asistencia[{i}].USUARIO_rut"],
                        EVENTO_evento_id = eventoID
                    };

                    db.ASISTENCIA.Add(nuevoItem);
                }

                db.SaveChanges();
                return RedirectToAction("EventoLista");
            }

            // En caso de errores, regresar a la vista
            ViewBag.Eventos = new SelectList(db.EVENTO.ToList(), "evento_id", "nombre");
            ViewBag.Usuarios = new SelectList(db.USUARIO.ToList(), "rut", "rut");
            return View("Crear");
        }


        public async Task<ActionResult> EditarAsistencia(int eventoId)
        {
            // Obtener la información del evento
            var evento = await db.EVENTO.FindAsync(eventoId);
            ViewBag.Evento = evento;

            // Obtener la lista de asistencia para el evento
            var asistencia = await db.ASISTENCIA
                .Where(a => a.EVENTO_evento_id == eventoId)
                .Include(a => a.USUARIO)
                .ToListAsync();

            return View(asistencia);
        }



        // Método asincrónico GET para mostrar la vista de edición de asistencia
        public async Task<ActionResult> EditarAsistenciaEvento(int eventoId)
        {
            var evento = await db.EVENTO.FindAsync(eventoId);
            ViewBag.NombreEvento = evento.nombre;

            var usuarios = await db.USUARIO
            .Where(u => db.ASISTENCIA.Any(a => a.USUARIO_rut == u.rut && a.EVENTO_evento_id == eventoId))
            .ToListAsync();


            var asistencias = new List<ASISTENCIA>();

            foreach (var usuario in usuarios)
            {
                var asistencia = await db.ASISTENCIA.FirstOrDefaultAsync(a => a.USUARIO_rut == usuario.rut && a.EVENTO_evento_id == eventoId);

                if (asistencia == null)
                {
                    asistencia = new ASISTENCIA
                    {
                        USUARIO_rut = usuario.rut,
                        EVENTO_evento_id = eventoId,
                        fecha_registro_asistencia = DateTime.Now,
                        registro_asistencia_evento = false
                    };
                }

                asistencias.Add(asistencia);
            }

            ViewBag.Eventos = new SelectList(db.EVENTO.ToList(), "evento_id", "nombre");
            return View(asistencias);
        }


        // Método asincrónico POST para guardar la asistencia editada
        [HttpPost]
        public async Task<ActionResult> EditarAsistenciaEvento(List<ASISTENCIA> asistencias)
        {
            if (ModelState.IsValid)
            {
                foreach (var asistencia in asistencias)
                {
                    asistencia.fecha_registro_asistencia = DateTime.Now;
                    db.Entry(asistencia).State = EntityState.Modified;
                }

                await db.SaveChangesAsync();

                return RedirectToAction("EventoLista", "Asistencia"); // Redirigir a la página de inicio después de guardar
            }

            ViewBag.Eventos = new SelectList(db.EVENTO.ToList(), "evento_id", "nombre");
            return View(asistencias); // Mostrar la vista de nuevo si hay errores de validación
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
