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
    public class InsigniaController : Controller
    {
        private EmPoderTICConexionFinal db = new EmPoderTICConexionFinal();

        // GET: Insignia
        public async Task<ActionResult> Index()
        {
            var iNSIGNIA = db.INSIGNIA.Include(i => i.AREA).Include(i => i.CERTIFICACION).Include(i => i.COMPETENCIA).Include(i => i.EVENTO).Include(i => i.NIVEL).Include(i => i.TIPO_PERFIL);
            return View(await iNSIGNIA.ToListAsync());
        }

        // GET: Insignia/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INSIGNIA iNSIGNIA = await db.INSIGNIA.FindAsync(id);
            if (iNSIGNIA == null)
            {
                return HttpNotFound();
            }
            return View(iNSIGNIA);
        }

        // GET: Insignia/Create
        public ActionResult Create()
        {
            ViewBag.AREA_area_id = new SelectList(db.AREA, "area_id", "area_conocimiento");
            ViewBag.CERTIFICACION_certificacion_id = new SelectList(db.CERTIFICACION, "certificacion_id", "certificacion_id");
            ViewBag.COMPETENCIA_competencia_id = new SelectList(db.COMPETENCIA, "competencia_id", "competencia_conocimiento");
            ViewBag.EVENTO_evento_id = new SelectList(db.EVENTO, "evento_id", "nombre");
            ViewBag.NIVEL_nivel_id = new SelectList(db.NIVEL, "nivel_id", "categoría_nivel_insignia");
            ViewBag.TIPO_PERFIL_tipo_perfil_id = new SelectList(db.TIPO_PERFIL, "tipo_perfil_id", "nombre_tipo_perfil");
            return View();
        }

        // POST: Insignia/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "insignia_id,nombre,descripción,objetivo,imagen_url,NIVEL_nivel_id,AREA_area_id,COMPETENCIA_competencia_id,CERTIFICACION_certificacion_id,TIPO_PERFIL_tipo_perfil_id,EVENTO_evento_id")] INSIGNIA iNSIGNIA)
        {
            if (ModelState.IsValid)
            {
                db.INSIGNIA.Add(iNSIGNIA);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.AREA_area_id = new SelectList(db.AREA, "area_id", "area_conocimiento", iNSIGNIA.AREA_area_id);
            ViewBag.CERTIFICACION_certificacion_id = new SelectList(db.CERTIFICACION, "certificacion_id", "certificacion_id", iNSIGNIA.CERTIFICACION_certificacion_id);
            ViewBag.COMPETENCIA_competencia_id = new SelectList(db.COMPETENCIA, "competencia_id", "competencia_conocimiento", iNSIGNIA.COMPETENCIA_competencia_id);
            ViewBag.EVENTO_evento_id = new SelectList(db.EVENTO, "evento_id", "nombre", iNSIGNIA.EVENTO_evento_id);
            ViewBag.NIVEL_nivel_id = new SelectList(db.NIVEL, "nivel_id", "categoría_nivel_insignia", iNSIGNIA.NIVEL_nivel_id);
            ViewBag.TIPO_PERFIL_tipo_perfil_id = new SelectList(db.TIPO_PERFIL, "tipo_perfil_id", "nombre_tipo_perfil", iNSIGNIA.TIPO_PERFIL_tipo_perfil_id);
            return View(iNSIGNIA);
        }

        // GET: Insignia/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INSIGNIA iNSIGNIA = await db.INSIGNIA.FindAsync(id);
            if (iNSIGNIA == null)
            {
                return HttpNotFound();
            }
            ViewBag.AREA_area_id = new SelectList(db.AREA, "area_id", "area_conocimiento", iNSIGNIA.AREA_area_id);
            ViewBag.CERTIFICACION_certificacion_id = new SelectList(db.CERTIFICACION, "certificacion_id", "certificacion_id", iNSIGNIA.CERTIFICACION_certificacion_id);
            ViewBag.COMPETENCIA_competencia_id = new SelectList(db.COMPETENCIA, "competencia_id", "competencia_conocimiento", iNSIGNIA.COMPETENCIA_competencia_id);
            ViewBag.EVENTO_evento_id = new SelectList(db.EVENTO, "evento_id", "nombre", iNSIGNIA.EVENTO_evento_id);
            ViewBag.NIVEL_nivel_id = new SelectList(db.NIVEL, "nivel_id", "categoría_nivel_insignia", iNSIGNIA.NIVEL_nivel_id);
            ViewBag.TIPO_PERFIL_tipo_perfil_id = new SelectList(db.TIPO_PERFIL, "tipo_perfil_id", "nombre_tipo_perfil", iNSIGNIA.TIPO_PERFIL_tipo_perfil_id);
            return View(iNSIGNIA);
        }

        // POST: Insignia/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "insignia_id,nombre,descripción,objetivo,imagen_url,NIVEL_nivel_id,AREA_area_id,COMPETENCIA_competencia_id,CERTIFICACION_certificacion_id,TIPO_PERFIL_tipo_perfil_id,EVENTO_evento_id")] INSIGNIA iNSIGNIA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iNSIGNIA).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AREA_area_id = new SelectList(db.AREA, "area_id", "area_conocimiento", iNSIGNIA.AREA_area_id);
            ViewBag.CERTIFICACION_certificacion_id = new SelectList(db.CERTIFICACION, "certificacion_id", "certificacion_id", iNSIGNIA.CERTIFICACION_certificacion_id);
            ViewBag.COMPETENCIA_competencia_id = new SelectList(db.COMPETENCIA, "competencia_id", "competencia_conocimiento", iNSIGNIA.COMPETENCIA_competencia_id);
            ViewBag.EVENTO_evento_id = new SelectList(db.EVENTO, "evento_id", "nombre", iNSIGNIA.EVENTO_evento_id);
            ViewBag.NIVEL_nivel_id = new SelectList(db.NIVEL, "nivel_id", "categoría_nivel_insignia", iNSIGNIA.NIVEL_nivel_id);
            ViewBag.TIPO_PERFIL_tipo_perfil_id = new SelectList(db.TIPO_PERFIL, "tipo_perfil_id", "nombre_tipo_perfil", iNSIGNIA.TIPO_PERFIL_tipo_perfil_id);
            return View(iNSIGNIA);
        }

        // GET: Insignia/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INSIGNIA iNSIGNIA = await db.INSIGNIA.FindAsync(id);
            if (iNSIGNIA == null)
            {
                return HttpNotFound();
            }
            return View(iNSIGNIA);
        }

        // POST: Insignia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            INSIGNIA iNSIGNIA = await db.INSIGNIA.FindAsync(id);
            db.INSIGNIA.Remove(iNSIGNIA);
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
