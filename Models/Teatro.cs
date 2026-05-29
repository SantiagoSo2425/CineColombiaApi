using System;
using System.Collections.Generic;

namespace CineColombiaApi.Models;

public partial class Teatro
{
    public int IdTeatro { get; set; }

    public int IdCiudad { get; set; }

    public string Nombre { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Email { get; set; }

    public bool Activo { get; set; }

    public int? RegistradoPor { get; set; }

    public DateTime FechaRegistro { get; set; }
}
