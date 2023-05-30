using System;
using System.Collections.Generic;

namespace amazon.Models;

public partial class Empleado
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public DateTime FechaNacimiento { get; set; }

    public string Telefono { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public int Sedeid { get; set; }

    public int Contratoid { get; set; }

    public int Documentoid { get; set; }

    public virtual Contrato Contrato { get; set; } = null!;

    public virtual Documento Documento { get; set; } = null!;

    public virtual Sede Sede { get; set; } = null!;
}
