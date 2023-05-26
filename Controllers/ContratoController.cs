using Microsoft.AspNetCore.Mvc;
using amazon.Models;
using Microsoft.EntityFrameworkCore;

namespace amazon.Controllers
{
    public class ContratoController : Controller
    {
        //instanciamos conección a bd
        DbamazonContext context = new DbamazonContext();
        //creamos lista de contratos
        List<Contrato> contratos = new List<Contrato>(); 

        public IActionResult Index()
        {
            contratos = context.Contratos.Include(x => x.Empleados).ToList();
            ViewBag.Contratos = contratos; //guarda objetos y muestra en lista
            return View();
        }
    }
}
