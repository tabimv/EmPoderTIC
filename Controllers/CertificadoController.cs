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
    public class CertificadoController : Controller
    {
        private EmPoderTIC_OFICIALEntities db = new EmPoderTIC_OFICIALEntities();

        // GET: Certificado
        public async Task<ActionResult> Index()
        {
            var cERTIFICADO = db.CERTIFICADO.Include(c => c.AREA).Include(c => c.CERTIFICACION).Include(c => c.INSIGNIA);
            return View(await cERTIFICADO.ToListAsync());
        }

        // GET: Certificado/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CERTIFICADO cERTIFICADO = await db.CERTIFICADO.FindAsync(id);
            if (cERTIFICADO == null)
            {
                return HttpNotFound();
            }
            return View(cERTIFICADO);
        }

        // GET: Certificado/Create
        public ActionResult Create()
        {
            ViewBag.AREA_area_id = new SelectList(db.AREA, "area_id", "area_conocimiento");
            ViewBag.CERTIFICACION_certificacion_id = new SelectList(db.CERTIFICACION, "certificacion_id", "certificacion_id");
            ViewBag.INSIGNIA_insignia_id = new SelectList(db.INSIGNIA, "insignia_id", "nombre");
            return View();
        }

        // POST: Certificado/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "certificado_id,nombre,descripcion,CERTIFICACION_certificacion_id,INSIGNIA_insignia_id,AREA_area_id")] CERTIFICADO cERTIFICADO)
        {
            if (ModelState.IsValid)
            {
                db.CERTIFICADO.Add(cERTIFICADO);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.AREA_area_id = new SelectList(db.AREA, "area_id", "area_conocimiento", cERTIFICADO.AREA_area_id);
            ViewBag.CERTIFICACION_certificacion_id = new SelectList(db.CERTIFICACION, "certificacion_id", "certificacion_id", cERTIFICADO.CERTIFICACION_certificacion_id);
            ViewBag.INSIGNIA_insignia_id = new SelectList(db.INSIGNIA, "insignia_id", "nombre", cERTIFICADO.INSIGNIA_insignia_id);
            return View(cERTIFICADO);
        }

        // GET: Certificado/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CERTIFICADO cERTIFICADO = await db.CERTIFICADO.FindAsync(id);
            if (cERTIFICADO == null)
            {
                return HttpNotFound();
            }
            ViewBag.AREA_area_id = new SelectList(db.AREA, "area_id", "area_conocimiento", cERTIFICADO.AREA_area_id);
            ViewBag.CERTIFICACION_certificacion_id = new SelectList(db.CERTIFICACION, "certificacion_id", "certificacion_id", cERTIFICADO.CERTIFICACION_certificacion_id);
            ViewBag.INSIGNIA_insignia_id = new SelectList(db.INSIGNIA, "insignia_id", "nombre", cERTIFICADO.INSIGNIA_insignia_id);
            return View(cERTIFICADO);
        }

        // POST: Certificado/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "certificado_id,nombre,descripcion,CERTIFICACION_certificacion_id,INSIGNIA_insignia_id,AREA_area_id")] CERTIFICADO cERTIFICADO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cERTIFICADO).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AREA_area_id = new SelectList(db.AREA, "area_id", "area_conocimiento", cERTIFICADO.AREA_area_id);
            ViewBag.CERTIFICACION_certificacion_id = new SelectList(db.CERTIFICACION, "certificacion_id", "certificacion_id", cERTIFICADO.CERTIFICACION_certificacion_id);
            ViewBag.INSIGNIA_insignia_id = new SelectList(db.INSIGNIA, "insignia_id", "nombre", cERTIFICADO.INSIGNIA_insignia_id);
            return View(cERTIFICADO);
        }

        // GET: Certificado/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CERTIFICADO cERTIFICADO = await db.CERTIFICADO.FindAsync(id);
            if (cERTIFICADO == null)
            {
                return HttpNotFound();
            }
            return View(cERTIFICADO);
        }

        // POST: Certificado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CERTIFICADO cERTIFICADO = await db.CERTIFICADO.FindAsync(id);
            db.CERTIFICADO.Remove(cERTIFICADO);
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
