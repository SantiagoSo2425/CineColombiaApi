using System.Collections.Generic;
using System.Linq;
using CineColombiaApi.Models;

namespace apiCine.Clases;

public class clsOpeVenta
{
    private readonly CineColombiaContext oCine;
    public Ventum tblVenta { get; set; }

    public clsOpeVenta(CineColombiaContext oCine)
    {
        this.oCine = oCine;
        tblVenta = new Ventum();
    }

    public List<Ventum> ListarVentas()
    {
        return oCine.Venta.ToList();
    }

    public IQueryable ConsultarVenta(int idVenta)
    {
        return from x in oCine.Venta
               where x.IdVenta == idVenta
               select x;
    }

    public IQueryable ConsultarDetalleVenta(int idVenta)
    {
        return from b in oCine.Boleticas
               where b.IdVenta == idVenta
               select b;
    }

    public int Agregar()
    {
        oCine.Venta.Add(tblVenta);
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }

    public int Modificar()
    {
        var venta = (from x in oCine.Venta
                     where x.IdVenta == tblVenta.IdVenta
                     select x).FirstOrDefault();

        if (venta == null)
        {
            return -2;
        }

        venta.IdCliente = tblVenta.IdCliente;
        venta.IdEmpleado = tblVenta.IdEmpleado;
        venta.IdMetodoPago = tblVenta.IdMetodoPago;
        venta.FechaHora = tblVenta.FechaHora;
        venta.Subtotal = tblVenta.Subtotal;
        venta.TotalDescuento = tblVenta.TotalDescuento;
        venta.TotalVenta = tblVenta.TotalVenta;
        venta.Estado = tblVenta.Estado;

        return oCine.SaveChanges() > 0 ? 1 : 0;
    }

    public int Inactivar(int idVenta)
    {
        var venta = (from x in oCine.Venta
                     where x.IdVenta == idVenta
                     select x).FirstOrDefault();

        if (venta == null)
        {
            return -2;
        }

        venta.Estado = false;
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }
}
