using System;
using System.Collections.Generic;

namespace amazon.Models;

public partial class Telefono
{
    public int Telefonoid { get; set; }

    public string Numero { get; set; } = null!;

    public int Sedeid { get; set; }

    public int Empleadoid { get; set; }

    public virtual Empleado Empleado { get; set; } = null!;

    public virtual Sede Sede { get; set; } = null!;
}
