using System.Collections.Generic;
using System.Linq;
using CineColombiaApi.Models;

namespace apiCine.Clases;

public class clsOpeBoleticaSilla
{
    private readonly CineColombiaContext oCine;
    public BoleticaSilla tblBoleticaSilla { get; set; }

    public clsOpeBoleticaSilla(CineColombiaContext oCine)
    {
        this.oCine = oCine;
        tblBoleticaSilla = new BoleticaSilla();
    }

    public List<BoleticaSilla> ListarBoleticaSillas()
    {
        return oCine.BoleticaSillas.ToList();
    }

    public IQueryable ConsultarBoleticaSilla(int idBoleticaSilla)
    {
        return from x in oCine.BoleticaSillas
               where x.IdBoleticaSilla == idBoleticaSilla
               select x;
    }

    public int Agregar()
    {
        var idFuncion = (from b in oCine.Boleticas
                         where b.IdBoletica == tblBoleticaSilla.IdBoletica
                         select b.IdFuncion).FirstOrDefault();

        if (idFuncion == 0)
        {
            return -2;
        }

        var existe = (from bs in oCine.BoleticaSillas
                      join b in oCine.Boleticas on bs.IdBoletica equals b.IdBoletica
                      where b.IdFuncion == idFuncion
                      && bs.IdSilla == tblBoleticaSilla.IdSilla
                      select bs).Any();

        if (existe)
        {
            return -1;
        }

        oCine.BoleticaSillas.Add(tblBoleticaSilla);
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }

    public int Modificar()
    {
        var boleticaSilla = (from x in oCine.BoleticaSillas
                             where x.IdBoleticaSilla == tblBoleticaSilla.IdBoleticaSilla
                             select x).FirstOrDefault();

        if (boleticaSilla == null)
        {
            return -2;
        }

        var idFuncion = (from b in oCine.Boleticas
                         where b.IdBoletica == tblBoleticaSilla.IdBoletica
                         select b.IdFuncion).FirstOrDefault();

        if (idFuncion == 0)
        {
            return -2;
        }

        var existe = (from bs in oCine.BoleticaSillas
                      join b in oCine.Boleticas on bs.IdBoletica equals b.IdBoletica
                      where bs.IdBoleticaSilla != tblBoleticaSilla.IdBoleticaSilla
                      && b.IdFuncion == idFuncion
                      && bs.IdSilla == tblBoleticaSilla.IdSilla
                      select bs).Any();

        if (existe)
        {
            return -1;
        }

        boleticaSilla.IdBoletica = tblBoleticaSilla.IdBoletica;
        boleticaSilla.IdSilla = tblBoleticaSilla.IdSilla;
        boleticaSilla.PrecioUnitario = tblBoleticaSilla.PrecioUnitario;
        boleticaSilla.Descuento = tblBoleticaSilla.Descuento;
        boleticaSilla.PrecioFinal = tblBoleticaSilla.PrecioFinal;
        boleticaSilla.Estado = tblBoleticaSilla.Estado;

        return oCine.SaveChanges() > 0 ? 1 : 0;
    }

    public int Inactivar(int idBoleticaSilla)
    {
        var boleticaSilla = (from x in oCine.BoleticaSillas
                             where x.IdBoleticaSilla == idBoleticaSilla
                             select x).FirstOrDefault();

        if (boleticaSilla == null)
        {
            return -2;
        }

        boleticaSilla.Estado = 0;
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }
}
