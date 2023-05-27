using Microsoft.AspNetCore.Mvc;
using amazon.Models;
using Microsoft.EntityFrameworkCore;

namespace amazon.Controllers
{
    public class AcuerdoController : Controller
    {
        //instanciamos conección a bd
        private readonly DbamazonContext context;
        public AcuerdoController(DbamazonContext _context)
        {
            context = _context;
        }

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
            return View(acuerdos);

        }
    }
}
