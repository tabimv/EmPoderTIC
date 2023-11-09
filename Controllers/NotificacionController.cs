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
using System.Data.Entity.Validation;

namespace EmPoderTIC.Controllers
{
    public class NotificacionController : Controller
    {
        private EmPoderTICConexionFinal db = new EmPoderTICConexionFinal();

        public ActionResult SolicitarInsigniaComoLogro(int insignia_id)
        {
            if (Session["UsuarioAutenticado"] != null)
            {
                var usuarioAutenticado = (USUARIO)Session["UsuarioAutenticado"];
                var mensaje = $"ha solicitado la insignia {insignia_id} como certificado de logros para su perfil de LinkedIn.";

                var nOTIFICACION = new NOTIFICACION
                {
                    mensaje = mensaje,
                    fecha = DateTime.Now,
                    solicitud_aprobada = false,
                    USUARIO_rut = usuarioAutenticado.rut,
                    INSIGNIA_insignia_id = insignia_id
                };

                using (var db = new EmPoderTICConexionFinal())
                {
                    db.NOTIFICACION.Add(nOTIFICACION);
                    db.SaveChanges();
                }
                // Redirige al usuario a donde corresponda después de enviar la notificación

                if (usuarioAutenticado.TIPO_PERFIL.nombre_tipo_perfil == "Estudiante y/o Titulado")
                {
                    return RedirectToAction("Perfil", "VistaPerfil1");
                }
                else if (usuarioAutenticado.TIPO_PERFIL.nombre_tipo_perfil == "Docente")
                {
                    return RedirectToAction("Perfil", "VistaPerfil2");
                }
                else if (usuarioAutenticado.TIPO_PERFIL.nombre_tipo_perfil == "Colaborador administrativo")
                {
                    return RedirectToAction("Perfil", "VistaPerfil3");
                }


                return RedirectToAction("Perfil");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }



        public ActionResult SolicitarCertificado(int insignia_id)
        {
            if (Session["UsuarioAutenticado"] != null)
            {
                var usuarioAutenticado = (USUARIO)Session["UsuarioAutenticado"];
                var mensaje = $"ha solicitado el Certificado correspondiente a la Insignia Certificada {insignia_id}.";

                var nOTIFICACION = new NOTIFICACION
                {
                    mensaje = mensaje,
                    fecha = DateTime.Now,
                    solicitud_aprobada = false,
                    USUARIO_rut = usuarioAutenticado.rut,
                    INSIGNIA_insignia_id = insignia_id
                };

                using (var db = new EmPoderTICConexionFinal())
                {
                    db.NOTIFICACION.Add(nOTIFICACION);
                    db.SaveChanges();
                }
                // Redirige al usuario a donde corresponda después de enviar la notificación

                if (usuarioAutenticado.TIPO_PERFIL.nombre_tipo_perfil == "Estudiante y/o Titulado")
                {
                    return RedirectToAction("Perfil", "VistaPerfil1");
                }
                else if (usuarioAutenticado.TIPO_PERFIL.nombre_tipo_perfil == "Docente")
                {
                    return RedirectToAction("Perfil", "VistaPerfil2");
                }
                else if (usuarioAutenticado.TIPO_PERFIL.nombre_tipo_perfil == "Colaborador administrativo")
                {
                    return RedirectToAction("Perfil", "VistaPerfil3");
                }


                return RedirectToAction("Perfil");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }


        // GET: Notificacion
        public async Task<ActionResult> Index()
        {
            var nOTIFICACION = db.NOTIFICACION.Include(n => n.INSIGNIA).Include(n => n.USUARIO);
            return View(await nOTIFICACION.ToListAsync());
        }


        // GET: Notificacion/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NOTIFICACION nOTIFICACION = await db.NOTIFICACION.FindAsync(id);
            if (nOTIFICACION == null)
            {
                return HttpNotFound();
            }
            ViewBag.INSIGNIA_insignia_id = new SelectList(db.INSIGNIA, "insignia_id", "nombre", nOTIFICACION.INSIGNIA_insignia_id);
            ViewBag.USUARIO_rut = new SelectList(db.USUARIO, "rut", "nombre", nOTIFICACION.USUARIO_rut);
            return View(nOTIFICACION);
        }

        // POST: Notificacion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "notificacion_id,mensaje,fecha,solicitud_aprobada,USUARIO_rut,INSIGNIA_insignia_id")] NOTIFICACION nOTIFICACION)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nOTIFICACION).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.INSIGNIA_insignia_id = new SelectList(db.INSIGNIA, "insignia_id", "nombre", nOTIFICACION.INSIGNIA_insignia_id);
            ViewBag.USUARIO_rut = new SelectList(db.USUARIO, "rut", "nombre", nOTIFICACION.USUARIO_rut);
            return View(nOTIFICACION);
        }

        // GET: Notificacion/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NOTIFICACION nOTIFICACION = await db.NOTIFICACION.FindAsync(id);
            if (nOTIFICACION == null)
            {
                return HttpNotFound();
            }
            return View(nOTIFICACION);
        }

        // POST: Notificacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            NOTIFICACION nOTIFICACION = await db.NOTIFICACION.FindAsync(id);
            db.NOTIFICACION.Remove(nOTIFICACION);
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
