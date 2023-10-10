using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using EmPoderTIC.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using System.Web.Security;

namespace EmPoderTIC.Controllers
{
    public class InsigniaController : Controller
    {
        private EmPoderTICConexionFast db = new EmPoderTICConexionFast();

        public ActionResult Index()
        {
            var areas = db.AREA.ToList();
            var areasBloqueadas = new List<string>();

            foreach (var area in areas)
            {
                var insignias = db.INSIGNIA
                    .Include(i => i.NIVEL)
                    .Include(i => i.CERTIFICADO)
                    .Include(i => i.CERTIFICACION)
                    .Include(i => i.EVENTO.Select(e => e.ASISTENCIA))
                    .Where(i => i.AREA_area_id == area.area_id) // Filtrar por el área actual
                    .ToList();

                foreach (var INSIGNIA in insignias)
                {
                    bool alMenosUnRegistroFalso = INSIGNIA
                        .EVENTO
                        .SelectMany(evento => evento.ASISTENCIA)
                        .Any(asistencia => !asistencia.registro_asistencia_evento);

                    INSIGNIA.insignia_bloqueada = alMenosUnRegistroFalso;

                    if (alMenosUnRegistroFalso)
                    {
                        // Si al menos una insignia en el área tiene registro falso, agregar el área a las áreas bloqueadas
                        areasBloqueadas.Add(area.area_conocimiento);
                        break; // Salir del bucle interno
                    }
                }
            }

            var niveles = db.NIVEL.ToDictionary(n => n.nivel_id, n => n.categoría_nivel_insignia);
            ViewBag.Niveles = niveles;

            // Obtener el URL del certificado (puedes ajustar esta lógica según tu modelo de datos)
            var certificado = db.CERTIFICADO.FirstOrDefault(); // Puedes aplicar filtros aquí según tus necesidades

            if (certificado != null)
            {
                ViewBag.CertificadoURL = certificado.certificado_url;
            }
            else
            {
                // Si no se encuentra el certificado, puedes manejarlo aquí
                ViewBag.CertificadoURL = string.Empty; // O cualquier otro valor predeterminado que desees
            }

            ViewBag.AreasBloqueadas = areasBloqueadas;

            return View(areas);
        }


       
        public ActionResult Perfil()
        {
            // Comprueba si el usuario está autenticado
            if (Session["UsuarioAutenticado"] != null)
            {
                // Recupera la información del usuario autenticado desde la sesión
                var usuarioAutenticado = (USUARIO)Session["UsuarioAutenticado"];
                ViewBag.UsuarioAutenticado = usuarioAutenticado;


                // Filtra todos los eventos con asistencia verdadera para el usuario autenticado
                var eventosConAsistencia = db.EVENTO
                    .Where(e => e.USUARIO.rut == usuarioAutenticado.rut && e.ASISTENCIA.Any(a => a.registro_asistencia_evento))
                    .ToList();


                var informacionUsuarios = db.USUARIO
                     .Include(u => u.TIPO_PERFIL)
                     .Where(u => u.rut == usuarioAutenticado.rut) // Filtra la información solo para el usuario autenticado
                     .ToList();


                // Filtra las insignias de nivel 1 y 2 que no estén bloqueadas
                var insigniasNivel1y2 = db.INSIGNIA
                    .Include(i => i.NIVEL)
                    .Include(i => i.CERTIFICADO)
                    .Include(i => i.CERTIFICACION)
                    .Include(i => i.EVENTO.Select(e => e.ASISTENCIA))
                    .Include(i => i.AREA)
                    .Where(i => (i.NIVEL.categoría_nivel_insignia == "Principiante" || i.NIVEL.categoría_nivel_insignia == "Intermedio") && !i.insignia_bloqueada)
                    .ToList();



                // Filtra las insignias de nivel 3 que no están bloqueadas y están asociadas al usuario autenticado
                // Filtra las insignias de nivel 3 que no estén bloqueadas
                var insigniasNivel3 = db.INSIGNIA
                    .Include(i => i.NIVEL)
                    .Include(i => i.CERTIFICADO)
                    .Include(i => i.CERTIFICACION)
                    .Include(i => i.EVENTO.Select(e => e.ASISTENCIA))
                    .Include(i => i.AREA)
                    .Where(i => i.NIVEL.categoría_nivel_insignia == "Avanzado" && !i.insignia_bloqueada)
                    .ToList();



                // Asigna los datos filtrados a las ViewBag para su uso en la vista
                ViewBag.InsigniasNivel1y2 = insigniasNivel1y2;
                ViewBag.InsigniasNivel3 = insigniasNivel3;
                ViewBag.EventosConAsistencia = eventosConAsistencia;
                ViewBag.InformacionUsuarios = informacionUsuarios;
                return View("Perfil");
            }
            else
            {
                // Maneja el caso en el que el usuario no esté autenticado correctamente
                return RedirectToAction("Login", "Account"); // Redirige a la página de inicio de sesión u otra página adecuada
            }
        }

    }
}
