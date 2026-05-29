using System;
using System.Collections.Generic;

namespace CineColombiaApi.Models;

public partial class Pai
{
    public int IdPais { get; set; }

    public string Nombre { get; set; } = null!;

    public string Codigo { get; set; } = null!;
}
