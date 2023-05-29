using Microsoft.AspNetCore.Mvc;
using amazon.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using amazon.Models.Inputs;
using amazon.Models.Outputs;


namespace amazon.Controllers
{   
    [Route("[controller]")]
    public class PaisController : Controller
    {

        DbamazonContext context = new DbamazonContext();
        public IActionResult Index()
        {
            List<Paise> paises = context.Paises.ToList();
            ViewBag.Paises = paises;
            return View();
        }
    }
}
