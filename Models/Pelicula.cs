using System;
using System.Collections.Generic;

namespace CineColombiaApi.Models;

public partial class Pelicula
{
    public int IdPelicula { get; set; }

    public int IdGenero { get; set; }

    public int IdClasificacion { get; set; }

    public string TituloOriginal { get; set; } = null!;

    public string NombreOferta { get; set; } = null!;

    public string Resumen { get; set; } = null!;

    public DateOnly AnioEstreno { get; set; }

    public string? TrailerLink { get; set; }

    public int DuracionMin { get; set; }

    public int RegistradoPor { get; set; }

    public DateTime FechaRegistro { get; set; }
}
