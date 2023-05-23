using System;
using System.Collections.Generic;

namespace amazon.Models;

public partial class Acuerdo
{
    public string Nombre { get; set; } = null!;

    public string Contenido { get; set; } = null!;

    public string Tipo { get; set; } = null!;

    public int Acuerdoid { get; set; }

    public int Paisid { get; set; }

    public int Pivoteid { get; set; }

    public int Contratoid { get; set; }

    public virtual Contrato Contrato { get; set; } = null!;

    public virtual Pai Pais { get; set; } = null!;
}
