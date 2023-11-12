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
    public class InscripcionController : Controller
    {
        private EmPoderTIC_Conexion_Oficial db = new EmPoderTIC_Conexion_Oficial();

        // GET: Inscripcion
        public async Task<ActionResult> Index()
        {
            var iNSCRIPCION = db.INSCRIPCION.Include(i => i.EVENTO).Include(i => i.USUARIO);
            return View(await iNSCRIPCION.ToListAsync());
        }

        // GET: Inscripcion/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INSCRIPCION iNSCRIPCION = await db.INSCRIPCION.FindAsync(id);
            if (iNSCRIPCION == null)
            {
                return HttpNotFound();
            }
            return View(iNSCRIPCION);
        }

        // GET: Inscripcion/Create
        public ActionResult Create()
        {
            ViewBag.EVENTO_evento_id = new SelectList(db.EVENTO, "evento_id", "nombre");
            ViewBag.USUARIO_rut = new SelectList(db.USUARIO, "rut", "nombre");
            return View();
        }

        // POST: Inscripcion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "inscripcion_id,USUARIO_rut,EVENTO_evento_id")] INSCRIPCION iNSCRIPCION)
        {
            if (ModelState.IsValid)
            {
                db.INSCRIPCION.Add(iNSCRIPCION);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.EVENTO_evento_id = new SelectList(db.EVENTO, "evento_id", "nombre", iNSCRIPCION.EVENTO_evento_id);
            ViewBag.USUARIO_rut = new SelectList(db.USUARIO, "rut", "nombre", iNSCRIPCION.USUARIO_rut);
            return View(iNSCRIPCION);
        }

        // GET: Inscripcion/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INSCRIPCION iNSCRIPCION = await db.INSCRIPCION.FindAsync(id);
            if (iNSCRIPCION == null)
            {
                return HttpNotFound();
            }
            ViewBag.EVENTO_evento_id = new SelectList(db.EVENTO, "evento_id", "nombre", iNSCRIPCION.EVENTO_evento_id);
            ViewBag.USUARIO_rut = new SelectList(db.USUARIO, "rut", "nombre", iNSCRIPCION.USUARIO_rut);
            return View(iNSCRIPCION);
        }

        // POST: Inscripcion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "inscripcion_id,USUARIO_rut,EVENTO_evento_id")] INSCRIPCION iNSCRIPCION)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iNSCRIPCION).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.EVENTO_evento_id = new SelectList(db.EVENTO, "evento_id", "nombre", iNSCRIPCION.EVENTO_evento_id);
            ViewBag.USUARIO_rut = new SelectList(db.USUARIO, "rut", "nombre", iNSCRIPCION.USUARIO_rut);
            return View(iNSCRIPCION);
        }

        // GET: Inscripcion/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INSCRIPCION iNSCRIPCION = await db.INSCRIPCION.FindAsync(id);
            if (iNSCRIPCION == null)
            {
                return HttpNotFound();
            }
            return View(iNSCRIPCION);
        }

        // POST: Inscripcion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            INSCRIPCION iNSCRIPCION = await db.INSCRIPCION.FindAsync(id);
            db.INSCRIPCION.Remove(iNSCRIPCION);
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
