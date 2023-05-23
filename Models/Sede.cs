using System;
using System.Collections.Generic;

namespace amazon.Models;

public partial class Sede
{
    public string Nombre { get; set; } = null!;

    public int Sedeid { get; set; }

    public string Codigosede { get; set; } = null!;

    public int Paisid { get; set; }

    public virtual Pai Pais { get; set; } = null!;

    public virtual ICollection<Telefono> Telefonos { get; set; } = new List<Telefono>();
}
