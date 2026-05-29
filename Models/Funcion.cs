using System;
using System.Collections.Generic;

namespace CineColombiaApi.Models;

public partial class Funcion
{
    public int IdFuncion { get; set; }

    public int IdSala { get; set; }

    public int IdPelicula { get; set; }

    public int IdIdioma { get; set; }

    public int IdFormato { get; set; }

    public DateOnly FechaFuncion { get; set; }

    public DateTime HoraInicio { get; set; }

    public DateTime HoraFin { get; set; }

    public decimal PrecioBase { get; set; }

    public bool Estado { get; set; }

   // public virtual ICollection<Boletica> Boleticas { get; set; } = new List<Boletica>();

   // public virtual Formato IdFormatoNavigation { get; set; } = null!;

    //public virtual Idioma IdIdiomaNavigation { get; set; } = null!;

   // public virtual Pelicula IdPeliculaNavigation { get; set; } = null!;

    //public virtual Sala IdSalaNavigation { get; set; } = null!;
}
