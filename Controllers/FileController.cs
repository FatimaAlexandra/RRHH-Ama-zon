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
                        
                        //---------------------------------------------------------------------

                        //Guardar los datos del documento
                        Documento documento = new Documento();
                       
                        documento.TipoDocumento = valores[0];
                        documento.NumeroDocumento = valores[1];
                        _context.Documentos.Add(documento);
                        _context.SaveChanges();
                       
                        
                        //Guardar los datos del contrato

                        Contrato contrato = new Contrato();

                        contrato.FechaInicio = DateTime.Parse(valores[2]);
                        contrato.FechaFin = DateTime.Parse(valores[3]);
                        contrato.Cargo = valores[4];
                        contrato.Acuerdoid = 1;
                        _context.Contratos.Add(contrato);
                        _context.SaveChanges();
                        
                        //Guardar los datos del empleado

                        Empleado empleado = new Empleado();

                        empleado.Nombre = valores[5];
                        empleado.Correo = valores[6];
                        empleado.FechaNacimiento = DateTime.Parse(valores[7]);
                        empleado.Telefono = valores[8];
                        empleado.Direccion = valores[9];
                        empleado.Sedeid = 1;
                        empleado.Contratoid = contrato.Id;
                        empleado.Documentoid = documento.Id;


                        _context.Empleados.Add(empleado);
                        _context.SaveChanges();
                        

//---------------------------------------------------------------------

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
