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
    public class CertificacionController : Controller
    {
        private EmPoderTICConectadoEntities db = new EmPoderTICConectadoEntities();

        // GET: Certificacion
        public async Task<ActionResult> Index()
        {
            var cERTIFICACION = db.CERTIFICACION.Include(c => c.NIVEL);
            return View(await cERTIFICACION.ToListAsync());
        }

        // GET: Certificacion/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CERTIFICACION cERTIFICACION = await db.CERTIFICACION.FindAsync(id);
            if (cERTIFICACION == null)
            {
                return HttpNotFound();
            }
            return View(cERTIFICACION);
        }

        // GET: Certificacion/Create
        public ActionResult Create()
        {
            ViewBag.NIVEL_nivel_id = new SelectList(db.NIVEL, "nivel_id", "categoría_nivel_insignia");
            return View();
        }

        // POST: Certificacion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "certificacion_id,tiene_certificacion,NIVEL_nivel_id")] CERTIFICACION cERTIFICACION)
        {
            if (ModelState.IsValid)
            {
                db.CERTIFICACION.Add(cERTIFICACION);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.NIVEL_nivel_id = new SelectList(db.NIVEL, "nivel_id", "categoría_nivel_insignia", cERTIFICACION.NIVEL_nivel_id);
            return View(cERTIFICACION);
        }

        // GET: Certificacion/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CERTIFICACION cERTIFICACION = await db.CERTIFICACION.FindAsync(id);
            if (cERTIFICACION == null)
            {
                return HttpNotFound();
            }
            ViewBag.NIVEL_nivel_id = new SelectList(db.NIVEL, "nivel_id", "categoría_nivel_insignia", cERTIFICACION.NIVEL_nivel_id);
            return View(cERTIFICACION);
        }

        // POST: Certificacion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "certificacion_id,tiene_certificacion,NIVEL_nivel_id")] CERTIFICACION cERTIFICACION)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cERTIFICACION).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.NIVEL_nivel_id = new SelectList(db.NIVEL, "nivel_id", "categoría_nivel_insignia", cERTIFICACION.NIVEL_nivel_id);
            return View(cERTIFICACION);
        }

        // GET: Certificacion/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CERTIFICACION cERTIFICACION = await db.CERTIFICACION.FindAsync(id);
            if (cERTIFICACION == null)
            {
                return HttpNotFound();
            }
            return View(cERTIFICACION);
        }

        // POST: Certificacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CERTIFICACION cERTIFICACION = await db.CERTIFICACION.FindAsync(id);
            if (cERTIFICACION == null)
            {
                return HttpNotFound();
            }

            // Verificar si existen relaciones con claves foráneas
            if (db.CERTIFICACION.Any(t => t.certificacion_id == id))
            {
                ViewBag.ErrorMessage = "No se puede eliminar este Requerimiento de Certificación debido a  que esta relacionado a otras Entidades.";
                return View("Delete", cERTIFICACION); // Mostrar vista de eliminación con el mensaje de error
            }

            db.CERTIFICACION.Remove(cERTIFICACION);
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
