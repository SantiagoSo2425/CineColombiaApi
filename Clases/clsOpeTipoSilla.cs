using System.Collections.Generic;
using System.Linq;
using CineColombiaApi.Models;

namespace apiCine.Clases;

public class clsOpeTipoSilla
{
    private readonly CineColombiaContext oCine;
    public TipoSilla tblTipoSilla { get; set; }

    public clsOpeTipoSilla(CineColombiaContext oCine)
    {
        this.oCine = oCine;
        tblTipoSilla = new TipoSilla();
    }

    public List<TipoSilla> ListarTipoSillas()
    {
        return oCine.TipoSillas.ToList();
    }

    public IQueryable ConsultarTipoSilla(int idTipoSilla)
    {
        return from x in oCine.TipoSillas
               where x.IdTipoSilla == idTipoSilla
               select x;
    }

    public int Agregar()
    {
        var existe = (from x in oCine.TipoSillas
                      where x.IdTipoSilla == tblTipoSilla.IdTipoSilla
                      select x).Any();

        if (existe)
        {
            return -1;
        }

        oCine.TipoSillas.Add(tblTipoSilla);
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }

    public int Modificar()
    {
        var tipoSilla = (from x in oCine.TipoSillas
                         where x.IdTipoSilla == tblTipoSilla.IdTipoSilla
                         select x).FirstOrDefault();

        if (tipoSilla == null)
        {
            return -2;
        }

        tipoSilla.Nombre = tblTipoSilla.Nombre;
        tipoSilla.PrecioBase = tblTipoSilla.PrecioBase;
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }
}
