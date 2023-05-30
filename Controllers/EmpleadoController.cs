using Microsoft.AspNetCore.Mvc;
using amazon.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using amazon.Models.Inputs;
using amazon.Models.Outputs;

namespace amazon.Controllers
{
    [Route("[controller]")]
    // [Authorize]

    public class EmpleadoController : Controller
    {
        //instanciamos conección a bd
        DbamazonContext context = new DbamazonContext();
        //creamos lista de contratos
        List<Empleado> empleados = new List<Empleado>(); 

        public IActionResult Index()
        {
            empleados = context.Empleados
                .Include(x => x.Contrato)
                .Include(d => d.Documento)
                .Include(s => s.Sede)
                .ToList();
            List<Sede> sedes = context.Sedes.ToList();

            ViewBag.Empleados = empleados; //guarda objetos y muestra en lista\
            ViewBag.Sedes = sedes;
            return View();
        }

        [HttpGet("{id}")]
        [Route("Obtener/{id}")]
        public ActionResult Obetener(int id)
        {
            Empleado empleado = context.Empleados
                .Include(x => x.Contrato)
                .Include(d => d.Documento)
                .Include(s => s.Sede)
                .FirstOrDefault(x => x.Id == id);

            if (empleado == null) {
                return NotFound();
            }

            // Sede sede = context.Sedes.Find(empleado.SedeId);
            // if (sede == null) {
            //     return NotFound();
            // }
            Contrato contrato = context.Contratos.Find(empleado.Contratoid);
            if (contrato == null) {
                return NotFound();
            }
            Documento documento = context.Documentos.Find(empleado.Documentoid);
            if (documento == null) {
                return NotFound();
            }

            Acuerdo acuerdo = context.Acuerdos.Find(contrato.Acuerdoid);

            ObtenerEmpleadoOutputModel empleadoOutputModel = new ObtenerEmpleadoOutputModel
            {
                Id = empleado.Id,
                Cargo = contrato.Cargo,
                Correo = empleado.Correo,
                Direccion = empleado.Direccion,
                FechaInicio = empleado.Contrato.FechaInicio,
                FechaNacimiento = empleado.FechaNacimiento,
                Nombre = empleado.Nombre,
                NumeroDocumento = documento.NumeroDocumento,
                SedeId  = empleado.Sede.Id,
                Telefono = empleado.Telefono,
                TipoContrato = acuerdo.Tipo, 
                TipoDocumento = documento.TipoDocumento
            };
            return Json(empleadoOutputModel);
        }


        [HttpDelete]
        [Route("Eliminar/{id}")]
        public ActionResult Delete(int id)
        {
            Empleado empleado = context.Empleados
                .Include(x => x.Contrato)
                .Include(d => d.Documento)
                .FirstOrDefault(x => x.Id == id);
            if (empleado == null)
            {
                // Manejar el caso cuando no se encuentra el empleado
                return NotFound();
            }
            context.Empleados.Remove(empleado);
            context.Contratos.Remove(empleado.Contrato);
            context.Documentos.Remove(empleado.Documento);
            context.SaveChanges();
            return Json(empleado);
        }

        [HttpPost]
        public IActionResult CrearEmpleado([FromBody] CrearEmpleadoInputModel input)
        {
            // imprimir en console input
            Console.WriteLine(input);

            Sede sede = context.Sedes.Find(input.SedeId);
            Paise pais = context.Paises.Find(sede.Paisid);
            // Acuerdo segun pais.Id y input.TipoContrato
            Acuerdo acuerdo = context.Acuerdos.FirstOrDefault(x => x.Paisid == pais.Id && x.Tipo == input.TipoContrato);
            if (acuerdo == null)
            {
                //retornar 409
                return StatusCode(409);
            }
            Contrato contrato = new Contrato
            {
                FechaInicio = input.FechaInicio,
                Acuerdoid = acuerdo.Id,
                Cargo = input.Cargo
            };

            // save contrato
            Contrato contratoCreado = context.Contratos.Add(contrato).Entity;
            context.SaveChanges();

            Documento documento = new Documento
            {
                TipoDocumento  = input.TipoDocumento,
                NumeroDocumento = input.NumeroDocumento
            };

            // save documento
            Documento documentoCreado = context.Documentos.Add(documento).Entity;
            context.SaveChanges();

            // Crear una nueva instancia de Acuerdo con los datos recibidos
            Empleado empleado = new Empleado
            {
                Nombre = input.Nombre,
                Correo = input.Correo,
                FechaNacimiento = input.FechaNacimiento,
                Telefono = input.Telefono,
                Direccion = input.Direccion,
                Sedeid = input.SedeId,
                Documentoid = documentoCreado.Id,
                Contratoid = contratoCreado.Id,
            };


            // Guardar el acuerdo en la base de datos o realizar otras operaciones necesarias
            Empleado empleadoCreado = context.Empleados.Add(empleado).Entity;
            context.SaveChanges();

            return Json(empleadoCreado); // Devolver el acuerdo como respuesta JSON
        }

        [HttpPut("{id}")]
        [Route("Editar/{id}")]
        public IActionResult EditarEmpleado(int id, [FromBody] EditarEmpleadoInputModel input)
        {
            // imprimir en console input
            Console.WriteLine(input);

            Empleado empleado = context.Empleados
                .Include(x => x.Contrato)
                .Include(d => d.Documento)
                .FirstOrDefault(x => x.Id == id);
            if (empleado == null)
            {
                // Manejar el caso cuando no se encuentra el empleado
                return NotFound();
            }

            Sede sede = context.Sedes.Find(input.SedeId);
            Paise pais = context.Paises.Find(sede.Paisid);
            // Acuerdo segun pais.Id y input.TipoContrato
            Acuerdo acuerdo = context.Acuerdos.FirstOrDefault(x => x.Paisid == pais.Id && x.Tipo == input.TipoContrato);
            if (acuerdo == null)
            {
                //retornar 409
                return StatusCode(409);
            }
            Contrato contrato = context.Contratos.Find(empleado.Contratoid);
            if (contrato == null)
            {
                // Manejar el caso cuando no se encuentra el contrato
                return NotFound();
            }
            contrato.FechaInicio = input.FechaInicio;
            contrato.Cargo = input.Cargo;
            contrato.Acuerdoid = acuerdo.Id;

            // save contrato
            Contrato contratoEditado = context.Contratos.Update(contrato).Entity;
            context.SaveChanges();

            Documento documento = context.Documentos.Find(empleado.Documentoid);
            if (documento == null)
            {
                // Manejar el caso cuando no se encuentra el documento
                return NotFound();
            }
            documento.TipoDocumento  = input.TipoDocumento;
            documento.NumeroDocumento = input.NumeroDocumento;

            // save documento
            Documento documentoEditado = context.Documentos.Update(documento).Entity;
            context.SaveChanges();

            // Crear una nueva instancia de Acuerdo con los datos recibidos
            empleado.Nombre = input.Nombre;
            empleado.Correo = input.Correo;
            empleado.FechaNacimiento = input.FechaNacimiento;
            empleado.Telefono = input.Telefono;
            empleado.Direccion = input.Direccion;
            empleado.Sedeid = input.SedeId;
            empleado.Documentoid = documentoEditado.Id;
            empleado.Contratoid = contratoEditado.Id;

            // Guardar el acuerdo en la base de datos o realizar otras operaciones necesarias
            Empleado empleadoEditado = context.Empleados.Update(empleado).Entity;
            context.SaveChanges();

            return Json(empleadoEditado); // Devolver el acuerdo como respuesta JSON
        }

    }
}
