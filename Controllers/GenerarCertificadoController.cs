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

namespace EmPoderTIC.Controllers
{
    public class GenerarCertificadoController : Controller
    {
        private EmPoderTIC_OFICIAL db = new EmPoderTIC_OFICIAL();
        // GET: GenerarCertificado
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Generar()
        {
            // Ruta de la plantilla PDF
            string templatePath = "C://Users//tbmv1//Downloads//PDF//Entrada//Certificado_Estructura.PDF";

            // Carpeta de salida para los PDF individuales
            string outputFolder = "C://Users//Public//Downloads//Certificados";
            var areas = db.AREA.ToList();

            foreach (var area in areas)
            {
                // Listar Certificado Por Area para un Usuario 
                var DatosUsuarios = db.USUARIO_CERTIFICADO
                                   .Include(uc => uc.USUARIO)
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

                        stamper.Close();
                    }

                   

                    // Actualiza el campo CertificadoPdf en la entidad Usuario
                    datosCertificado.certificado_url = Path.Combine(outputFolder, outputPath);
                    db.Entry(datosCertificado).State = EntityState.Modified;
                    db.SaveChanges();
                }

                ViewBag.Datosusuarios = DatosUsuarios;
            }

            return Content("PDFs generados y almacenados en la base de datos.");
        }


        public ActionResult ObtenerCertificado(string nombre, string apellido)
        {
            string outputFolder = "C://Users//Public//Downloads//Certificados";
            string outputPath = Path.Combine(outputFolder, $"{nombre}_{apellido}_Certificado.pdf");

            if (System.IO.File.Exists(outputPath))
            {
                // Si el archivo existe, lo entregamos como un archivo para su descarga
                return File(outputPath, "application/pdf", $"{nombre}_{apellido}_Certificado.pdf");
            }
            else
            {
                // Si el archivo no existe, puedes mostrar un mensaje de error o redirigir a una página de error.
                return Content("El certificado no está disponible.");
            }
        }






        // Acción para descargar un certificado específico por su ID



    }

}
