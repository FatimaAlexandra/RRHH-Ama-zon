using Microsoft.AspNetCore.Mvc;
using amazon.Models;
using Microsoft.EntityFrameworkCore;

namespace amazon.Controllers
{
    public class EmpleadoController : Controller
    {
        //instanciamos conección a bd
        DbamazonContext context = new DbamazonContext();
        //creamos lista de contratos
        List<Empleado> empleados = new List<Empleado>(); 

        public IActionResult Index()
        {
            empleados = context.Empleados.Include(x => x.Contrato).ToList();
            ViewBag.Empleados = empleados; //guarda objetos y muestra en lista
            return View();
        }
    }
}
