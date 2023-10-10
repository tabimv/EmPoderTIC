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
        private EmPoderTICConexionFast db = new EmPoderTICConexionFast(); // Tu contexto de base de datos

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
            if (ModelState.IsValid)
            {
                var user = db.USUARIO.FirstOrDefault(u => u.correo_electronico == model.correo && u.clave == model.password);

                if (user != null)
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
                        return RedirectToAction("Perfil", "Insignia");
                    }
                    else if (Session["TipoPerfil"].Equals("Administrador"))
                    {
                        return RedirectToAction("Index", "Usuario");
                    }
                    else
                    {
                        ModelState.AddModelError("", "El tipo de perfil no es válido.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Las credenciales son incorrectas.");
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