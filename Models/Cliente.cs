using System;
using System.Collections.Generic;

namespace CineColombiaApi.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public int IdTipoCliente { get; set; }

    public int IdTipoDoc { get; set; }

    public string NumDocumento { get; set; } = null!;

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string? Email { get; set; }

    public bool Activo { get; set; }

    public int RegistradoPor { get; set; }

    public DateTime FechaRegistro { get; set; }

}
