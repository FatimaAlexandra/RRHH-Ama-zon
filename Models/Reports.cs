using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iText.Html2pdf;
using System.Text;
using amazon.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace amazon.Models
{
    public class Reports
    {
        public MemoryStream GenerarReporteEmpleados(string ruta, DbamazonContext _dbContext, List<Empleado> empleados)
        {
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(ruta, FileMode.Create));
            document.Open();

            Header("Empleado", document);

            Logo("SV", document);

            string cssStyle = ".new-page {page-break-before: always;} html, body {width: 100%;height: 842px; width: 595px; margin-left: auto; margin-right: auto;} body {margin: 20px;}img { width: 100%; } p, h1, h2, h3, h4, h5, h6, span, b, strong, li { width: 100%; word-break: normal; } p, li { text-align: justify;}";

            string htmlContent = "<!DOCTYPE html><html lang=\"en\"><head><meta charset=\"UTF-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"><title>PDF</title></head><body>";
            htmlContent = "<style>" + cssStyle + "</style>";
        

            foreach (Empleado emp in empleados)
            {
                Empleado empleado = _dbContext.Empleados
                    .Include(e => e.Contrato)
                    .ThenInclude(c => c.Acuerdo)
                    .Include(e => e.Documento)
                    .Include(e => e.Sede)
                    .ThenInclude(s => s.Pais)
                    .Include(e => e.Contrato)

                .FirstOrDefault(e => e.Id == emp.Id);

                
                htmlContent += "<div style=\"clear:both;\"><figure style=\"float:left;\"><img style=\"width: 100px;\" src=\"wwwroot/images/" + empleado.Sede.Pais.Isocode +".png\" alt=\"Logo-Amazon-El-Salvador\" border=\"0\"></figure><figure style=\"float:right;\"><img style=\"width: 100px;\" src=\"wwwroot/images/Logo.png\" alt=\"Logo-Amazon-El-Salvador\" border=\"0\"></figure></div>";
                htmlContent += "<div style=\"clear: both;\"></div>";
                
                htmlContent += empleado.Contrato.Acuerdo.Contenido;                
                htmlContent = htmlContent.Replace("&nbsp;", " ");

                // remplazar variables: nombre, correo, fecha_nacimiento, telefono, direccion, tipo_documento, numero_documento, fecha_inicio, cargo, tipo_contrato, fecha_fin, sede, pais
                htmlContent = htmlContent.Replace("[nombre]", empleado.Nombre);
                htmlContent = htmlContent.Replace("[correo]", empleado.Correo);
                htmlContent = htmlContent.Replace("[fecha_nacimiento]", empleado.FechaNacimiento.ToString("dd/MM/yyyy"));
                htmlContent = htmlContent.Replace("[telefono]", empleado.Telefono);
                htmlContent = htmlContent.Replace("[direccion]", empleado.Direccion);
                htmlContent = htmlContent.Replace("[tipo_documento]", empleado.Documento.TipoDocumento);
                htmlContent = htmlContent.Replace("[numero_documento]", empleado.Documento.NumeroDocumento);
                htmlContent = htmlContent.Replace("[fecha_inicio]", empleado.Contrato.FechaInicio.ToString("dd/MM/yyyy"));
                htmlContent = htmlContent.Replace("[cargo]", empleado.Contrato.Cargo);
                htmlContent = htmlContent.Replace("[tipo_contrato]", empleado.Contrato.Acuerdo.Tipo);
                if (empleado.Contrato.FechaFin is DateTime fechaFin) {
                    htmlContent = htmlContent.Replace("[fecha_fin]", fechaFin.ToString("dd/MM/yyyy"));
                }
                htmlContent = htmlContent.Replace("[sede]", empleado.Sede.Nombre);
                htmlContent = htmlContent.Replace("[pais]", empleado.Sede.Pais.Nombre);

                if (empleado.Salario is null) {
                    htmlContent = htmlContent.Replace("[salario]", "No especificado");
                } else {
                    htmlContent = htmlContent.Replace("[salario]", empleado.Salario.ToString());
                }

                htmlContent += "<div class=\"new-page\"></div>";
                // document.NewPage();
                // Header(empleado.Nombre, document);


                // string template = empleado.Contrato.Acuerdo.Contenido;
                // template = template.Replace("[Nombre]", empleado.Nombre);
                // template = template.Replace("[TipoDocumento]", empleado.Documento.TipoDocumento);
                // template = template.Replace("[NumeroDocumento]", empleado.Documento.NumeroDocumento);
                // template = template.Replace("[Direccion]", empleado.Direccion);
                // template = template.Replace("[Cargo]", empleado.Direccion);
                // template = template.Replace("[FechaInicio]", empleado.Contrato.FechaInicio.ToString("dd/MM/yyyy"));
                // //template = template.Replace("[FechaFin]", empleado.Contrato.FechaFin.ToString("dd/MM/yyyy")); 
                // template = template.Replace("[FechaNacimiento]", empleado.FechaNacimiento.ToString("dd/MM/yyyy"));
                // template = template.Replace("[Pais]", empleado.Sede.Pais.Nombre);
                // template = template.Replace("[Sede]", empleado.Sede.Nombre);
                // template = template.Replace("[FechaEmision]", DateTime.Now.ToString());


                // Logo(empleado.Sede.Pais.Isocode, document);

                // Paragraph espacio = new Paragraph("");
                // espacio.Add(Environment.NewLine);
                // espacio.Add(Environment.NewLine);
                // espacio.Add(Environment.NewLine);

                // Paragraph plantilla = new Paragraph(template);
                // //plantilla.Alignment = Element.ALIGN_RIGHT;
                // //plantilla.Add(Environment.NewLine);
                // //plantilla.Add(Environment.NewLine);
              
         

                // plantilla.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 17);

                // document.Add(espacio);
                // document.Add(plantilla);
            }

            htmlContent = htmlContent + "</body></html>";

            // Crear el convertidor
            ConverterProperties converterProperties = new ConverterProperties();

            // Generar el PDF en memoria
            MemoryStream pdfStream = new MemoryStream();
            HtmlConverter.ConvertToPdf(htmlContent, pdfStream, converterProperties);
            
            
            return pdfStream;
            

            // document.Close();

            // nuevo -----

        }


        private void CellHeaderTable(string nombre, Document document, PdfPTable table)
        {
            PdfPCell cell = new PdfPCell(new Phrase(nombre, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD, BaseColor.WHITE)));
            cell.PaddingTop = 5f;
            cell.PaddingBottom = 5f;
            cell.BackgroundColor = new iTextSharp.text.BaseColor(33, 51, 99);
            table.AddCell(cell);
        }

        private void Logo(string file,  Document document)
        {
            // Logo
            string ruta_logo = "wwwroot/images/" + file + ".png";
            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(ruta_logo);
            logo.ScaleToFit(100, 100);
            logo.SetAbsolutePosition(50, 700);

            // Agregar logo y elementos
            document.Add(logo);
        }

        private void Header(string titulo, Document document)
        {
            // Título
            Phrase headerPhrase = new Phrase();
            Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 20);
            Chunk titleChunk = new Chunk(titulo, titleFont);
            headerPhrase.Add(titleChunk);
            Paragraph headerParagraph = new Paragraph(headerPhrase);
            headerParagraph.Alignment = Element.ALIGN_CENTER;

            // Fecha
            Paragraph fecha = new Paragraph(DateTime.Now.ToString());
            fecha.Alignment = Element.ALIGN_RIGHT;
            fecha.Add(Environment.NewLine);
            fecha.Add(Environment.NewLine);
            fecha.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 17);

            // Agregar fecha y título
            document.Add(headerParagraph);
            document.Add(fecha);
        }


    }
}
