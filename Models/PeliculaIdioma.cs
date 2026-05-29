using System;
using System.Collections.Generic;

namespace CineColombiaApi.Models;

public partial class PeliculaIdioma
{
    public int IdPeliculaIdioma { get; set; }

    public int IdPelicula { get; set; }

    public int IdIdioma { get; set; }

    public bool EsOriginal { get; set; }
}
