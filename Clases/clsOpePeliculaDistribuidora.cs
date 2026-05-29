using System.Collections.Generic;
using System.Linq;
using CineColombiaApi.Models;

namespace apiCine.Clases;

public class clsOpePeliculaDistribuidora
{
    private readonly CineColombiaContext oCine;
    public PeliculaDistribuidora tblPeliculaDistribuidora { get; set; }

    public clsOpePeliculaDistribuidora(CineColombiaContext oCine)
    {
        this.oCine = oCine;
        tblPeliculaDistribuidora = new PeliculaDistribuidora();
    }

    public List<PeliculaDistribuidora> ListarPeliculasDistribuidora()
    {
        return oCine.PeliculaDistribuidoras.ToList();
    }

    public IQueryable ConsultarPeliculaDistribuidora(int idPeliculaDistribuidora)
    {
        return from x in oCine.PeliculaDistribuidoras
               where x.IdPeliculaDistribuidora == idPeliculaDistribuidora
               select x;
    }

    public int Agregar()
    {
        var existe = (from x in oCine.PeliculaDistribuidoras
                      where x.IdPelicula == tblPeliculaDistribuidora.IdPelicula
                      && x.IdDistribuidora == tblPeliculaDistribuidora.IdDistribuidora
                      select x).Any();

        if (existe)
        {
            return -1;
        }

        oCine.PeliculaDistribuidoras.Add(tblPeliculaDistribuidora);
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }

    public int Modificar()
    {
        var peliculaDistribuidora = (from x in oCine.PeliculaDistribuidoras
                                     where x.IdPeliculaDistribuidora == tblPeliculaDistribuidora.IdPeliculaDistribuidora
                                     select x).FirstOrDefault();

        if (peliculaDistribuidora == null)
        {
            return -2;
        }

        var existe = (from x in oCine.PeliculaDistribuidoras
                      where x.IdPeliculaDistribuidora != tblPeliculaDistribuidora.IdPeliculaDistribuidora
                      && x.IdPelicula == tblPeliculaDistribuidora.IdPelicula
                      && x.IdDistribuidora == tblPeliculaDistribuidora.IdDistribuidora
                      select x).Any();

        if (existe)
        {
            return -1;
        }

        peliculaDistribuidora.IdPelicula = tblPeliculaDistribuidora.IdPelicula;
        peliculaDistribuidora.IdDistribuidora = tblPeliculaDistribuidora.IdDistribuidora;
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }
}
