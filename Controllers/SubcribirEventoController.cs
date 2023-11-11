using EmPoderTIC.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmPoderTIC.Controllers
{
    public class SubcribirEventoController : Controller
    {
        private EmPoderTICConexionFinal db = new EmPoderTICConexionFinal();

        // GET: SubcribirEvento
        public ActionResult SubscribirEvento()
        {
            // Recupera la lista de eventos desde la base de datos
            List<EVENTO> eventos = db.EVENTO.ToList();
            // Verifica si el usuario está autenticado
            // Verifica si el usuario está autenticado
            if (Session["UsuarioAutenticado"] != null)
            {
                // Recupera la información del usuario autenticado desde la sesión
                var usuarioAutenticado = (USUARIO)Session["UsuarioAutenticado"];

                // Crea una lista para almacenar los IDs de eventos en los que el usuario está inscrito
                List<int> eventosInscritos = db.INSCRIPCION
                    .Where(i => i.USUARIO_rut == usuarioAutenticado.rut)
                    .Select(i => i.EVENTO_evento_id)
                    .ToList();

                // Agrega la lista de eventos inscritos al modelo de vista
                ViewBag.EventosInscritos = eventosInscritos;
            }
            // Pasa la lista de eventos a la vista
            return View(eventos);
        }


        [HttpPost]
        public ActionResult InscribirEvento(int eventoId)
        {
            // Verifica si el usuario está autenticado
            if (Session["UsuarioAutenticado"] != null)
            {
                // Recupera la información del usuario autenticado desde la sesión
                var usuarioAutenticado = (USUARIO)Session["UsuarioAutenticado"];

                // Crea una nueva inscripción
                INSCRIPCION inscripcion = new INSCRIPCION
                {
                    USUARIO_rut = usuarioAutenticado.rut,
                    EVENTO_evento_id = eventoId
                };

                // Agrega la inscripción a la base de datos
                db.INSCRIPCION.Add(inscripcion);
                db.SaveChanges();

            }

            // Redirige a la página de detalles del evento después de la inscripción
            return RedirectToAction("DetallesEvento", new { eventoId = eventoId });
        }


        public ActionResult DetallesEvento(int eventoId)
        {
            // Obtén el evento específico por ID desde la base de datos
            EVENTO evento = db.EVENTO.Find(eventoId);

            // Verifica si el evento existe
            if (evento == null)
            {
                // Puedes redirigir a una página de error o simplemente a la página principal
                return RedirectToAction("SubscribirEvento", "SubcribirEvento");
            }
            

            // Muestra la vista de detalles con el evento
            return View(evento);
        }



    }
}