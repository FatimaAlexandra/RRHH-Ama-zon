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
        

        public IActionResult Index()
        {
            List<Acuerdo> acuerdos = context.Acuerdos.Include(p => p.Pais).ToList();

            return View(acuerdos);
        }
    }
}
