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
    public class AsistenciaController : Controller
    {
        private EmPoderTIC_WEB db = new EmPoderTIC_WEB();

        // GET: Asistencia
        public async Task<ActionResult> Index()
        {
            var aSISTENCIA = db.ASISTENCIA.Include(a => a.EVENTO).Include(a => a.USUARIO);
            return View(await aSISTENCIA.ToListAsync());
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
                return RedirectToAction("Index");
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
                return RedirectToAction("Index");
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
