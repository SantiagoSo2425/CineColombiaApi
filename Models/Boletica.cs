using System;
using System.Collections.Generic;

namespace CineColombiaApi.Models;

public partial class Boletica
{
    public int IdBoletica { get; set; }

    public int IdVenta { get; set; }

    public int IdFuncion { get; set; }

    public int Estado { get; set; }
}
