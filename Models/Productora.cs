using System;
using System.Collections.Generic;

namespace CineColombiaApi.Models;

public partial class Productora
{
    public int IdProductora { get; set; }

    public string Nombre { get; set; } = null!;

    public int? IdPais { get; set; }
}
