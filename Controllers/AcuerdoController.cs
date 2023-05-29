using Microsoft.AspNetCore.Mvc;
using amazon.Models;
using Microsoft.EntityFrameworkCore;
using amazon.Models.Inputs;

namespace amazon.Controllers
{

    [Route("[controller]")]
    public class AcuerdoController : Controller
    {
        //instanciamos conección a bd
        DbamazonContext context = new DbamazonContext();

        [HttpPost]
        public IActionResult CrearAcuerdo([FromBody] CrearAcuerdoInputModel input)
        {
            // Crear una nueva instancia de Acuerdo con los datos recibidos
            Acuerdo acuerdo = new Acuerdo
            {
                Nombre = input.Nombre,
                Contenido = input.Contenido,
                Tipo = input.Tipo,
                Paisid = input.PaisId
            };

            // Guardar el acuerdo en la base de datos o realizar otras operaciones necesarias
            Acuerdo acuerdoCreado = context.Acuerdos.Add(acuerdo).Entity;
            context.SaveChanges();

            return Json(acuerdoCreado); // Devolver el acuerdo como respuesta JSON
        }

        [HttpPut("{id}")]
        [Route("Editar/{id}")]
        public IActionResult EditarAcuerdo(int id, [FromBody] EditarAcuerdoInputModel input)
        {
            // Buscar el acuerdo a editar por su id
            Acuerdo acuerdo = context.Acuerdos.Find(id);
            if (acuerdo == null)
            {
                // Manejar el caso cuando no se encuentra el acuerdo
                return NotFound();
            }

            // Actualizar el acuerdo con los datos recibidos
            acuerdo.Nombre = input.Nombre;
            acuerdo.Contenido = input.Contenido;
            acuerdo.Tipo = input.Tipo;
            acuerdo.Paisid = input.PaisId;

            // Guardar el acuerdo en la base de datos o realizar otras operaciones necesarias
            Acuerdo acuerdoActualizado = context.Acuerdos.Update(acuerdo).Entity;
            context.SaveChanges();

            return Json(acuerdoActualizado); // Devolver el acuerdo como respuesta JSON
        }

        public IActionResult Index()
        {
            List<Acuerdo> acuerdos = context.Acuerdos.Include(p => p.Pais).ToList();
            List<Paise> paises = context.Paises.ToList();
            ViewBag.Acuerdos = acuerdos;
            ViewBag.Paises = paises;

            return View();

        }

        [HttpGet("{id}")]
        [Route("Obtener/{id}")]
        public ActionResult Obetener(int id)
        {
            Acuerdo acuerdo = context.Acuerdos.Find(id);
            return Json(acuerdo);
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]
        public ActionResult Delete(int id)
        {
            Acuerdo acuerdo = context.Acuerdos.Find(id);
            if (acuerdo == null)
            {
                // Manejar el caso cuando no se encuentra el acuerdo
                return NotFound();
            }
            context.Acuerdos.Remove(acuerdo);
            context.SaveChanges();
            return Json(acuerdo);
        }

    }
}
