using EmPoderTIC.Models;
using EmPoderTIC.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;

namespace EmPoderTIC.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        private EmPoderTIC_OFICIAL db = new EmPoderTIC_OFICIAL();

        [HttpGet]
        [AllowAnonymous]
        [OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        public ActionResult Login()
        {
            // Configura los encabezados para evitar el almacenamiento en caché
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {

            var user = db.USUARIO.FirstOrDefault(u => u.correo_electronico == model.correo);

            if (user == null)
            {
                ModelState.AddModelError("correo", "El correo es incorrecto.");
            }
            else if (ModelState.IsValid) // Solo si el correo es válido, verifica la contraseña
            {
                if (user.contraseña != model.password)
                {
                    ModelState.AddModelError("password", "La contraseña es incorrecta.");
                }

                if (ModelState.IsValid) // Verifica nuevamente después de agregar los errores
                {
                    // Autenticar al usuario
                    FormsAuthentication.SetAuthCookie(model.correo, false);

                    // Almacena el objeto de usuario completo en sesión
                    Session["UsuarioAutenticado"] = user;

                    // Obtener el nombre del tipo de perfil del usuario desde la base de datos
                    var tipoPerfil = db.TIPO_PERFIL.FirstOrDefault(tp => tp.tipo_perfil_id == user.TIPO_PERFIL_tipo_perfil_id)?.nombre_tipo_perfil;

                    // Almacenar el tipo de perfil en una variable de sesión
                    Session["TipoPerfil"] = user.TIPO_PERFIL.nombre_tipo_perfil;

                    if (Session["TipoPerfil"].Equals("Estudiante y/o Titulado"))
                    {
                        return RedirectToAction("Perfil", "VistaPerfil1");
                    }
                    else if (Session["TipoPerfil"].Equals("Docente"))
                    {
                        return RedirectToAction("Perfil", "VistaPerfil2");
                    }
                    else if (Session["TipoPerfil"].Equals("Colaborador administrativo"))
                    {
                        return RedirectToAction("Perfil", "VistaPerfil3");
                    }
                    else if (Session["TipoPerfil"].Equals("Administrador"))
                    {
                        return RedirectToAction("Index", "Usuario");
                    }
                }
            }

            return View(model);
        }


        public ActionResult Logout()
        {
            // Cerrar la sesión actual
            FormsAuthentication.SignOut();

            // Limpiar la variable de sesión relacionada con el tipo de perfil
            Session.Remove("TipoPerfil");

            // Eliminar completamente la sesión
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
          

            // Invalidar la caché del navegador para evitar el acceso a la sesión anterior
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Cookies.Clear();

            // Agregar una cookie expirada para asegurar el cierre de sesión en Chrome
            HttpCookie cookie = new HttpCookie("ASP.NET_SessionId", "");
            cookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie);

            // Redirigir al inicio de sesión
            return RedirectToAction("Login", "Account");
        }



    }
}