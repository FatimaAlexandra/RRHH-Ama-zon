using amazon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace amazon.Controllers
{
    public class ReporteController : Controller
    {
        DbamazonContext _dbContext = new DbamazonContext();
        List<Empleado> empleadosSeleccionados = new List<Empleado>(); // Aquí debes tener la lista de empleados seleccionados

        public IActionResult GenerarReporteEmpleados()
        {
            var reportGenerator = new Reports();
            var filePath = "archivo.pdf";



            reportGenerator.GenerarReporteEmpleados(filePath, _dbContext, empleadosSeleccionados);

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/pdf", "ReporteEmpleados.pdf");
        }
    }
    
}
