using System.Collections.Generic;
using System.Linq;
using CineColombiaApi.Models;

namespace apiCine.Clases;

public class clsOpePeliculaProductora
{
    private readonly CineColombiaContext oCine;
    public PeliculaProductora tblPeliculaProductora { get; set; }

    public clsOpePeliculaProductora(CineColombiaContext oCine)
    {
        this.oCine = oCine;
        tblPeliculaProductora = new PeliculaProductora();
    }

    public List<PeliculaProductora> ListarPeliculasProductora()
    {
        return oCine.PeliculaProductoras.ToList();
    }

    public IQueryable ConsultarPeliculaProductora(int idPeliculaProductora)
    {
        return from x in oCine.PeliculaProductoras
               where x.IdPeliculaProductora == idPeliculaProductora
               select x;
    }

    public int Agregar()
    {
        var existe = (from x in oCine.PeliculaProductoras
                      where x.IdPelicula == tblPeliculaProductora.IdPelicula
                      && x.IdProductora == tblPeliculaProductora.IdProductora
                      select x).Any();

        if (existe)
        {
            return -1;
        }

        oCine.PeliculaProductoras.Add(tblPeliculaProductora);
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }

    public int Modificar()
    {
        var peliculaProductora = (from x in oCine.PeliculaProductoras
                                  where x.IdPeliculaProductora == tblPeliculaProductora.IdPeliculaProductora
                                  select x).FirstOrDefault();

        if (peliculaProductora == null)
        {
            return -2;
        }

        var existe = (from x in oCine.PeliculaProductoras
                      where x.IdPeliculaProductora != tblPeliculaProductora.IdPeliculaProductora
                      && x.IdPelicula == tblPeliculaProductora.IdPelicula
                      && x.IdProductora == tblPeliculaProductora.IdProductora
                      select x).Any();

        if (existe)
        {
            return -1;
        }

        peliculaProductora.IdPelicula = tblPeliculaProductora.IdPelicula;
        peliculaProductora.IdProductora = tblPeliculaProductora.IdProductora;
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }
}
