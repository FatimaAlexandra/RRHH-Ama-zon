using amazon.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Web;

namespace amazon.Controllers
{
    [Authorize]
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


                        Paise pais = _context.Paises.FirstOrDefault(p => p.Isocode == valores[11]); // 11 => isocode 
                        Acuerdo acuerdo = _context.Acuerdos.FirstOrDefault(a => a.Paisid == pais.Id);

                       
                        documento.TipoDocumento = valores[9]; // 9 => tipo documento
                        documento.NumeroDocumento = valores[10]; // 10 => documento
                        _context.Documentos.Add(documento);
                        _context.SaveChanges();
                       
                        
                        //Guardar los datos del contrato

                        Contrato contrato = new Contrato();

                        contrato.FechaInicio = DateTime.Parse(valores[6]); // 6 => fecha inicio contrato
                        contrato.FechaFin = DateTime.Parse(valores[7]); // 7 => fecha fin contrato
                        contrato.Cargo = valores[8]; // 8 => cargo
                        contrato.Acuerdoid = acuerdo.Id;
                        _context.Contratos.Add(contrato);
                        _context.SaveChanges();
                        
                        //Guardar los datos del empleado

                        Empleado empleado = new Empleado();

                        empleado.Nombre = valores[0]; // 0 => nombre
                        empleado.Correo = valores[1]; // 1 => correo
                        empleado.FechaNacimiento = DateTime.Parse(valores[2]); // 2 => fecha nacimiento
                        empleado.Telefono = valores[3]; // 3 => telefono
                        empleado.Direccion = valores[4]; // 4 => direccion
                        empleado.Salario = valores[12]; // 12 => salario
                        
                        Sede sede = _context.Sedes.FirstOrDefault(s => s.Codigosede == valores[5]); // 5 => codigo sede
                        
                        empleado.Sedeid = sede.Id;
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
