using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace amazon.Models;

public partial class Empleado
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public DateTime FechaNacimiento { get; set; }

    public string Telefono { get; set; } = null!;

    public string? Salario { get; set; }

    public string Direccion { get; set; } = null!;

    public int Sedeid { get; set; }

    public int Contratoid { get; set; }

    public int Documentoid { get; set; }
    [JsonIgnore]
    public virtual Contrato Contrato { get; set; } = null!;
    [JsonIgnore]
    public virtual Documento Documento { get; set; } = null!;
    [JsonIgnore]
    public virtual Sede Sede { get; set; } = null!;
}
