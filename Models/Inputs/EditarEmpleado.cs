using System;
using System.Collections.Generic;

namespace amazon.Models.Inputs;
    public class EditarEmpleadoInputModel
    {
        public string Nombre { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public string Correo { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public int SedeId { get; set; }
        public string TipoDocumento { get; set; } = null!;
        public string NumeroDocumento { get; set; } = null!;
        public string TipoContrato { get; set; } = null!;
        public string Cargo { get; set; } = null!;
        public DateTime FechaInicio { get; set; }
        public string? Salario { get; set; }
    }
