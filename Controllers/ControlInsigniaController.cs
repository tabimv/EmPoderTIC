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
    public class ControlInsigniaController : Controller
    {
        private EmPoderTIC_Conexion_Oficial_WEB db = new EmPoderTIC_Conexion_Oficial_WEB();

        // GET: ControlInsignia
        public async Task<ActionResult> Index()
        {
            var cONTROL_INSIGNIA = db.CONTROL_INSIGNIA.Include(c => c.INSIGNIA).Include(c => c.USUARIO);
            return View(await cONTROL_INSIGNIA.ToListAsync());
        }

        // GET: ControlInsignia/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONTROL_INSIGNIA cONTROL_INSIGNIA = await db.CONTROL_INSIGNIA.FindAsync(id);
            if (cONTROL_INSIGNIA == null)
            {
                return HttpNotFound();
            }
            return View(cONTROL_INSIGNIA);
        }

        // GET: ControlInsignia/Create
        public ActionResult Create()
        {
            ViewBag.INSIGNIA_insignia_id = new SelectList(db.INSIGNIA, "insignia_id", "nombre");
            ViewBag.USUARIO_rut = new SelectList(db.USUARIO, "rut", "nombre");
            return View();
        }

        // POST: ControlInsignia/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "insignia_bloqueada,USUARIO_rut,fecha_otorgamiento,INSIGNIA_insignia_id")] CONTROL_INSIGNIA cONTROL_INSIGNIA)
        {
            if (ModelState.IsValid)
            {
                db.CONTROL_INSIGNIA.Add(cONTROL_INSIGNIA);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.INSIGNIA_insignia_id = new SelectList(db.INSIGNIA, "insignia_id", "nombre", cONTROL_INSIGNIA.INSIGNIA_insignia_id);
            ViewBag.USUARIO_rut = new SelectList(db.USUARIO, "rut", "nombre", cONTROL_INSIGNIA.USUARIO_rut);
            return View(cONTROL_INSIGNIA);
        }

        // GET: ControlInsignia/Edit/5
        public async Task<ActionResult> Edit(string id, int? INSIGNIA_insignia_id)
        {
            if (string.IsNullOrEmpty(id) || INSIGNIA_insignia_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONTROL_INSIGNIA cONTROL_INSIGNIA = await db.CONTROL_INSIGNIA.FirstOrDefaultAsync(ci => ci.USUARIO_rut == id && ci.INSIGNIA_insignia_id == INSIGNIA_insignia_id);
            if (cONTROL_INSIGNIA == null)
            {
                return HttpNotFound();
            }
            ViewBag.INSIGNIA_insignia_id = new SelectList(db.INSIGNIA, "insignia_id", "nombre", cONTROL_INSIGNIA.INSIGNIA_insignia_id);
            ViewBag.USUARIO_rut = new SelectList(db.USUARIO, "rut", "nombre", cONTROL_INSIGNIA.USUARIO_rut);
            return View(cONTROL_INSIGNIA);
        }


        // POST: ControlInsignia/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "insignia_bloqueada,USUARIO_rut,fecha_otorgamiento,INSIGNIA_insignia_id")] CONTROL_INSIGNIA cONTROL_INSIGNIA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cONTROL_INSIGNIA).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.INSIGNIA_insignia_id = new SelectList(db.INSIGNIA, "insignia_id", "nombre", cONTROL_INSIGNIA.INSIGNIA_insignia_id);
            ViewBag.USUARIO_rut = new SelectList(db.USUARIO, "rut", "nombre", cONTROL_INSIGNIA.USUARIO_rut);
            return View(cONTROL_INSIGNIA);
        }

        // GET: ControlInsignia/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONTROL_INSIGNIA cONTROL_INSIGNIA = await db.CONTROL_INSIGNIA.FindAsync(id);
            if (cONTROL_INSIGNIA == null)
            {
                return HttpNotFound();
            }
            return View(cONTROL_INSIGNIA);
        }

        // POST: ControlInsignia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            CONTROL_INSIGNIA cONTROL_INSIGNIA = await db.CONTROL_INSIGNIA.FindAsync(id);
            db.CONTROL_INSIGNIA.Remove(cONTROL_INSIGNIA);
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
