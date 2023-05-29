using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace amazon.Models;

public partial class Sede
{
    public int Id { get; set; }

    public string Logo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Codigosede { get; set; } = null!;

    public int Paisid { get; set; }
    [JsonIgnore]
    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
    [JsonIgnore]
    public virtual Paise Pais { get; set; } = null!;
}
