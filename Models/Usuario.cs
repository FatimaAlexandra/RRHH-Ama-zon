using System;
using System.Collections.Generic;

namespace amazon.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Usuario1 { get; set; } = null!;

    public string Pass { get; set; } = null!;

    public int Rol { get; set; }
}
