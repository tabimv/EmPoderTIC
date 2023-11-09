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
    public class EventoController : Controller
    {
        private EmPoderTICConexionFinal db = new EmPoderTICConexionFinal();

        // GET: Evento
        public async Task<ActionResult> Index()
        {
            var eVENTO = db.EVENTO.Include(e => e.AREA).Include(e => e.COMPETENCIA);
            return View(await eVENTO.ToListAsync());
        }

        // GET: Evento/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EVENTO eVENTO = await db.EVENTO.FindAsync(id);
            if (eVENTO == null)
            {
                return HttpNotFound();
            }
            return View(eVENTO);
        }

        // GET: Evento/Create
        public ActionResult Create()
        {
            ViewBag.AREA_area_id = new SelectList(db.AREA, "area_id", "area_conocimiento");
            ViewBag.COMPETENCIA_competencia_id = new SelectList(db.COMPETENCIA, "competencia_id", "competencia_conocimiento");
            return View();
        }

        // POST: Evento/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "evento_id,nombre,descripcion,tipo_evento,fecha_evento,hora_evento,lugar_evento,AREA_area_id,COMPETENCIA_competencia_id")] EVENTO eVENTO)
        {
            if (ModelState.IsValid)
            {
                db.EVENTO.Add(eVENTO);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.AREA_area_id = new SelectList(db.AREA, "area_id", "area_conocimiento", eVENTO.AREA_area_id);
            ViewBag.COMPETENCIA_competencia_id = new SelectList(db.COMPETENCIA, "competencia_id", "competencia_conocimiento", eVENTO.COMPETENCIA_competencia_id);
            return View(eVENTO);
        }

        // GET: Evento/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EVENTO eVENTO = await db.EVENTO.FindAsync(id);
            if (eVENTO == null)
            {
                return HttpNotFound();
            }
            ViewBag.AREA_area_id = new SelectList(db.AREA, "area_id", "area_conocimiento", eVENTO.AREA_area_id);
            ViewBag.COMPETENCIA_competencia_id = new SelectList(db.COMPETENCIA, "competencia_id", "competencia_conocimiento", eVENTO.COMPETENCIA_competencia_id);
            return View(eVENTO);
        }

        // POST: Evento/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "evento_id,nombre,descripcion,tipo_evento,fecha_evento,hora_evento,lugar_evento,AREA_area_id,COMPETENCIA_competencia_id")] EVENTO eVENTO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eVENTO).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AREA_area_id = new SelectList(db.AREA, "area_id", "area_conocimiento", eVENTO.AREA_area_id);
            ViewBag.COMPETENCIA_competencia_id = new SelectList(db.COMPETENCIA, "competencia_id", "competencia_conocimiento", eVENTO.COMPETENCIA_competencia_id);
            return View(eVENTO);
        }

        // GET: Evento/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EVENTO eVENTO = await db.EVENTO.FindAsync(id);
            if (eVENTO == null)
            {
                return HttpNotFound();
            }
            return View(eVENTO);
        }

        // POST: Evento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            EVENTO eVENTO = await db.EVENTO.FindAsync(id);
            db.EVENTO.Remove(eVENTO);
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
