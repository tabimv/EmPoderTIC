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

namespace EmPoderTIC.Controllers
{
    public class OtorgarInsigniaPerfil3Controller : Controller
    {
        private EmPoderTIC_Conexion_Oficial_WEB db = new EmPoderTIC_Conexion_Oficial_WEB();

        // GET: OtorgarInsigniaPerfil3
        public async Task<ActionResult> Index()
        {
            var oTORGAR_INSIGNIA_P3 = db.OTORGAR_INSIGNIA_P3.Include(o => o.EVENTO).Include(o => o.USUARIO);
            return View(await oTORGAR_INSIGNIA_P3.ToListAsync());
        }

        public async Task<ActionResult> EventoOtorgarInsigniaLista()
        {
            var eventos = await db.EVENTO.ToListAsync();
            return View(eventos);
        }

        public async Task<ActionResult> EventDetails(int eventoId)
        {
            var asistentes = await db.OTORGAR_INSIGNIA_P3
                .Where(a => a.EVENTO_evento_id == eventoId)
                .Include(a => a.USUARIO)
                .ToListAsync();

            return View(asistentes);
        }

        // GET: OtorgarInsigniaPerfil3/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OTORGAR_INSIGNIA_P3 oTORGAR_INSIGNIA_P3 = await db.OTORGAR_INSIGNIA_P3.FindAsync(id);
            if (oTORGAR_INSIGNIA_P3 == null)
            {
                return HttpNotFound();
            }
            return View(oTORGAR_INSIGNIA_P3);
        }

        // GET: OtorgarInsigniaPerfil3/Create
        public ActionResult Create()
        {
            ViewBag.EVENTO_evento_id = new SelectList(db.EVENTO, "evento_id", "nombre");
            ViewBag.USUARIO_rut = new SelectList(db.USUARIO, "rut", "nombre");
            return View();
        }

        // POST: OtorgarInsigniaPerfil3/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "registro_insignia_evento,contribucion_evento,fecha_registro_otorgamiento,USUARIO_rut,EVENTO_evento_id")] OTORGAR_INSIGNIA_P3 oTORGAR_INSIGNIA_P3)
        {
            if (ModelState.IsValid)
            {
                db.OTORGAR_INSIGNIA_P3.Add(oTORGAR_INSIGNIA_P3);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.EVENTO_evento_id = new SelectList(db.EVENTO, "evento_id", "nombre", oTORGAR_INSIGNIA_P3.EVENTO_evento_id);
            ViewBag.USUARIO_rut = new SelectList(db.USUARIO, "rut", "nombre", oTORGAR_INSIGNIA_P3.USUARIO_rut);
            return View(oTORGAR_INSIGNIA_P3);
        }

        // GET: OtorgarInsigniaPerfil3/Edit/5
        public async Task<ActionResult> Edit(string id, int? EVENTO_evento_id)
        {
            if (string.IsNullOrEmpty(id) || EVENTO_evento_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            OTORGAR_INSIGNIA_P3 oTORGAR_INSIGNIA_P3 = await db.OTORGAR_INSIGNIA_P3.FirstOrDefaultAsync(a =>
              a.USUARIO_rut == id && a.EVENTO_evento_id == EVENTO_evento_id);

            if (oTORGAR_INSIGNIA_P3 == null)
            {
                return HttpNotFound();
            }
            ViewBag.EVENTO_evento_id = new SelectList(db.EVENTO, "evento_id", "nombre", oTORGAR_INSIGNIA_P3.EVENTO_evento_id);
            ViewBag.USUARIO_rut = new SelectList(db.USUARIO, "rut", "nombre", oTORGAR_INSIGNIA_P3.USUARIO_rut);
            return View(oTORGAR_INSIGNIA_P3);
        }

        // POST: OtorgarInsigniaPerfil3/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "registro_insignia_evento,contribucion_evento,fecha_registro_otorgamiento,USUARIO_rut,EVENTO_evento_id")] OTORGAR_INSIGNIA_P3 oTORGAR_INSIGNIA_P3)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oTORGAR_INSIGNIA_P3).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.EVENTO_evento_id = new SelectList(db.EVENTO, "evento_id", "nombre", oTORGAR_INSIGNIA_P3.EVENTO_evento_id);
            ViewBag.USUARIO_rut = new SelectList(db.USUARIO, "rut", "nombre", oTORGAR_INSIGNIA_P3.USUARIO_rut);
            return View(oTORGAR_INSIGNIA_P3);
        }

        // GET: OtorgarInsigniaPerfil3/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OTORGAR_INSIGNIA_P3 oTORGAR_INSIGNIA_P3 = await db.OTORGAR_INSIGNIA_P3.FindAsync(id);
            if (oTORGAR_INSIGNIA_P3 == null)
            {
                return HttpNotFound();
            }
            return View(oTORGAR_INSIGNIA_P3);
        }

        // POST: OtorgarInsigniaPerfil3/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            OTORGAR_INSIGNIA_P3 oTORGAR_INSIGNIA_P3 = await db.OTORGAR_INSIGNIA_P3.FindAsync(id);
            db.OTORGAR_INSIGNIA_P3.Remove(oTORGAR_INSIGNIA_P3);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public ActionResult Crear()
        {

            // Filtrar usuarios por tipo_perfil_id = 2
            var usuariosFiltrados = db.USUARIO.Where(u => u.TIPO_PERFIL_tipo_perfil_id == 3).ToList();

            ViewBag.Eventos = new SelectList(db.EVENTO.ToList(), "evento_id", "nombre");
            ViewBag.Usuarios = new SelectList(usuariosFiltrados, "rut", "rut");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GuardarRegistros(FormCollection form)
        {
            if (ModelState.IsValid)
            {

                int eventoID = int.Parse(form["eventoID"]);


                for (int i = 0; i < form.Count / 5; i++)
                {
                    var nuevoItem = new OTORGAR_INSIGNIA_P3
                    {
                        registro_insignia_evento = Convert.ToBoolean(form[$"otorgar_insignia_p3[{i}].registro_insignia_evento"]),
                        contribucion_evento = form[$"otorgar_insignia_p3[{i}].contribucion_evento"],
                        fecha_registro_otorgamiento = DateTime.Parse(form[$"otorgar_insignia_p3[{i}].fecha_registro_otorgamiento"]),
                        USUARIO_rut = form[$"otorgar_insignia_p3[{i}].USUARIO_rut"],
                        EVENTO_evento_id = eventoID
                    };

                    db.OTORGAR_INSIGNIA_P3.Add(nuevoItem);
                }

                db.SaveChanges();
                return RedirectToAction("EventoOtorgarInsigniaLista");
            }

            var usuariosFiltrados = db.USUARIO.Where(u => u.TIPO_PERFIL_tipo_perfil_id == 3).ToList();
            // En caso de errores, regresar a la vista
            ViewBag.Eventos = new SelectList(db.EVENTO.ToList(), "evento_id", "nombre");
            ViewBag.Usuarios = new SelectList(usuariosFiltrados, "rut", "rut");
            return View("Crear");
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
