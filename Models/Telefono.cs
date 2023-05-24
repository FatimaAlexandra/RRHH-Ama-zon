using System;
using System.Collections.Generic;

namespace amazon.Models;

public partial class Telefono
{
    public int Id { get; set; }

    public string Numero { get; set; } = null!;

    public int IdSede { get; set; }

    public string IdEmpleado { get; set; } = null!;

    public virtual Empleado Empleado { get; set; } = null!;

    public virtual Sede Sede { get; set; } = null!;
}
