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

    public int RegistradoPor { get; set; }

    public DateTime FechaRegistro { get; set; }

}
