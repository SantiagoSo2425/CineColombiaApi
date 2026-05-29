using System;
using System.Collections.Generic;

namespace CineColombiaApi.Models;

public partial class PeliculaFormato
{
    public int IdPeliculaFormato { get; set; }

    public int IdPelicula { get; set; }

    public int IdFormato { get; set; }
}
