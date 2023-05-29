using System;
using System.Collections.Generic;

namespace amazon.Models.Outputs;
    public class ObtenerEmpleadoOutputModel
    {
        //   Nombre, FechaNacimiento, Correo, Telefono, Direccion, SedeId, TipoDocumento, NumeroDocumento, tipoContrato, Cargo, FechaInicio
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
        public int Id { get; set; }
        
    }
