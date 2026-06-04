namespace CineColombiaApi.Models;

public class RegistroVentaDto
{
    public int? IdCliente { get; set; }
    public int IdEmpleado { get; set; }
    public int IdMetodoPago { get; set; }
    public DateTime FechaHora { get; set; }
    public decimal Subtotal { get; set; }
    public decimal TotalDescuento { get; set; }
    public decimal TotalVenta { get; set; }
    public List<BoleticaVentaDto> Boleticas { get; set; } = new();
}

public class BoleticaVentaDto
{
    public int IdFuncion { get; set; }
    public List<SillaVentaDto> Sillas { get; set; } = new();
}

public class SillaVentaDto
{
    public int IdSilla { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Descuento { get; set; }
    public decimal PrecioFinal { get; set; }
}
