using System;
using System.Collections.Generic;

namespace CineColombiaApi.Models;

public partial class BoleticaSilla
{
    public int IdBoleticaSilla { get; set; }

    public int IdBoletica { get; set; }

    public int IdSilla { get; set; }

    public decimal PrecioUnitario { get; set; }

    public decimal Descuento { get; set; }

    public decimal PrecioFinal { get; set; }

    public int Estado { get; set; }

}
