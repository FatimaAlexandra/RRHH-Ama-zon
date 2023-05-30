using System;
using System.Collections.Generic;

namespace amazon.Models.Inputs;
    public class EditarSedeInputModel
    {
        public string Nombre { get; set; } = null!;
        public string Logo { get; set; } = null!;

        public string Codigosede { get; set; } = null!;
        public int Paisid { get; set; }
    }
