using System;
using System.Collections.Generic;

namespace CineColombiaApi.Models;

public partial class Genero
{
    public int IdGenero { get; set; }

    public string Nombre { get; set; } = null!;

   // public virtual ICollection<Pelicula> Peliculas { get; set; } = new List<Pelicula>();
}
