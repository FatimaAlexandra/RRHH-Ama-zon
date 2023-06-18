using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace amazon.Models
{
    public class Reports
    {
        public void GenerarReporteEmpleados(string ruta, DbamazonContext _dbContext, List<Empleado> empleados)
        {
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(ruta, FileMode.Create));
            document.Open();

            Header("Empleado", document);

            Logo("SV", document);

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

                document.NewPage();
                Header(empleado.Nombre, document);


                string template = empleado.Contrato.Acuerdo.Contenido;
                template = template.Replace("[nombre]", empleado.Nombre);
                template = template.Replace("[correo]", empleado.Correo);
                template = template.Replace("[telefono]", empleado.Telefono);
                template = template.Replace("[tipo_documento]", empleado.Documento.TipoDocumento);
                template = template.Replace("[numero_documento]", empleado.Documento.NumeroDocumento);
                template = template.Replace("[direccion]", empleado.Direccion);
                template = template.Replace("[cargo]", empleado.Direccion);
                template = template.Replace("[fecha_inicio]", empleado.Contrato.FechaInicio.ToString("dd/MM/yyyy"));
                if(empleado.Contrato.FechaFin is DateTime fechaFin)
                {
                    template = template.Replace("[fecha_fin]", fechaFin.ToString("dd/MM/yyyy")); 
                }
                template = template.Replace("[fecha_nacimiento]", empleado.FechaNacimiento.ToString("dd/MM/yyyy"));
                template = template.Replace("[pais]", empleado.Sede.Pais.Nombre);
                template = template.Replace("[sede]", empleado.Sede.Nombre);
                template = template.Replace("[tipo_contrato]", empleado.Contrato.Acuerdo.Tipo);
                template = template.Replace("[fecha_emision]", DateTime.Now.ToString());
                template = template.Replace("&nbsp", " ");


                Logo(empleado.Sede.Pais.Isocode, document);

                Paragraph espacio = new Paragraph("");
                espacio.Add(Environment.NewLine);
                espacio.Add(Environment.NewLine);
                espacio.Add(Environment.NewLine);

                Paragraph plantilla = new Paragraph(template);

         

                plantilla.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 17);

                document.Add(espacio);
                document.Add(plantilla);
            }




            document.Close();

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
