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
        public void GenerarReporteEmpleados(string ruta, DbamazonContext _dbContext, List<Empleado> empleadosSeleccionados, Document document)
        {
            //Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(ruta, FileMode.Create));
            document.Open();

            Header("Empleado", document);

            // Logo
            string ruta_logo = "wwwroot/images/Logo.png";
            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(ruta_logo);
            logo.ScaleToFit(100, 100);
            logo.SetAbsolutePosition(50, 800);

            // Agregar logo y elementos
            document.Add(logo);

            TContenidoEmpleado(empleadosSeleccionados, document);
            document.Close();
        }

        private void CellHeaderTable(string nombre, Document document, PdfPTable table)
        {
            PdfPCell cell = new PdfPCell(new Phrase(nombre, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD, BaseColor.WHITE)));
            cell.PaddingTop = 5f;
            cell.PaddingBottom = 5f;
            cell.BackgroundColor = new iTextSharp.text.BaseColor(33, 51, 99);
            table.AddCell(cell);
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

        private void TContenidoEmpleado(List<Empleado> empleadosSeleccionados, Document document)
        {
            // Agregar contenido al PDF
            PdfPTable table = new PdfPTable(11);
            table.WidthPercentage = 100;

            // Agregar encabezados de columna
            CellHeaderTable("ID", document, table);
            CellHeaderTable("Tipo Documento", document, table);
            CellHeaderTable("Número Documento", document, table);
            CellHeaderTable("Nombre", document, table);
            CellHeaderTable("Correo", document, table);
            CellHeaderTable("Fecha Nacimiento", document, table);
            CellHeaderTable("Teléfono", document, table);
            CellHeaderTable("Dirección", document, table);
            //CellHeaderTable("Sede", document, table);
            //CellHeaderTable("Código Sede", document, table);
            CellHeaderTable("Cargo", document, table);

            // Agregar filas al PDF con los datos de empleados seleccionados
            foreach (var empleado in empleadosSeleccionados)
            {
                table.AddCell(empleado.Id.ToString());
                table.AddCell(empleado.Documento.TipoDocumento);
                table.AddCell(empleado.Documento.NumeroDocumento);
                table.AddCell(empleado.Nombre);
                table.AddCell(empleado.Correo);
                table.AddCell(empleado.FechaNacimiento.ToString());
                table.AddCell(empleado.Telefono);
                table.AddCell(empleado.Direccion);
                //table.AddCell(empleado..Sede);
                //table.AddCell(empleado.Codigosede);
                table.AddCell(empleado.Contrato.Cargo);
            }
           

            // Agregar la tabla al documento PDF
            document.Add(table);
        }



    }
}
