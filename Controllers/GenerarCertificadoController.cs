using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using EmPoderTIC.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Threading;
using System.Configuration;

namespace EmPoderTIC.Controllers
{
    public class GenerarCertificadoController : Controller
    {
        private EmPoderTIC_Conexion_Oficial_WEB db = new EmPoderTIC_Conexion_Oficial_WEB();
        // GET: GenerarCertificado
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Generar()
        {
            // Ruta de la plantilla PDF
            string templatePath = ConfigurationManager.AppSettings["TemplatePath"];

            // Carpeta de salida para los PDF individuales
            string outputFolder = Server.MapPath("~/App_Data/Certificados");
            var areas = db.AREA.ToList();

            foreach (var area in areas)
            {
                // Listar Certificado Por Area para un Usuario 
                var DatosUsuarios = db.USUARIO_CERTIFICADO
                                   .Include(uc => uc.USUARIO)
                                   .Include(uc => uc.CERTIFICADO.AREA)
                                   .Include(uc => uc.CERTIFICADO.INSIGNIA.COMPETENCIA)
                                   .ToList();

                foreach (var datosCertificado in DatosUsuarios)
                {
                    // Nombre del archivo de salida para el PDF individual
                    string outputPath = Path.Combine(outputFolder, $"{datosCertificado.USUARIO.nombre}_{datosCertificado.USUARIO.apellido_paterno}_Certificado.pdf");

                    using (FileStream fs = new FileStream(outputPath, FileMode.Create))
                    {
                        PdfReader reader = new PdfReader(templatePath);
                        PdfStamper stamper = new PdfStamper(reader, fs);
                        PdfContentByte content = stamper.GetOverContent(1);

                        // Establece las coordenadas (x, y) en puntos donde se agregará el texto
                        float x = 295; // Cambia esto a la coordenada x deseada
                        float y = 285; // Cambia esto a la coordenada y deseada

                        content.BeginText();
                        content.SetFontAndSize(BaseFont.CreateFont(), 52); // Personaliza la fuente y tamaño
                        content.SetTextMatrix(x, y);
                        content.ShowText(datosCertificado.USUARIO.nombre + " " + datosCertificado.USUARIO.apellido_paterno); // Utiliza las propiedades del usuario
                        content.EndText();

                        // Establece las coordenadas (x, y) para el segundo texto
                        float x2 = 145; // Cambia esto a la nueva coordenada x deseada
                        float y2 = 235; // Cambia esto a la nueva coordenada y deseada

                        // Define la fuente y el tamaño de fuente
                        BaseFont font = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                        float fontSize = 12;

                        // Define el alto de la fuente
                        float lineHeight = fontSize * 1.5f;

                        content.BeginText();
                        content.SetFontAndSize(font, fontSize);
                        content.SetTextMatrix(x2, y2);

                        // Agrega el texto con salto de línea para controlar el alto
                        string texto = "Este Certificado se concede en reconocimiento a la destacada participación y compromiso demostrados por \n" +
                            datosCertificado.USUARIO.nombre + " " + datosCertificado.USUARIO.apellido_paterno + " en el Área de " + datosCertificado.CERTIFICADO.AREA.area_conocimiento + " en colaboración con Más Mujeres en las TIC impartido por \n" + 
                            " Duoc UC. Durante su participación en este programa," + datosCertificado.USUARIO.nombre + " " + datosCertificado.USUARIO.apellido_paterno + " ha sobresalido en las competencias clave \n" +
                            "para el " + datosCertificado.CERTIFICADO.AREA.area_conocimiento + " y sus Competencias de Conocimientos.";

                        string[] lineas = texto.Split('\n');
                        foreach (string linea in lineas)
                        {
                            content.ShowText(linea);
                            content.SetTextMatrix(x2, y2 -= lineHeight);
                        }

                        content.EndText();

                        stamper.Close();
                    }

                    datosCertificado.certificado_url = Path.Combine(outputFolder, outputPath);
                    db.Entry(datosCertificado).State = EntityState.Modified;
                    db.SaveChanges();
                }

                ViewBag.Datosusuarios = DatosUsuarios;
            }

            return Content("PDFs generados y almacenados en la base de datos.");
        }


        public ActionResult ListaCertificados()
        {
            // Obtener la lista de archivos en la carpeta de salida
            string outputFolder = Server.MapPath("~/App_Data/Certificados");
            string[] archivos = Directory.GetFiles(outputFolder);

            // Puedes pasar la lista de archivos a la vista
            return View(archivos);
        }

        public ActionResult DescargarCertificado(string archivoPath)
        {
            // Verificar que el archivo existe
            if (System.IO.File.Exists(archivoPath))
            {
                return File(archivoPath, "application/pdf", Path.GetFileName(archivoPath));
            }
            else
            {
                return HttpNotFound(); // Devolver un error 404 si el archivo no existe
            }
        }










        // Acción para descargar un certificado específico por su ID



    }

}
