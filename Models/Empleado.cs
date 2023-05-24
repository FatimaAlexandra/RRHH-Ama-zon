using System;
using System.Collections.Generic;

namespace amazon.Models;

public partial class Empleado
{
    public string Id { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string FechaNacimiento { get; set; } = null!;

    public virtual ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();

    public virtual ICollection<Telefono> Telefonos { get; set; } = new List<Telefono>();
}
