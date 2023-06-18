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
        List<Empleado> empleadosSeleccionados = new List<Empleado>(); // Aquí debes tener la lista de empleados seleccionados


        [HttpPost]
        public IActionResult GenerarDatosEmpleado(string empleados, string esperado)
        {
            List<Object> emps = JsonSerializer.Deserialize<List<Object>>(empleados);

            foreach (var emp in emps)
            {
                //

            }

            return Json(emps);
        }

        [HttpPost]
        public IActionResult GenerarReporteEmpleados()
        {
            var reportGenerator = new Reports();
            var filePath = "archivo.pdf";



            reportGenerator.GenerarReporteEmpleados(filePath, _dbContext, empleadosSeleccionados, document);

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/pdf", "ReporteEmpleados.pdf");
        }

       
    }
    
}
