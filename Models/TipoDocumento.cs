using System;
using System.Collections.Generic;

namespace CineColombiaApi.Models;

public partial class TipoDocumento
{
    public int IdTipoDoc { get; set; }

    public string Codigo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;
}
