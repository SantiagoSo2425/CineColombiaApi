using System.Collections.Generic;
using System.Linq;
using CineColombiaApi.Models;

namespace apiCine.Clases;

public class clsOpePeliculaIdioma
{
    private readonly CineColombiaContext oCine;
    public PeliculaIdioma tblPeliculaIdioma { get; set; }

    public clsOpePeliculaIdioma(CineColombiaContext oCine)
    {
        this.oCine = oCine;
        tblPeliculaIdioma = new PeliculaIdioma();
    }

    public List<PeliculaIdioma> ListarPeliculasIdioma()
    {
        return oCine.PeliculaIdiomas.ToList();
    }

    public IQueryable ConsultarPeliculaIdioma(int idPeliculaIdioma)
    {
        return from x in oCine.PeliculaIdiomas
               where x.IdPeliculaIdioma == idPeliculaIdioma
               select x;
    }

    public int Agregar()
    {
        var existe = (from x in oCine.PeliculaIdiomas
                      where x.IdPelicula == tblPeliculaIdioma.IdPelicula
                      && x.IdIdioma == tblPeliculaIdioma.IdIdioma
                      select x).Any();

        if (existe)
        {
            return -1;
        }

        oCine.PeliculaIdiomas.Add(tblPeliculaIdioma);
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }

    public int Modificar()
    {
        var peliculaIdioma = (from x in oCine.PeliculaIdiomas
                              where x.IdPeliculaIdioma == tblPeliculaIdioma.IdPeliculaIdioma
                              select x).FirstOrDefault();

        if (peliculaIdioma == null)
        {
            return -2;
        }

        var existe = (from x in oCine.PeliculaIdiomas
                      where x.IdPeliculaIdioma != tblPeliculaIdioma.IdPeliculaIdioma
                      && x.IdPelicula == tblPeliculaIdioma.IdPelicula
                      && x.IdIdioma == tblPeliculaIdioma.IdIdioma
                      select x).Any();

        if (existe)
        {
            return -1;
        }

        peliculaIdioma.IdPelicula = tblPeliculaIdioma.IdPelicula;
        peliculaIdioma.IdIdioma = tblPeliculaIdioma.IdIdioma;
        peliculaIdioma.EsOriginal = tblPeliculaIdioma.EsOriginal;
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }
}
