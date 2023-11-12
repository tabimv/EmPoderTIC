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
        private EmPoderTIC_Conexion_Oficial_WEB db = new EmPoderTIC_Conexion_Oficial_WEB();

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
        public async Task<ActionResult> Create([Bind(Include = "rut,nombre,apellido_paterno,apellido_materno,correo_electronico,contraseña,token,token_reset_contraseña,fecha_solicitud_reset_contraseña,estado_confirmacion,TIPO_PERFIL_tipo_perfil_id")] USUARIO uSUARIO)
        {
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
        public async Task<ActionResult> Edit([Bind(Include = "rut,nombre,apellido_paterno,apellido_materno,correo_electronico,contraseña, estado_confirmacion,TIPO_PERFIL_tipo_perfil_id")] USUARIO uSUARIO)
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
            USUARIO uSUARIO = await db.USUARIO.FindAsync(id);
            if (uSUARIO == null)
            {
                return HttpNotFound();
            }
            return View(uSUARIO);
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            USUARIO uSUARIO = await db.USUARIO.FindAsync(id);
            db.USUARIO.Remove(uSUARIO);
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


        [HttpPost]
        public async Task<ActionResult> ToggleEstado(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Obtener el usuario autenticado desde la sesión
            var usuarioAutenticado = (USUARIO)Session["UsuarioAutenticado"];

            // Obtener el usuario que se va a deshabilitar
            USUARIO uSUARIO = await db.USUARIO.FindAsync(id);

            if (uSUARIO == null)
            {
                return HttpNotFound();
            }

            // Verificar si el usuario autenticado es el mismo que se está intentando deshabilitar
            if (usuarioAutenticado.rut == uSUARIO.rut)
            {
                // El usuario autenticado no puede deshabilitarse a sí mismo
                TempData["ErrorMessage"] = "No puedes deshabilitar tu propia cuenta.";
            }
            else
            {
                // Cambiar el estado
                uSUARIO.activo = !uSUARIO.activo;

                // Guardar los cambios
                db.Entry(uSUARIO).State = EntityState.Modified;
                await db.SaveChangesAsync();

                TempData["SuccessMessage"] = $"El estado del usuario {uSUARIO.rut} ha sido cambiado con éxito.";
            }

            // Redirigir a la vista Index
            return RedirectToAction("Index");
        }


    }
}
