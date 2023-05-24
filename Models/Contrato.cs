using System;
using System.Collections.Generic;

namespace amazon.Models;

public partial class Contrato
{
    public int Id { get; set; }

    public string FechaInicio { get; set; } = null!;

    public string FechaFin { get; set; } = null!;

    public string Tipo { get; set; } = null!;

    public string IdEmpleado { get; set; } = null!;

    public string Cargo { get; set; } = null!;

    public virtual ICollection<Acuerdo> Acuerdos { get; set; } = new List<Acuerdo>();

    public virtual Empleado Empleado { get; set; } = null!;
}
