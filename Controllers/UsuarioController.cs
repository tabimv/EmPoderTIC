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
   
    public class UsuarioController : Controller
    {
        private EmPoderTICConexionFast db = new EmPoderTICConexionFast();

        // GET: Usuario
        public async Task<ActionResult> Index()
        {
            var uSUARIO = db.USUARIO.Include(u => u.TIPO_PERFIL);
            return View(await uSUARIO.ToListAsync());
        }

        // GET: Usuario/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USUARIO uSUARIO = await db.USUARIO.FindAsync(id);
            if (uSUARIO == null)
            {
                return HttpNotFound();
            }
            return View(uSUARIO);
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            ViewBag.TIPO_PERFIL_tipo_perfil_id = new SelectList(db.TIPO_PERFIL, "tipo_perfil_id", "nombre_tipo_perfil");
            return View();
        }

        // POST: Usuario/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "rut,nombre,apellido_paterno,apellido_materno,correo_electronico,clave,TIPO_PERFIL_tipo_perfil_id")] USUARIO uSUARIO)
        {
            // Verificar si ya existe un usuario con el mismo rut
            var existingUserWithRut = await db.USUARIO.FirstOrDefaultAsync(x => x.rut == uSUARIO.rut);

            if (existingUserWithRut != null)
            {
                ModelState.AddModelError("rut", "El rut ya está en uso por otro usuario.");
            }

            // Verificar si ya existe un usuario con el mismo correo electrónico
            var existingUserWithEmail = await db.USUARIO.FirstOrDefaultAsync(x => x.correo_electronico == uSUARIO.correo_electronico);

            if (existingUserWithEmail != null)
            {
                ModelState.AddModelError("correo_electronico", "El correo electrónico ya está en uso por otro usuario.");
            }

            if (ModelState.IsValid)
            {
                db.USUARIO.Add(uSUARIO);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.TIPO_PERFIL_tipo_perfil_id = new SelectList(db.TIPO_PERFIL, "tipo_perfil_id", "nombre_tipo_perfil", uSUARIO.TIPO_PERFIL_tipo_perfil_id);
            return View(uSUARIO);
        }

        // GET: Usuario/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USUARIO uSUARIO = await db.USUARIO.FindAsync(id);
            if (uSUARIO == null)
            {
                return HttpNotFound();
            }
            ViewBag.TIPO_PERFIL_tipo_perfil_id = new SelectList(db.TIPO_PERFIL, "tipo_perfil_id", "nombre_tipo_perfil", uSUARIO.TIPO_PERFIL_tipo_perfil_id);
            return View(uSUARIO);
        }

        // POST: Usuario/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "rut,nombre,apellido_paterno,apellido_materno,correo_electronico,clave,TIPO_PERFIL_tipo_perfil_id")] USUARIO uSUARIO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uSUARIO).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.TIPO_PERFIL_tipo_perfil_id = new SelectList(db.TIPO_PERFIL, "tipo_perfil_id", "nombre_tipo_perfil", uSUARIO.TIPO_PERFIL_tipo_perfil_id);
            return View(uSUARIO);
        }

        // GET: Usuario/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Verificar si el usuario tiene el tipo de perfil adecuado en la variable de sesión
            if (Session["TipoPerfil"] != null && Session["TipoPerfil"].ToString() == "Administrador")
            {
                USUARIO uSUARIO = await db.USUARIO.FindAsync(id);

                if (uSUARIO == null)
                {
                    return HttpNotFound();
                }

                return View(uSUARIO);
            }
            else
            {
                ViewBag.ErrorMessage = "Acceso no autorizado. Debes ser un administrador para realizar esta acción.";
                return View("Error"); // Crear una vista de error que muestre el mensaje de error.
            }
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            // Verificar si el usuario tiene el tipo de perfil adecuado en la variable de sesión
            if (Session["TipoPerfil"] != null && Session["TipoPerfil"].ToString() == "Administrador")
            {
                USUARIO uSUARIO = await db.USUARIO.FindAsync(id);

                if (uSUARIO == null)
                {
                    return HttpNotFound();
                }

                // Verificar si existen relaciones con claves foráneas
                if (db.USUARIO.Any(t => t.rut == id))
                {
                    ViewBag.ErrorMessage = "No se puede eliminar este Usuario debido a que está relacionado con otras Entidades.";
                    return View("Delete", uSUARIO); // Mostrar vista de eliminación con el mensaje de error
                }

                db.USUARIO.Remove(uSUARIO);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = "Acceso no autorizado. Debes ser un administrador para realizar esta acción.";
                return View("Error"); // Crear una vista de error que muestre el mensaje de error.
            }
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
