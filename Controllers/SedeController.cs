using Microsoft.AspNetCore.Mvc;
using amazon.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using amazon.Models.Inputs;
using amazon.Models.Outputs;

namespace amazon.Controllers
{
    [Route("[controller]")]
    public class SedeController : Controller
    {

        DbamazonContext context = new DbamazonContext();
        public IActionResult Index()
        {
            List<Sede> sedes = context.Sedes.Include(s => s.Pais).ToList();
            List<Paise> paises = context.Paises.ToList();
            ViewBag.Sedes = sedes;
            ViewBag.Paises = paises;
            return View();
        }
    }
}
