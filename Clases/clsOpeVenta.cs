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
        if (tblVenta.IdVenta == 0)
        {
            var maxId = oCine.Venta.Max(v => (int?)v.IdVenta) ?? 0;
            tblVenta.IdVenta = maxId + 1;
        }
        oCine.Venta.Add(tblVenta);
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }

    public int RegistrarVentaCompleta(RegistroVentaDto dto)
    {
        if (dto.Boleticas == null || dto.Boleticas.Count == 0)
            return -3;

        var maxIdVenta = oCine.Venta.Max(v => (int?)v.IdVenta) ?? 0;
        var maxIdBoletica = oCine.Boleticas.Max(b => (int?)b.IdBoletica) ?? 0;
        var maxIdBoleticaSilla = oCine.BoleticaSillas.Max(bs => (int?)bs.IdBoleticaSilla) ?? 0;

        var venta = new Ventum
        {
            IdVenta = maxIdVenta + 1,
            IdCliente = dto.IdCliente,
            IdEmpleado = dto.IdEmpleado,
            IdMetodoPago = dto.IdMetodoPago,
            FechaHora = dto.FechaHora,
            Subtotal = dto.Subtotal,
            TotalDescuento = dto.TotalDescuento,
            TotalVenta = dto.TotalVenta,
            Estado = true
        };

        oCine.Venta.Add(venta);

        var funcionesIds = dto.Boleticas.Select(b => b.IdFuncion).Distinct().ToList();

        var sillasOcupadasPorFuncion = new Dictionary<int, HashSet<int>>();
        var sillasEnRequest = new Dictionary<int, HashSet<int>>();

        foreach (var idFuncion in funcionesIds)
        {
            sillasOcupadasPorFuncion[idFuncion] = (from bs in oCine.BoleticaSillas
                                                   join b in oCine.Boleticas on bs.IdBoletica equals b.IdBoletica
                                                   where b.IdFuncion == idFuncion
                                                   select bs.IdSilla).ToHashSet();
            sillasEnRequest[idFuncion] = new HashSet<int>();
        }

        int idBoleticaCounter = maxIdBoletica;

        foreach (var boleticaDto in dto.Boleticas)
        {
            idBoleticaCounter++;
            var boletica = new Boletica
            {
                IdBoletica = idBoleticaCounter,
                IdVenta = venta.IdVenta,
                IdFuncion = boleticaDto.IdFuncion,
                Estado = 1
            };

            oCine.Boleticas.Add(boletica);

            if (boleticaDto.Sillas == null || boleticaDto.Sillas.Count == 0)
                continue;

            foreach (var sillaDto in boleticaDto.Sillas)
            {
                if (sillasOcupadasPorFuncion[boleticaDto.IdFuncion].Contains(sillaDto.IdSilla)
                    || sillasEnRequest[boleticaDto.IdFuncion].Contains(sillaDto.IdSilla))
                    return -1;

                sillasEnRequest[boleticaDto.IdFuncion].Add(sillaDto.IdSilla);

                maxIdBoleticaSilla++;
                var boleticaSilla = new BoleticaSilla
                {
                    IdBoleticaSilla = maxIdBoleticaSilla,
                    IdBoletica = boletica.IdBoletica,
                    IdSilla = sillaDto.IdSilla,
                    PrecioUnitario = sillaDto.PrecioUnitario,
                    Descuento = sillaDto.Descuento,
                    PrecioFinal = sillaDto.PrecioFinal,
                    Estado = 1
                };

                oCine.BoleticaSillas.Add(boleticaSilla);
            }
        }

        return oCine.SaveChanges() > 0 ? venta.IdVenta : 0;
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
