using System;
using System.Collections.Generic;

namespace amazon.Models;

public partial class Sede
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Codigosede { get; set; } = null!;

    public int IdPais { get; set; }

    public virtual Pai IdPaisNavigation { get; set; } = null!;

    public virtual ICollection<Telefono> Telefonos { get; set; } = new List<Telefono>();
}
