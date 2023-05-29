using System;
using System.Collections.Generic;

namespace amazon.Models.Inputs;
    public class CrearAcuerdoInputModel
    {
        public string Nombre { get; set; }
        public string Contenido { get; set; }
        public string Tipo { get; set; }
        public int PaisId { get; set; }
    }
