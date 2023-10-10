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
    public class NivelController : Controller
    {
        private EmPoderTICConexionFast db = new EmPoderTICConexionFast();

        // GET: Nivel
        public async Task<ActionResult> Index()
        {
            return View(await db.NIVEL.ToListAsync());
        }

        // GET: Nivel/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NIVEL nIVEL = await db.NIVEL.FindAsync(id);
            if (nIVEL == null)
            {
                return HttpNotFound();
            }
            return View(nIVEL);
        }

        // GET: Nivel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Nivel/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "nivel_id,categoría_nivel_insignia")] NIVEL nIVEL)
        {
            if (ModelState.IsValid)
            {
                db.NIVEL.Add(nIVEL);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(nIVEL);
        }

        // GET: Nivel/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NIVEL nIVEL = await db.NIVEL.FindAsync(id);
            if (nIVEL == null)
            {
                return HttpNotFound();
            }
            return View(nIVEL);
        }

        // POST: Nivel/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "nivel_id,categoría_nivel_insignia")] NIVEL nIVEL)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nIVEL).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(nIVEL);
        }

        // GET: Nivel/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NIVEL nIVEL = await db.NIVEL.FindAsync(id);
            if (nIVEL == null)
            {
                return HttpNotFound();
            }
            return View(nIVEL);
        }

        // POST: Nivel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            NIVEL nIVEL = await db.NIVEL.FindAsync(id);
            db.NIVEL.Remove(nIVEL);
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
