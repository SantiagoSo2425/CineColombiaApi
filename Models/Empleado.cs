using System;
using System.Collections.Generic;

namespace CineColombiaApi.Models;

public partial class Empleado
{
    public int IdEmpleado { get; set; }

    public string CodigoEmpleado { get; set; } = null!;

    public int IdTeatro { get; set; }

    public int IdTipoDoc { get; set; }

    public string NumDocumento { get; set; } = null!;

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public DateOnly FechaIngreso { get; set; }

    public bool Activo { get; set; }

    public int? RegistradoPor { get; set; }

    public DateTime FechaRegistro { get; set; }
}
