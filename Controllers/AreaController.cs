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
    public class AreaController : Controller
    {
        private EmPoderTICConexionFast db = new EmPoderTICConexionFast();

        // GET: Area
        public async Task<ActionResult> Index()
        {
            return View(await db.AREA.ToListAsync());
        }

        // GET: Area/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AREA aREA = await db.AREA.FindAsync(id);
            if (aREA == null)
            {
                return HttpNotFound();
            }
            return View(aREA);
        }

        // GET: Area/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Area/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "area_id,area_conocimiento")] AREA aREA)
        {
            if (ModelState.IsValid)
            {
                db.AREA.Add(aREA);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(aREA);
        }

        // GET: Area/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AREA aREA = await db.AREA.FindAsync(id);
            if (aREA == null)
            {
                return HttpNotFound();
            }
            return View(aREA);
        }

        // POST: Area/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "area_id,area_conocimiento")] AREA aREA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aREA).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(aREA);
        }

        // GET: Area/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AREA aREA = await db.AREA.FindAsync(id);
            if (aREA == null)
            {
                return HttpNotFound();
            }
            return View(aREA);
        }

        // POST: Area/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            AREA aREA = await db.AREA.FindAsync(id);

            if (aREA == null)
            {
                return HttpNotFound();
            }

            // Verificar si existen relaciones con claves foráneas
            if (db.AREA.Any(t => t.area_id == id))
            {
                ViewBag.ErrorMessage = "No se puede eliminar esta Área debido a  que esta relacionado a otras Entidades.";
                return View("Delete", aREA); // Mostrar vista de eliminación con el mensaje de error
            }

            db.AREA.Remove(aREA);
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
