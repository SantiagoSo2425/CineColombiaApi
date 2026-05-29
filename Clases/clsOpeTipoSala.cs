using System.Collections.Generic;
using System.Linq;
using CineColombiaApi.Models;

namespace apiCine.Clases;

public class clsOpeTipoSala
{
    private readonly CineColombiaContext oCine;
    public TipoSala tblTipoSala { get; set; }

    public clsOpeTipoSala(CineColombiaContext oCine)
    {
        this.oCine = oCine;
        tblTipoSala = new TipoSala();
    }

    public List<TipoSala> ListarTipoSalas()
    {
        return oCine.TipoSalas.ToList();
    }

    public IQueryable ConsultarTipoSala(int idTipoSala)
    {
        return from x in oCine.TipoSalas
               where x.IdTipoSala == idTipoSala
               select x;
    }

    public int Agregar()
    {
        var existe = (from x in oCine.TipoSalas
                      where x.IdTipoSala == tblTipoSala.IdTipoSala
                      select x).Any();

        if (existe)
        {
            return -1;
        }

        oCine.TipoSalas.Add(tblTipoSala);
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }

    public int Modificar()
    {
        var tipoSala = (from x in oCine.TipoSalas
                        where x.IdTipoSala == tblTipoSala.IdTipoSala
                        select x).FirstOrDefault();

        if (tipoSala == null)
        {
            return -2;
        }

        tipoSala.Nombre = tblTipoSala.Nombre;
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }
}
