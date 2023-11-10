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

        private EmPoderTICConexionFinal db = new EmPoderTICConexionFinal();

        // GET: 
        public ActionResult InicioGeneral()
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