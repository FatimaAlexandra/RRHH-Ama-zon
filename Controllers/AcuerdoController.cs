using Microsoft.AspNetCore.Mvc;
using amazon.Models;
using Microsoft.EntityFrameworkCore;

namespace amazon.Controllers
{
    [Route("[controller]")]
    public class AcuerdoController : Controller
    {
        //instanciamos conección a bd
        DbamazonContext context = new DbamazonContext();

        [HttpPost]
        public IActionResult CrearAcuerdo(string nombre, string contenido, string tipo, int paisId)
        {
            // Crear una nueva instancia de Acuerdo con los datos recibidos
            Acuerdo acuerdo = new Acuerdo
            {
                Nombre = nombre,
                Contenido = contenido,
                Tipo = tipo,
                Paisid = paisId
            };

            // Guardar el acuerdo en la base de datos o realizar otras operaciones necesarias

            return Json(acuerdo); // Devolver el acuerdo como respuesta JSON
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
            Paise pais = context.Paises.Find(acuerdo.Paisid);
            acuerdo.Pais = pais;
            return Json(acuerdo);
            // var myObject = context.Acuerdos.Find(id);
            // // if (myObject == null)
            // // {
            // //     return NotFound();
            // // }

            // return Json(myObject);
        }
    }
}
