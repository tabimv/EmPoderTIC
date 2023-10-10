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
    public class TipoPerfilController : Controller
    {
        private EmPoderTICConexionFast db = new EmPoderTICConexionFast();

        // GET: TipoPerfil
        public async Task<ActionResult> Index()
        {
            return View(await db.TIPO_PERFIL.ToListAsync());
        }

        // GET: TipoPerfil/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPO_PERFIL tIPO_PERFIL = await db.TIPO_PERFIL.FindAsync(id);
            if (tIPO_PERFIL == null)
            {
                return HttpNotFound();
            }
            return View(tIPO_PERFIL);
        }

        // GET: TipoPerfil/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoPerfil/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "tipo_perfil_id,nombre_tipo_perfil,dominio_correo")] TIPO_PERFIL tIPO_PERFIL)
        {
            if (ModelState.IsValid)
            {
                db.TIPO_PERFIL.Add(tIPO_PERFIL);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tIPO_PERFIL);
        }

        // GET: TipoPerfil/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPO_PERFIL tIPO_PERFIL = await db.TIPO_PERFIL.FindAsync(id);
            if (tIPO_PERFIL == null)
            {
                return HttpNotFound();
            }
            return View(tIPO_PERFIL);
        }

        // POST: TipoPerfil/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "tipo_perfil_id,nombre_tipo_perfil,dominio_correo")] TIPO_PERFIL tIPO_PERFIL)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tIPO_PERFIL).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tIPO_PERFIL);
        }

        // GET: TipoPerfil/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPO_PERFIL tIPO_PERFIL = await db.TIPO_PERFIL.FindAsync(id);
            if (tIPO_PERFIL == null)
            {
                return HttpNotFound();
            }
            return View(tIPO_PERFIL);
        }

        // POST: TipoPerfil/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TIPO_PERFIL tIPO_PERFIL = await db.TIPO_PERFIL.FindAsync(id);

            if (tIPO_PERFIL == null)
            {
                return HttpNotFound();
            }

            // Verificar si existen relaciones con claves foráneas
            if (db.TIPO_PERFIL.Any(t => t.tipo_perfil_id == id))
            {
                ViewBag.ErrorMessage = "No se puede eliminar este Tipo de Perfil debido a  que esta relacionado a otras Entidades.";
                return View("Delete", tIPO_PERFIL); // Mostrar vista de eliminación con el mensaje de error
            }

            db.TIPO_PERFIL.Remove(tIPO_PERFIL);
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
