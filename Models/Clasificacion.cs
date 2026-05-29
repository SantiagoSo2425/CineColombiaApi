using System;
using System.Collections.Generic;

namespace CineColombiaApi.Models;

public partial class Clasificacion
{
    public int IdClasificacion { get; set; }

    public string Codigo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;
}
