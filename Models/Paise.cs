using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace amazon.Models;

public partial class Paise
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Isocode { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Acuerdo> Acuerdos { get; set; } = new List<Acuerdo>();
    [JsonIgnore]
    public virtual ICollection<Sede> Sedes { get; set; } = new List<Sede>();
}
