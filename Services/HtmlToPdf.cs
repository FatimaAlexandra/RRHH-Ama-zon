using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using amazon.Models;
using iText.Html2pdf;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Text;
using iText.Html2pdf;

namespace amazon.Services{
    
    public class HtmlToPdf
    {
        public string GenerarPdfStream(Empleado empleado)
        {
            DbamazonContext context = new DbamazonContext();
            Contrato contrato = context.Contratos.Find(empleado.Contratoid);
            Documento documento = context.Documentos.Find(empleado.Documentoid);
            Sede sede = context.Sedes.Find(empleado.Sedeid);
            Acuerdo acuerdo = context.Acuerdos.Find(contrato.Acuerdoid);
            Paise pais = context.Paises.Find(sede.Paisid);

            string cssStyle = "html, body {width: 100%;height: 842px; width: 595px; margin-left: auto; margin-right: auto;} body {margin: 20px;}img { width: 100%; } p, h1, h2, h3, h4, h5, h6, span, b, strong, li { width: 100%; word-break: normal; } p, li { text-align: justify;}";

            string htmlContent = "<!DOCTYPE html><html lang=\"en\"><head><meta charset=\"UTF-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"><title>PDF</title></head><body>";
            htmlContent = "<style>" + cssStyle + "</style>";
            htmlContent += "<div style=\"clear:both;\"><figure style=\"float:left;\"><img style=\"width: 100px;\" src=\"wwwroot/images/" + pais.Isocode +".png\" alt=\"Logo-Amazon-El-Salvador\" border=\"0\"></figure><figure style=\"float:right;\"><img style=\"width: 100px;\" src=\"wwwroot/images/Logo.png\" alt=\"Logo-Amazon-El-Salvador\" border=\"0\"></figure></div>";
            htmlContent += "<div style=\"clear: both;\"></div>";
            htmlContent = htmlContent + acuerdo.Contenido + "</body></html>";

            htmlContent = htmlContent.Replace("&nbsp;", " ");
            
            // remplazar variables: nombre, correo, fecha_nacimiento, telefono, direccion, tipo_documento, numero_documento, fecha_inicio, cargo, tipo_contrato, fecha_fin, sede, pais
            htmlContent = htmlContent.Replace("[nombre]", empleado.Nombre);
            htmlContent = htmlContent.Replace("[correo]", empleado.Correo);
            htmlContent = htmlContent.Replace("[fecha_nacimiento]", empleado.FechaNacimiento.ToString("dd/MM/yyyy"));
            htmlContent = htmlContent.Replace("[telefono]", empleado.Telefono);
            htmlContent = htmlContent.Replace("[direccion]", empleado.Direccion);
            htmlContent = htmlContent.Replace("[tipo_documento]", documento.TipoDocumento);
            htmlContent = htmlContent.Replace("[numero_documento]", documento.NumeroDocumento);
            htmlContent = htmlContent.Replace("[fecha_inicio]", contrato.FechaInicio.ToString("dd/MM/yyyy"));
            htmlContent = htmlContent.Replace("[cargo]", contrato.Cargo);
            htmlContent = htmlContent.Replace("[tipo_contrato]", acuerdo.Tipo);
            if (contrato.FechaFin is DateTime fechaFin) {
                htmlContent = htmlContent.Replace("[fecha_fin]", fechaFin.ToString("dd/MM/yyyy"));
            }
            htmlContent = htmlContent.Replace("[sede]", sede.Nombre);
            htmlContent = htmlContent.Replace("[pais]", pais.Nombre);
            htmlContent = htmlContent.Replace("[fecha_emision]", DateTime.Now.ToString("dd/MM/yyyy"));
            
            if (empleado.Salario is null) {
                htmlContent = htmlContent.Replace("[salario]", "No especificado");
            } else {
                htmlContent = htmlContent.Replace("[salario]", empleado.Salario);
            }

            byte[] fileContents = Encoding.UTF8.GetBytes(htmlContent);
            
            string fileName = "htmls/empleado-" + empleado.Id + ".html" ; 

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            System.IO.File.WriteAllBytes(filePath, fileContents);


            return fileName;
            
        }
    }

    
}

