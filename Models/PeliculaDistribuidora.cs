using System;
using System.Collections.Generic;

namespace CineColombiaApi.Models;

public partial class PeliculaDistribuidora
{
    public int IdPeliculaDistribuidora { get; set; }

    public int IdPelicula { get; set; }

    public int IdDistribuidora { get; set; }
}
