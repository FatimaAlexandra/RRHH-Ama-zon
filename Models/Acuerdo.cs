using System;
using System.Collections.Generic;

namespace amazon.Models;

public partial class Acuerdo
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Contenido { get; set; } = null!;

    public string Tipo { get; set; } = null!;

    public int IdPais { get; set; }

    public int IdContrato { get; set; }

    public virtual Contrato Contrato { get; set; } = null!;

    public virtual Pai Pais { get; set; } = null!;
}
