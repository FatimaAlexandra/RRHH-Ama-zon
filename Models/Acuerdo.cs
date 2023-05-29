using System;
using System.Collections.Generic;

namespace amazon.Models;

public partial class Acuerdo
{
    public int Id { get; set; }

    public string Contenido { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Tipo { get; set; } = null!;

    public int Paisid { get; set; }

    public virtual ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();

    public virtual Paise Pais { get; set; } = null!;
}
