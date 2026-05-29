using System;
using System.Collections.Generic;

namespace CineColombiaApi.Models;

public partial class Silla
{
    public int IdSilla { get; set; }

    public int IdSala { get; set; }

    public int IdTipoSilla { get; set; }

    public string Fila { get; set; } = null!;

    public int Numero { get; set; }

    public int Estado { get; set; }

    public int RegistradoPor { get; set; }

    public DateTime FechaRegistro { get; set; }
}
