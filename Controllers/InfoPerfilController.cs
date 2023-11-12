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
    public class InfoPerfilController : Controller
    {
        private EmPoderTIC_Conexion_Oficial_WEB db = new EmPoderTIC_Conexion_Oficial_WEB();

        // GET: InfoPerfil
        public async Task<ActionResult> Index()
        {
            var iNFO_PERFIL = db.INFO_PERFIL.Include(i => i.USUARIO);
            return View(await iNFO_PERFIL.ToListAsync());
        }

        // GET: InfoPerfil/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INFO_PERFIL iNFO_PERFIL = await db.INFO_PERFIL.FindAsync(id);
            if (iNFO_PERFIL == null)
            {
                return HttpNotFound();
            }
            return View(iNFO_PERFIL);
        }

        // GET: InfoPerfil/Create
        public ActionResult Create()
        {
            ViewBag.USUARIO_rut = new SelectList(db.USUARIO, "rut", "nombre");
            return View();
        }

        // POST: InfoPerfil/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "info_perfil_id,escuela,carrera,sede,USUARIO_rut")] INFO_PERFIL iNFO_PERFIL)
        {
            if (ModelState.IsValid)
            {
                db.INFO_PERFIL.Add(iNFO_PERFIL);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.USUARIO_rut = new SelectList(db.USUARIO, "rut", "nombre", iNFO_PERFIL.USUARIO_rut);
            return View(iNFO_PERFIL);
        }

        // GET: InfoPerfil/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INFO_PERFIL iNFO_PERFIL = await db.INFO_PERFIL.FindAsync(id);
            if (iNFO_PERFIL == null)
            {
                return HttpNotFound();
            }
            ViewBag.USUARIO_rut = new SelectList(db.USUARIO, "rut", "nombre", iNFO_PERFIL.USUARIO_rut);
            return View(iNFO_PERFIL);
        }

        // POST: InfoPerfil/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "info_perfil_id,escuela,carrera,sede,USUARIO_rut")] INFO_PERFIL iNFO_PERFIL)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iNFO_PERFIL).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.USUARIO_rut = new SelectList(db.USUARIO, "rut", "nombre", iNFO_PERFIL.USUARIO_rut);
            return View(iNFO_PERFIL);
        }

        // GET: InfoPerfil/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INFO_PERFIL iNFO_PERFIL = await db.INFO_PERFIL.FindAsync(id);
            if (iNFO_PERFIL == null)
            {
                return HttpNotFound();
            }
            return View(iNFO_PERFIL);
        }

        // POST: InfoPerfil/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            INFO_PERFIL iNFO_PERFIL = await db.INFO_PERFIL.FindAsync(id);
            db.INFO_PERFIL.Remove(iNFO_PERFIL);
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
