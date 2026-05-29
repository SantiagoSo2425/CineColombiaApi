using System.Collections.Generic;
using System.Linq;
using CineColombiaApi.Models;

namespace apiCine.Clases;

public class clsOpeTarjetaFidelizacion
{
    private readonly CineColombiaContext oCine;
    public TarjetaFidelizacion tblTarjetaFidelizacion { get; set; }

    public clsOpeTarjetaFidelizacion(CineColombiaContext oCine)
    {
        this.oCine = oCine;
        tblTarjetaFidelizacion = new TarjetaFidelizacion();
    }

    public List<TarjetaFidelizacion> ListarTarjetasFidelizacion()
    {
        return oCine.TarjetaFidelizacions.ToList();
    }

    public IQueryable ConsultarTarjetaFidelizacion(int idTarjeta)
    {
        return from x in oCine.TarjetaFidelizacions
               where x.IdTarjeta == idTarjeta
               select x;
    }

    public int Agregar()
    {
        var existe = (from x in oCine.TarjetaFidelizacions
                      where x.IdCliente == tblTarjetaFidelizacion.IdCliente
                      select x).Any();

        if (existe)
        {
            return -1;
        }

        oCine.TarjetaFidelizacions.Add(tblTarjetaFidelizacion);
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }

    public int Modificar()
    {
        var tarjeta = (from x in oCine.TarjetaFidelizacions
                       where x.IdTarjeta == tblTarjetaFidelizacion.IdTarjeta
                       select x).FirstOrDefault();

        if (tarjeta == null)
        {
            return -2;
        }

        var existe = (from x in oCine.TarjetaFidelizacions
                      where x.IdTarjeta != tblTarjetaFidelizacion.IdTarjeta
                      && x.IdCliente == tblTarjetaFidelizacion.IdCliente
                      select x).Any();

        if (existe)
        {
            return -1;
        }

        tarjeta.IdCliente = tblTarjetaFidelizacion.IdCliente;
        tarjeta.NumeroTarjeta = tblTarjetaFidelizacion.NumeroTarjeta;
        tarjeta.FechaEmision = tblTarjetaFidelizacion.FechaEmision;
        tarjeta.FechaVencimiento = tblTarjetaFidelizacion.FechaVencimiento;
        tarjeta.PuntosAcumulados = tblTarjetaFidelizacion.PuntosAcumulados;
        tarjeta.DescuentoPorcentaje = tblTarjetaFidelizacion.DescuentoPorcentaje;
        tarjeta.Estado = tblTarjetaFidelizacion.Estado;
        tarjeta.RegistradoPor = tblTarjetaFidelizacion.RegistradoPor;
        tarjeta.FechaRegistro = tblTarjetaFidelizacion.FechaRegistro;
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }

    public int Inactivar(int idTarjeta)
    {
        var tarjeta = (from x in oCine.TarjetaFidelizacions
                       where x.IdTarjeta == idTarjeta
                       select x).FirstOrDefault();

        if (tarjeta == null)
        {
            return -2;
        }

        tarjeta.Estado = false;
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }
}
