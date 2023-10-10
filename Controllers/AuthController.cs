//using EmPoderTIC.Helpers;
//using EmPoderTIC.Models;
//using EmPoderTIC.Models.ViewModels;
//using Microsoft.AspNet.Identity;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using System.Web;
//using System.Web.Mvc;
//using System.Web.UI.WebControls;
//using static System.Collections.Specialized.BitVector32;

//namespace EmPoderTIC.Controllers
//{
//    public class AuthController : Controller
//    {
//        // GET: Auth
//        private EmPoderTICConexionFast db = new EmPoderTICConexionFast();

//        [HttpGet]
//        public ActionResult Login()
//        {
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Login(LoginViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var user = await db.USUARIO.FirstOrDefaultAsync(x => x.correo_electronico == model.correo);
//                if (user == null)
//                {
//                    TempData["ErrorMesagge"] = "Usuario no encontrado ";
//                    return RedirectToAction("Login");
//                }

//                // Aquí puedes realizar la autenticación del usuario si las credenciales son válidas
//                // Puedes usar Identity o cualquier otro sistema de autenticación que estés utilizando
//                // Autenticar al usuario antes de redirigirlo
//                await InitOwin(user);

//                // Luego, redirige al usuario a la página de Insignias (Index)
//                return RedirectToAction("Index", "Insignia");
//            }
//            TempData["SucessMessage"] = "Conectado correctamente";
//            return View();
//        }


//        private async Task InitOwin(USUARIO user)
//        {
//            var claims = new[]
//            {
//                new Claim(ClaimTypes.NameIdentifier, user.rut.ToString()),
//                new Claim(ClaimTypes.Email, user.correo_electronico.ToString()),
//            };

//            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
//            var role = await db.TIPO_PERFIL.FindAsync(user.TIPO_PERFIL_tipo_perfil_id);
//            if (role != null)
//                identity.AddClaim(new Claim(ClaimTypes.Role, role.nombre_tipo_perfil));

//            var context = Request.GetOwinContext();
//            var authManager = context.Authentication;

//            authManager.SignIn(identity);
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//                db.Dispose();
//            base.Dispose(disposing);
//        }
//    }
//}