using System.Collections.Generic;
using System.Linq;
using apiSIA.Models;
using CineColombiaApi.Models;

namespace apiCine.Clases;

public class clsOpeMetodoPago
{
    private readonly CineColombiaContext oCine;
    public MetodoPago tblMetodoPago { get; set; }

    public clsOpeMetodoPago(CineColombiaContext oCine)
    {
        this.oCine = oCine;
        tblMetodoPago = new MetodoPago();
    }

    public List<MetodoPago> ListarMetodosPago()
    {
        return oCine.MetodoPagos.ToList();
    }

    public IQueryable ConsultarMetodoPago(int idMetodoPago)
    {
        return from x in oCine.MetodoPagos
               where x.IdMetodoPago == idMetodoPago
               select x;
    }

    public int Agregar()
    {
        var existe = (from x in oCine.MetodoPagos
                      where x.IdMetodoPago == tblMetodoPago.IdMetodoPago
                      select x).Any();

        if (existe)
        {
            return -1;
        }

        oCine.MetodoPagos.Add(tblMetodoPago);
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }

    public int Modificar()
    {
        var metodoPago = (from x in oCine.MetodoPagos
                          where x.IdMetodoPago == tblMetodoPago.IdMetodoPago
                          select x).FirstOrDefault();

        if (metodoPago == null)
        {
            return -2;
        }

        metodoPago.Nombre = tblMetodoPago.Nombre;

        return oCine.SaveChanges() > 0 ? 1 : 0;
    }
}
