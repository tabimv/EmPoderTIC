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
using EmPoderTIC.Models.ViewModels;
using System.Security.Cryptography;


namespace EmPoderTIC.Controllers
{
    public class VistaPerfil3Controller : Controller
    {
        // GET: VistaPerfil3
        private EmPoderTIC_Conexion_Oficial db = new EmPoderTIC_Conexion_Oficial(); // Tu contexto de base de datos
        public ActionResult Index()
        {
            if (Session["UsuarioAutenticado"] != null)
            {
                var areas = db.AREA.ToList();
                var usuarioAutenticado = (USUARIO)Session["UsuarioAutenticado"];
                ViewBag.UsuarioAutenticado = usuarioAutenticado;
                // Verificar si hay alguna insignia bloqueada
                bool algunaInsigniaBloqueada = false;


                // Crea una lista para almacenar las listas de insignias por área
                var listaInsigniasPorArea = new List<List<InsigniaDesbloqueoViewModel>>();
                USUARIO_CERTIFICADO certificado = null; // Inicializa certificado fuera del bucle

                foreach (var area in areas)
                {
                    var listaInsignias = db.INSIGNIA
                        .Include(i => i.EVENTO)
                        .Include(i => i.AREA)
                        .OrderBy(i => i.NIVEL.nivel_id)
                        .Where(i => i.AREA.area_id == area.area_id && i.TIPO_PERFIL.tipo_perfil_id == 3)
                        .ToList();

                    var listaViewModel = listaInsignias
                        .Select(insignia => new InsigniaDesbloqueoViewModel
                        {
                            Insignia = insignia,
                            DesbloqueoInsignia = db.CONTROL_INSIGNIA
                                .FirstOrDefault(di => di.INSIGNIA_insignia_id == insignia.insignia_id
                                    && di.USUARIO_rut == usuarioAutenticado.rut)
                        })
                        .ToList();

                    if (!db.OTORGAR_INSIGNIA_P3.Any(a => a.USUARIO_rut == usuarioAutenticado.rut))
                    {
                        // Si el usuario no tiene registros en la tabla OTORGAR_INSIGNIA, asigna un valor especial
                        // para marcar que se deben mostrar todas las insignias con el filtro grayscale.
                        foreach (var viewModel in listaViewModel)
                        {
                            viewModel.DesbloqueoInsignia = new CONTROL_INSIGNIA { insignia_bloqueada = true };
                        }
                    }

                    // Verificar si alguna insignia está bloqueada en esta área
                    // Verificar si todas las insignias en esta área están desbloqueadas
                    if (listaViewModel.All(viewModel => viewModel.DesbloqueoInsignia != null && !viewModel.DesbloqueoInsignia.insignia_bloqueada))
                    {
                        algunaInsigniaBloqueada = false;
                    }


                    // Verifica si hay un certificado para este usuario en esta área
                    var certificadoArea = db.USUARIO_CERTIFICADO.Include(c => c.CERTIFICADO.AREA).FirstOrDefault(c => c.USUARIO_rut == usuarioAutenticado.rut && c.CERTIFICADO.INSIGNIA_insignia_id == c.CERTIFICADO.INSIGNIA.insignia_id && c.CERTIFICADO.AREA_area_id == c.CERTIFICADO.AREA.area_id);

                    if (certificadoArea != null && (certificado == null || certificadoArea.fecha_otorgamiento > certificado.fecha_otorgamiento))
                    {
                        certificado = certificadoArea;
                    }


                    listaInsigniasPorArea.Add(listaViewModel);
                }

                ViewBag.Certificado = certificado;
                ViewBag.ListaInsigniasPorArea = listaInsigniasPorArea;
                ViewBag.AlgunaInsigniaBloqueada = algunaInsigniaBloqueada;



                return View(areas);
            }

            // Maneja el caso en el que el usuario no esté autenticado correctamente
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Perfil()
        {
            // Comprueba si el usuario está autenticado
            if (Session["UsuarioAutenticado"] != null)
            {
                // Recupera la información del usuario autenticado desde la sesión
                var usuarioAutenticado = (USUARIO)Session["UsuarioAutenticado"];
                ViewBag.UsuarioAutenticado = usuarioAutenticado;


                // Filtra todos los eventos con registro de otorgamiento verdadera para el usuario autenticado
                var eventosConAsistencia = db.OTORGAR_INSIGNIA_P3
                 .Include(a => a.EVENTO)
                 .Where(a => a.USUARIO_rut == usuarioAutenticado.rut && a.registro_insignia_evento == true).ToList();


                var informacionUsuarios = db.USUARIO
                     .Include(u => u.TIPO_PERFIL)
                     .Where(u => u.rut == usuarioAutenticado.rut) // Filtra la información solo para el usuario autenticado
                     .ToList();


                var insigniasNivel3 = db.CONTROL_INSIGNIA
                    .Where(di => di.insignia_bloqueada == false)
                    .Include(di => di.INSIGNIA)
                    .Where(di => di.INSIGNIA.NIVEL.categoría_nivel_insignia == "Avanzado" && di.USUARIO_rut == usuarioAutenticado.rut)
                    .Include(di => di.INSIGNIA.EVENTO)
                    .ToList();


                var insigniasNivel1y2 = db.CONTROL_INSIGNIA
                    .Where(d => d.insignia_bloqueada == false)
                    .Include(d => d.INSIGNIA)
                    .Where(d => (d.INSIGNIA.NIVEL.categoría_nivel_insignia == "Principiante" || d.INSIGNIA.NIVEL.categoría_nivel_insignia == "Intermedio") && d.USUARIO_rut == usuarioAutenticado.rut)
                    .Include(d => d.INSIGNIA.EVENTO)
                    .ToList();

                var certificado = db.USUARIO_CERTIFICADO
                .Where(uc => uc.USUARIO_rut == usuarioAutenticado.rut).ToList();

                var datosPerfil = db.INFO_PERFIL.Where(ip => ip.USUARIO_rut == usuarioAutenticado.rut).ToList();



                // Asigna los datos filtrados a las ViewBag para su uso en la vista
                ViewBag.InsigniasDeNivel3 = insigniasNivel3;
                ViewBag.InsigniasDeNivel1y2 = insigniasNivel1y2;
                ViewBag.EventosConAsistencia = eventosConAsistencia;
                ViewBag.InformacionUsuarios = informacionUsuarios;
                ViewBag.Certificado = certificado;
                ViewBag.DatosPerfil = datosPerfil;
                return View("Perfil");
            }
            else
            {
                // Maneja el caso en el que el usuario no esté autenticado correctamente
                return RedirectToAction("Login", "Account"); // Redirige a la página de inicio de sesión u otra página adecuada
            }
        }



        public ActionResult CompletaPerfil()
        {
            // Comprueba si el usuario está autenticado
            if (Session["UsuarioAutenticado"] != null)
            {
                var usuarioAutenticado = (USUARIO)Session["UsuarioAutenticado"];

                // Verifica si ya existe información de perfil para el usuario
                var infoPerfil = db.INFO_PERFIL.FirstOrDefault(info => info.USUARIO_rut == usuarioAutenticado.rut);

                if (infoPerfil != null)
                {
                    // Ya hay información de perfil, muestra los detalles
                    ViewBag.InfoPerfil = infoPerfil;
                }
                else
                {
                    // No hay información de perfil, muestra el formulario para completar el perfil
                    ViewBag.MostrarFormulario = true;
                }

                return View();
            }
            else
            {
                // Maneja el caso en el que el usuario no esté autenticado correctamente
                return RedirectToAction("Login", "Account"); // Redirige a la página de inicio de sesión u otra página adecuada
            }
        }

        [HttpPost]
        public ActionResult CompletaPerfil(INFO_PERFIL nuevoPerfil)
        {
            if (ModelState.IsValid)
            {
                // Asigna el usuario actual al nuevo perfil
                nuevoPerfil.USUARIO_rut = ((USUARIO)Session["UsuarioAutenticado"]).rut;

                // Agrega el nuevo perfil a la base de datos
                db.INFO_PERFIL.Add(nuevoPerfil);
                db.SaveChanges();

                // Redirige a la página de perfil para mostrar los detalles actualizados
                return RedirectToAction("Perfil");
            }

            // Si el modelo no es válido, vuelve a mostrar el formulario con errores
            return View(nuevoPerfil);
        }


    }
}