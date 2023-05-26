using System;
using System.Collections.Generic;

namespace amazon.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Usuario1 { get; set; } = null!;

    public int Rol { get; set; }

    public string Clave { get; set; } = null!;
}
