using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace amazon.Models;

public partial class Contrato
{
    public int Id { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime? FechaFin { get; set; }

    public string Cargo { get; set; } = null!;

    public int Acuerdoid { get; set; }
    [JsonIgnore]
    public virtual Acuerdo Acuerdo { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
