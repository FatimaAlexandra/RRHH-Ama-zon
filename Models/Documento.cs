using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace amazon.Models;

public partial class Documento
{
    public int Id { get; set; }

    public string TipoDocumento { get; set; } = null!;

    public string NumeroDocumento { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
