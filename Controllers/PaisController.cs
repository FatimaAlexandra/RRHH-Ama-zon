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
    public class PaisController : Controller
    {

        DbamazonContext context = new DbamazonContext();
        public IActionResult Index()
        {
            List<Paise> paises = context.Paises.ToList();
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
            Paise pais = context.Paises.Find(id);
            if (pais == null)
            {
                // Manejar el caso cuando no se encuentra el país
                return NotFound();
            }
            context.Paises.Remove(pais);
            context.SaveChanges();
            return Json(pais);
        }



        [HttpPost]
        public IActionResult CrearPais([FromBody] CrearPaisInputModel input)
        {
            // Crear una nueva instancia de pais con los datos recibidos
            Paise pais = new Paise
            {
                Nombre = input.Nombre,
                Isocode = input.Isocode
            };

            // Guardar el pais en la base de datos o realizar otras operaciones necesarias
            Paise paisCreado = context.Paises.Add(pais).Entity;
            context.SaveChanges();

            return Json(paisCreado); // Devolver el pais como respuesta JSON
        }




        [HttpPut("{id}")]
        [Route("Editar/{id}")]
        public IActionResult EditarPais(int id, [FromBody] EditarPaisInputModel input)
        {
            // Buscar el acuerdo a editar por su id
            Paise pais = context.Paises.Find(id);
            if (pais == null)
            {
                // Manejar el caso cuando no se encuentra el pais
                return NotFound();
            }

            // Actualizar el pais con los datos recibidos
            pais.Nombre = input.Nombre;
            pais.Isocode = input.Isocode;

            // Guardar el pais en la base de datos o realizar otras operaciones necesarias
            Paise paisActualizado = context.Paises.Update(pais).Entity;
            context.SaveChanges();

            return Json(paisActualizado); // Devolver el acuerdo como respuesta JSON
        }

    }
}
