using Microsoft.AspNetCore.Mvc;
using amazon.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using amazon.Models.Inputs;
using amazon.Models.Outputs;
using iText.Html2pdf;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Text;
using amazon.Services;

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

        [HttpPut("{id}")]
        [Route("Despedir/{id}")]
        public IActionResult DespedirEmpleado(int id)
        {
            Empleado empleado = context.Empleados
                .Include(x => x.Contrato)
                .FirstOrDefault(x => x.Id == id);
            if (empleado == null)
            {
                // Manejar el caso cuando no se encuentra el empleado
                return NotFound();
            }

            Contrato contrato = context.Contratos.Find(empleado.Contratoid);
            if (contrato == null)
            {
                // Manejar el caso cuando no se encuentra el contrato
                return NotFound();
            }
            contrato.FechaFin = DateTime.Now;


            // save contrato
            Contrato contratoEditado = context.Contratos.Update(contrato).Entity;
            context.SaveChanges();

            return Json(empleado); // Devolver el acuerdo como respuesta JSON
        }

        [HttpPut("{id}")]
        [Route("EnviarEmail/{id}")]
        public IActionResult EnviarContrato(int id)
        {
            SendEmail EnviarEmail = new SendEmail();
            var message = EnviarEmail.GenerarPdf(id);
            EnviarEmail.EnviarEmail(message);

            // return statusode 200
            return Ok();
        }

        [HttpPost("{id}")]
        [Route("DescargarPdfEmpleado/{id}")]
        public IActionResult DescargarPdfEmpleado(int id)
        {
            Empleado empleado = context.Empleados
                .FirstOrDefault(x => x.Id == id);


            if (empleado == null)
            {
                // Manejar el caso cuando no se encuentra el empleado
                return NotFound();
            }

            Contrato contrato = context.Contratos.Find(empleado.Contratoid);
            Documento documento = context.Documentos.Find(empleado.Documentoid);
            Sede sede = context.Sedes.Find(empleado.Sedeid);
            Acuerdo acuerdo = context.Acuerdos.Find(contrato.Acuerdoid);
            Paise pais = context.Paises.Find(sede.Paisid);

            EmpleadoPdf empleadoPdf = new EmpleadoPdf();
            empleadoPdf.empleado = empleado;
            empleadoPdf.contrato = contrato;
            empleadoPdf.documento = documento;
            empleadoPdf.sede = sede;
            empleadoPdf.acuerdo = acuerdo;


            string cssStyle = "html, body {width: 100%;height: 842px; width: 595px; margin-left: auto; margin-right: auto;} body {margin: 20px;}img { width: 100%; } p, h1, h2, h3, h4, h5, h6, span, b, strong, li { width: 100%; word-break: normal; } p, li { text-align: justify;}";

            string htmlContent = "<!DOCTYPE html><html lang=\"en\"><head><meta charset=\"UTF-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"><title>PDF</title></head><body>";
            htmlContent = "<style>" + cssStyle + "</style>";
            htmlContent = htmlContent + acuerdo.Contenido + "</body></html>";

            htmlContent = htmlContent.Replace("&nbsp;", " ");
            
            // remplazar variables: nombre, correo, fecha_nacimiento, telefono, direccion, tipo_documento, numero_documento, fecha_inicio, cargo, tipo_contrato, fecha_fin, sede, pais
            htmlContent = htmlContent.Replace("{{nombre}}", empleado.Nombre);
            htmlContent = htmlContent.Replace("{{correo}}", empleado.Correo);
            htmlContent = htmlContent.Replace("{{fecha_nacimiento}}", empleado.FechaNacimiento.ToString("dd/MM/yyyy"));
            htmlContent = htmlContent.Replace("{{telefono}}", empleado.Telefono);
            htmlContent = htmlContent.Replace("{{direccion}}", empleado.Direccion);
            htmlContent = htmlContent.Replace("{{tipo_documento}}", documento.TipoDocumento);
            htmlContent = htmlContent.Replace("{{numero_documento}}", documento.NumeroDocumento);
            htmlContent = htmlContent.Replace("{{fecha_inicio}}", contrato.FechaInicio.ToString("dd/MM/yyyy"));
            htmlContent = htmlContent.Replace("{{cargo}}", contrato.Cargo);
            htmlContent = htmlContent.Replace("{{tipo_contrato}}", acuerdo.Tipo);
            if (contrato.FechaFin is DateTime fechaFin) {
                htmlContent = htmlContent.Replace("{{fecha_fin}}", fechaFin.ToString("dd/MM/yyyy"));
            }
            htmlContent = htmlContent.Replace("{{sede}}", sede.Nombre);
            htmlContent = htmlContent.Replace("{{pais}}", pais.Nombre);

            byte[] fileContents = Encoding.UTF8.GetBytes(htmlContent);
            
            string fileName = "htmls/empleado-" + id + ".html" ; 

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            System.IO.File.WriteAllBytes(filePath, fileContents);


            // Crear el convertidor
            ConverterProperties converterProperties = new ConverterProperties();

            MemoryStream pdfStream = new MemoryStream();
            HtmlConverter.ConvertToPdf(new FileStream(filePath, FileMode.Open), pdfStream, converterProperties);
            
            Response.Headers.Add("Content-Disposition", "attachment; filename=empleado-"+id+"-.pdf");
            Response.ContentType = "application/pdf";
            
            return File(pdfStream.ToArray(), "application/pdf");
        }

    }
}
