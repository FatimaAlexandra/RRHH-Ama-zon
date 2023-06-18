using amazon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Text.Json;

namespace amazon.Controllers
{
    public class ReporteController : Controller
    {
        DbamazonContext _dbContext = new DbamazonContext();
        // Crear el objeto Document
        Document document = new Document();
        List<Object> empleadosSeleccionados = new List<Object>(); // Aquí debes tener la lista de empleados seleccionados


        [HttpPost]
        public async Task<IActionResult> GenerarDatosEmpleado(string empleados, string esperado)
        {
            List<Object> emps = JsonSerializer.Deserialize<List<Object>>(empleados);

            var reportGenerator = new Reports();
            var filePath = "archivo.pdf";
            //await Task.Run(() => reportGenerator.GenerarReporteEmpleados(filePath, _dbContext, emps));

            byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(fileBytes, "application/pdf", "ReporteEmpleados.pdf");
        }

        
        public IActionResult GenerarAcuerdos()
        {
            var reportGenerator = new Reports();
            var filePath = "archivo.pdf";

            var empleadoSeleccionados = _dbContext.Empleados.ToList();

            MemoryStream pdfStream = reportGenerator.GenerarReporteEmpleados(filePath, _dbContext, empleadoSeleccionados);
            
            // Establecer el tipo de contenido y nombre de archivo en la respuesta
            Response.Headers.Add("Content-Disposition", "attachment; filename=nombre-archivo.pdf");
            Response.ContentType = "application/pdf";
            return File(pdfStream.ToArray(), "application/pdf");
            // byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            // return File(fileBytes, "application/pdf", "ReporteEmpleados.pdf");
        }

       
    }
    
}
