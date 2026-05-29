using System;
using System.Collections.Generic;

namespace CineColombiaApi.Models;

public partial class PeliculaProductora
{
    public int IdPeliculaProductora { get; set; }

    public int IdPelicula { get; set; }

    public int IdProductora { get; set; }
}
