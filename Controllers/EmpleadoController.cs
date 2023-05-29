using Microsoft.AspNetCore.Mvc;
using amazon.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace amazon.Controllers
{
    [Authorize]
    public class EmpleadoController : Controller
    {
        //instanciamos conección a bd
        DbamazonContext context = new DbamazonContext();
        //creamos lista de contratos
        List<Empleado> empleados = new List<Empleado>(); 

        public IActionResult Index()
        {
            empleados = context.Empleados.Include(x => x.Contrato).Include(d => d.Documento).ToList();
            ViewBag.Empleados = empleados; //guarda objetos y muestra en lista
            return View();
        }
    }
}
