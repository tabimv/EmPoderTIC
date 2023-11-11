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
    public class UsuarioCertificadoController : Controller
    {
        private EmPoderTIC_WEB db = new EmPoderTIC_WEB();

        // GET: UsuarioCertificado
        public async Task<ActionResult> Index()
        {
            var uSUARIO_CERTIFICADO = db.USUARIO_CERTIFICADO.Include(u => u.CERTIFICADO).Include(u => u.USUARIO);
            return View(await uSUARIO_CERTIFICADO.ToListAsync());
        }

        // GET: UsuarioCertificado/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USUARIO_CERTIFICADO uSUARIO_CERTIFICADO = await db.USUARIO_CERTIFICADO.FindAsync(id);
            if (uSUARIO_CERTIFICADO == null)
            {
                return HttpNotFound();
            }
            return View(uSUARIO_CERTIFICADO);
        }

        // GET: UsuarioCertificado/Create
        public ActionResult Create()
        {
            ViewBag.CERTIFICADO_certificado_id = new SelectList(db.CERTIFICADO, "certificado_id", "nombre");
            ViewBag.USUARIO_rut = new SelectList(db.USUARIO, "rut", "nombre");
            return View();
        }

        // POST: UsuarioCertificado/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "fecha_otorgamiento,certificado_url,CERTIFICADO_certificado_id,USUARIO_rut")] USUARIO_CERTIFICADO uSUARIO_CERTIFICADO)
        {
            if (ModelState.IsValid)
            {
                db.USUARIO_CERTIFICADO.Add(uSUARIO_CERTIFICADO);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CERTIFICADO_certificado_id = new SelectList(db.CERTIFICADO, "certificado_id", "nombre", uSUARIO_CERTIFICADO.CERTIFICADO_certificado_id);
            ViewBag.USUARIO_rut = new SelectList(db.USUARIO, "rut", "nombre", uSUARIO_CERTIFICADO.USUARIO_rut);
            return View(uSUARIO_CERTIFICADO);
        }

        // GET: UsuarioCertificado/Edit/5
        public async Task<ActionResult> Edit(int? certificado_id, string USUARIO_rut)
        {
            if (string.IsNullOrEmpty(USUARIO_rut) || certificado_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USUARIO_CERTIFICADO uSUARIO_CERTIFICADO = await db.USUARIO_CERTIFICADO.FirstOrDefaultAsync(uc => uc.CERTIFICADO_certificado_id == certificado_id &&
            uc.USUARIO_rut == USUARIO_rut);
            if (uSUARIO_CERTIFICADO == null)
            {
                return HttpNotFound();
            }
            ViewBag.CERTIFICADO_certificado_id = new SelectList(db.CERTIFICADO, "certificado_id", "nombre", uSUARIO_CERTIFICADO.CERTIFICADO_certificado_id);
            ViewBag.USUARIO_rut = new SelectList(db.USUARIO, "rut", "nombre", uSUARIO_CERTIFICADO.USUARIO_rut);
            return View(uSUARIO_CERTIFICADO);
        }

        // POST: UsuarioCertificado/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "fecha_otorgamiento,certificado_url,CERTIFICADO_certificado_id,USUARIO_rut")] USUARIO_CERTIFICADO uSUARIO_CERTIFICADO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uSUARIO_CERTIFICADO).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CERTIFICADO_certificado_id = new SelectList(db.CERTIFICADO, "certificado_id", "nombre", uSUARIO_CERTIFICADO.CERTIFICADO_certificado_id);
            ViewBag.USUARIO_rut = new SelectList(db.USUARIO, "rut", "nombre", uSUARIO_CERTIFICADO.USUARIO_rut);
            return View(uSUARIO_CERTIFICADO);
        }

        // GET: UsuarioCertificado/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USUARIO_CERTIFICADO uSUARIO_CERTIFICADO = await db.USUARIO_CERTIFICADO.FindAsync(id);
            if (uSUARIO_CERTIFICADO == null)
            {
                return HttpNotFound();
            }
            return View(uSUARIO_CERTIFICADO);
        }

        // POST: UsuarioCertificado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            USUARIO_CERTIFICADO uSUARIO_CERTIFICADO = await db.USUARIO_CERTIFICADO.FindAsync(id);
            db.USUARIO_CERTIFICADO.Remove(uSUARIO_CERTIFICADO);
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
