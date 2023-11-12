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
    public class CompetenciaController : Controller
    {
        private EmPoderTIC_Conexion_Oficial_WEB db = new EmPoderTIC_Conexion_Oficial_WEB();

        // GET: Competencia
        public async Task<ActionResult> Index()
        {
            var cOMPETENCIA = db.COMPETENCIA.Include(c => c.AREA).Include(c => c.NIVEL);
            return View(await cOMPETENCIA.ToListAsync());
        }

        // GET: Competencia/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMPETENCIA cOMPETENCIA = await db.COMPETENCIA.FindAsync(id);
            if (cOMPETENCIA == null)
            {
                return HttpNotFound();
            }
            return View(cOMPETENCIA);
        }

        // GET: Competencia/Create
        public ActionResult Create()
        {
            ViewBag.AREA_area_id = new SelectList(db.AREA, "area_id", "area_conocimiento");
            ViewBag.NIVEL_nivel_id = new SelectList(db.NIVEL, "nivel_id", "categoría_nivel_insignia");
            return View();
        }

        // POST: Competencia/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "competencia_id,competencia_conocimiento,AREA_area_id,NIVEL_nivel_id")] COMPETENCIA cOMPETENCIA)
        {
            if (ModelState.IsValid)
            {
                db.COMPETENCIA.Add(cOMPETENCIA);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.AREA_area_id = new SelectList(db.AREA, "area_id", "area_conocimiento", cOMPETENCIA.AREA_area_id);
            ViewBag.NIVEL_nivel_id = new SelectList(db.NIVEL, "nivel_id", "categoría_nivel_insignia", cOMPETENCIA.NIVEL_nivel_id);
            return View(cOMPETENCIA);
        }

        // GET: Competencia/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMPETENCIA cOMPETENCIA = await db.COMPETENCIA.FindAsync(id);
            if (cOMPETENCIA == null)
            {
                return HttpNotFound();
            }
            ViewBag.AREA_area_id = new SelectList(db.AREA, "area_id", "area_conocimiento", cOMPETENCIA.AREA_area_id);
            ViewBag.NIVEL_nivel_id = new SelectList(db.NIVEL, "nivel_id", "categoría_nivel_insignia", cOMPETENCIA.NIVEL_nivel_id);
            return View(cOMPETENCIA);
        }

        // POST: Competencia/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "competencia_id,competencia_conocimiento,AREA_area_id,NIVEL_nivel_id")] COMPETENCIA cOMPETENCIA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cOMPETENCIA).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AREA_area_id = new SelectList(db.AREA, "area_id", "area_conocimiento", cOMPETENCIA.AREA_area_id);
            ViewBag.NIVEL_nivel_id = new SelectList(db.NIVEL, "nivel_id", "categoría_nivel_insignia", cOMPETENCIA.NIVEL_nivel_id);
            return View(cOMPETENCIA);
        }

        // GET: Competencia/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMPETENCIA cOMPETENCIA = await db.COMPETENCIA.FindAsync(id);
            if (cOMPETENCIA == null)
            {
                return HttpNotFound();
            }
            return View(cOMPETENCIA);
        }

        // POST: Competencia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            COMPETENCIA cOMPETENCIA = await db.COMPETENCIA.FindAsync(id);

            if (cOMPETENCIA == null)
            {
                return HttpNotFound();
            }

            // Verificar si existen relaciones con claves foráneas
            if (db.COMPETENCIA.Any(t => t.competencia_id == id))
            {
                ViewBag.ErrorMessage = "No se puede eliminar esta Competencia debido a  que esta relacionado a otras Entidades.";
                return View("Delete", cOMPETENCIA); // Mostrar vista de eliminación con el mensaje de error
            }

            db.COMPETENCIA.Remove(cOMPETENCIA);
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
