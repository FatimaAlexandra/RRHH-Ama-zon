using System;
using System.Collections.Generic;
using amazon.Models;

namespace amazon.Models.Outputs;
    public class EmpleadoPdf
    {
        public Empleado empleado { get; set; } = null!;
        public Contrato contrato { get; set; } = null!;
        public Acuerdo acuerdo { get; set; } = null!;
        public Documento documento { get; set; } = null!;
        public Sede sede { get; set; } = null!;
        public Paise pais { get; set; } = null!;
        
    }
