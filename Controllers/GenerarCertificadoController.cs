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
using QRCoder;
using System.Drawing;
using System.Xml.Linq;

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


                        float qrX = 725; // ajusta esta coordenada x
                        float qrY = 40; // ajusta esta coordenada y



                        // Generar código QR
                        string eventoDescripcion = "Ha participado en los Eventos del Área de Conocimiento de: ";
                        string qrText = $"{datosCertificado.USUARIO.nombre} {datosCertificado.USUARIO.apellido_paterno} {eventoDescripcion.Substring(0, Math.Min(eventoDescripcion.Length, 50))} {datosCertificado.CERTIFICADO.AREA.area_conocimiento}";
                        byte[] qrBytes = GenerateQrCode(qrText);

                        // Convertir la imagen de QR a iTextSharp Image
                        iTextSharp.text.Image qrImage = iTextSharp.text.Image.GetInstance(qrBytes);
                        qrImage.ScaleAbsolute(100, 100); // Ajusta el tamaño según tus necesidades

                        // Agregar la imagen QR directamente al contenido (content) sin usar document
                        content.AddImage(qrImage, qrImage.ScaledWidth, 0, 0, qrImage.ScaledHeight, qrX, qrY);


                        // ... Resto de tu código para agregar texto al PDF ...

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

        // Método para generar el código QR
        private byte[] GenerateQrCode(string qrText)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            using (MemoryStream stream = new MemoryStream())
            {
                qrCodeImage.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
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
