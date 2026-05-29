using System;
using System.Collections.Generic;

namespace CineColombiaApi.Models;

public partial class Idioma
{
    public int IdIdioma { get; set; }

    public string Nombre { get; set; } = null!;

    public string Codigo { get; set; } = null!;
}
