using System;
using System.Collections.Generic;

namespace CineColombiaApi.Models;

public partial class TipoSala
{
    public int IdTipoSala { get; set; }

    public string Nombre { get; set; } = null!;
}
