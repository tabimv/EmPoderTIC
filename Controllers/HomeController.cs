using EmPoderTIC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmPoderTIC.Controllers
{
    public class HomeController : Controller
    {

        private EmPoderTIC_Conexion_Oficial db = new EmPoderTIC_Conexion_Oficial();

        // GET:

        public ActionResult Contacto()
        {
            return View();
        }
        public ActionResult InicioGeneral()
        {
            return View();
        }

        public ActionResult InicioP1()
        {
            return View();
        }

        public ActionResult InicioP2()
        {
            return View();
        }

        public ActionResult InicioP3()
        {
            return View();
        }

        public ActionResult ProgramaMasMujeresEnLasTics()
        {
            return View();
        }

        public ActionResult ProgramaMasMujeresEnLasTicsP1()
        {
            return View();
        }

        public ActionResult ProgramaMasMujeresEnLasTicsP2()
        {
            return View();
        }

        public ActionResult ProgramaMasMujeresEnLasTicsP3()
        {
            return View();
        }

    }
}