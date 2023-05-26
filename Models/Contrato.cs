using System;
using System.Collections.Generic;

namespace amazon.Models;

public partial class Contrato
{
    public int Id { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime FechaFin { get; set; }

    public string Cargo { get; set; } = null!;

    public int Acuerdoid { get; set; }

    public virtual Acuerdo Acuerdo { get; set; } = null!;

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
