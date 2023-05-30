using System;
using System.Collections.Generic;

namespace amazon.Models;

public partial class Sede
{
    public int Id { get; set; } = 0;

    public string Logo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Codigosede { get; set; } = null!;

    public int Paisid { get; set; }

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();

    public virtual Paise Pais { get; set; } = null!;
}
