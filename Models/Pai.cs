using System;
using System.Collections.Generic;

namespace amazon.Models;

public partial class Pai
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Idioma { get; set; } = null!;

    public virtual ICollection<Acuerdo> Acuerdos { get; set; } = new List<Acuerdo>();

    public virtual ICollection<Sede> Sedes { get; set; } = new List<Sede>();
}
