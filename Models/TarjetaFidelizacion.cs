using System;
using System.Collections.Generic;

namespace CineColombiaApi.Models;

public partial class TarjetaFidelizacion
{
    public int IdTarjeta { get; set; }

    public int IdCliente { get; set; }

    public string NumeroTarjeta { get; set; } = null!;

    public DateOnly FechaEmision { get; set; }

    public DateOnly FechaVencimiento { get; set; }

    public decimal PuntosAcumulados { get; set; }

    public decimal DescuentoPorcentaje { get; set; }

    public bool Estado { get; set; }

    public int RegistradoPor { get; set; }

    public DateTime FechaRegistro { get; set; }
}
