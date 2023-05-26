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
                        /*
                         * 
                     nombre, correo, fecha_nacimiento, telefono, direccion, codigosede, fecha_inicio_contrato, 
                        fecha_fin_contrato, cargo, acuerdoid, isocode, tipo_documento, numero_documento
                         */

                        //Guardar los datos en el empleado
                        Empleado empleado = new Empleado();

                        empleado.Nombre = valores[0];
                        empleado.Correo = valores[1];
                        empleado.FechaNacimiento = DateTime.Parse(valores[2]);
                        empleado.Telefono = valores[3];
                        empleado.Direccion = valores[4];
                        empleado.Documento.TipoDocumento = valores[5];
                        empleado.Documento.NumeroDocumento = valores[6];
                        empleado.Contrato.FechaInicio = DateTime.Parse(valores[7]);
                        empleado.Contrato.FechaFin = DateTime.Parse(valores[8]);



                        _context.Empleados.Add(empleado);
                        _context.SaveChanges();

                       /* Contrato contrato = new Contrato();
                        contrato.FechaInicio = valores[4];
                        contrato.FechaFin = valores[5];
                        contrato.Tipo = valores[6];
                        contrato.IdEmpleado = valores[0];
                        contrato.Cargo = valores[7];*/


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
