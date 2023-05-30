using Microsoft.AspNetCore.Mvc;
using amazon.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using amazon.Models.Inputs;
using amazon.Models.Outputs;

namespace amazon.Controllers
{
    [Route("[controller]")]
    [Authorize]
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


        [HttpGet("{id}")]
        [Route("Obtener/{id}")]
        public ActionResult Obetener(int id)
        {
            Sede sede = context.Sedes.Find(id);
            if (sede == null)
            {
                return NotFound();
            }

            return Json(sede);
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]
        public ActionResult Delete(int id)
        {
            Sede sede = context.Sedes.Find(id);
            // imprimir hola en consola
            if (sede == null)
            {
                return NotFound();
            }

            context.Sedes.Remove(sede);

            context.SaveChanges();
            return Json(sede);
        }

        [HttpPost]
        public IActionResult CrearSede([FromBody] CrearSedeInputModel input)
        {
            Sede sede = new Sede 
            {
                Nombre = input.Nombre,
                Codigosede = input.Codigosede,
                Logo = input.Logo,
                Paisid = input.Paisid,
            };

            Sede nuevaSede = context.Sedes.Add(sede).Entity;
            context.SaveChanges();

            return Json(nuevaSede);
        }

        [HttpPut("{id}")]
        [Route("Editar/{id}")]
        public IActionResult EditarSede(int id, [FromBody] EditarSedeInputModel input)
        {
            Sede sede = context.Sedes.Find(id);
            if (sede == null)
            {
                return NotFound();
            }

            sede.Nombre = input.Nombre;
            sede.Codigosede = input.Codigosede;
            sede.Logo = input.Logo;
            sede.Paisid = input.Paisid;

            context.Entry(sede).State = EntityState.Modified;
            context.SaveChanges();

            return Json(sede);
        }

    }
}
