using amazon.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Web;

namespace amazon.Controllers
{
    public class FileController : Controller
    {
        DbamazonContext _context = new DbamazonContext();
        [HttpPost]
        public ActionResult CargarArchivo(IFormFile archivo)
        {
            if (archivo != null && archivo.Length > 0)
            {
                using (var reader = new StreamReader(archivo.OpenReadStream()))
                {
                    while (!reader.EndOfStream)
                    {
                        var linea = reader.ReadLine();
                        var valores = linea.Split(',');

                        
                        //Guardar los datos en el empleado
                        Empleado empleado = new Empleado();
                        empleado.Id = valores[0];
                        empleado.Nombre = valores[1];
                        empleado.Correo = valores[2];
                        empleado.FechaNacimiento = valores[3];


                        _context.Empleados.Add(empleado);
                        _context.SaveChanges();

                        Contrato contrato = new Contrato();
                        contrato.FechaInicio = valores[4];
                        contrato.FechaFin = valores[5];
                        contrato.Tipo = valores[6];
                        contrato.IdEmpleado = valores[0];
                        contrato.Cargo = valores[7];


                        _context.Contratos.Add(contrato);
                        _context.SaveChanges();
                    }
                }

                // Redireccionar a la vista de éxito
                return RedirectToAction("Create");
            }

            // Redireccionar a la vista de error
            return RedirectToAction("Create");
        }


        public IActionResult Create()
        {

            return View();
        }

    }
}
