using System;
using System.Collections.Generic;

namespace CineColombiaApi.Models;

public partial class Sala
{
    public int IdSala { get; set; }

    public int IdTeatro { get; set; }

    public int IdTipoSala { get; set; }

    public string NombreSala { get; set; } = null!;

    public int CapacidadTotal { get; set; }

    public bool Activo { get; set; }

    public int RegistradoPor { get; set; }

    public DateTime FechaRegistro { get; set; }
}
