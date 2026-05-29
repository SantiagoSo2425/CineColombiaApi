using System;
using System.Collections.Generic;

namespace CineColombiaApi.Models;

public partial class Ventum
{
    public int IdVenta { get; set; }

    public int? IdCliente { get; set; }

    public int IdEmpleado { get; set; }

    public int IdMetodoPago { get; set; }

    public DateTime FechaHora { get; set; }

    public decimal Subtotal { get; set; }

    public decimal TotalDescuento { get; set; }

    public decimal TotalVenta { get; set; }

    public bool Estado { get; set; }

}
