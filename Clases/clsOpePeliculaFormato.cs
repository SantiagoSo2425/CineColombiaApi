using System.Collections.Generic;
using System.Linq;
using CineColombiaApi.Models;

namespace apiCine.Clases;

public class clsOpePeliculaFormato
{
    private readonly CineColombiaContext oCine;
    public PeliculaFormato tblPeliculaFormato { get; set; }

    public clsOpePeliculaFormato(CineColombiaContext oCine)
    {
        this.oCine = oCine;
        tblPeliculaFormato = new PeliculaFormato();
    }

    public List<PeliculaFormato> ListarPeliculasFormato()
    {
        return oCine.PeliculaFormatos.ToList();
    }

    public IQueryable ConsultarPeliculaFormato(int idPeliculaFormato)
    {
        return from x in oCine.PeliculaFormatos
               where x.IdPeliculaFormato == idPeliculaFormato
               select x;
    }

    public int Agregar()
    {
        var existe = (from x in oCine.PeliculaFormatos
                      where x.IdPelicula == tblPeliculaFormato.IdPelicula
                      && x.IdFormato == tblPeliculaFormato.IdFormato
                      select x).Any();

        if (existe)
        {
            return -1;
        }

        oCine.PeliculaFormatos.Add(tblPeliculaFormato);
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }

    public int Modificar()
    {
        var peliculaFormato = (from x in oCine.PeliculaFormatos
                               where x.IdPeliculaFormato == tblPeliculaFormato.IdPeliculaFormato
                               select x).FirstOrDefault();

        if (peliculaFormato == null)
        {
            return -2;
        }

        var existe = (from x in oCine.PeliculaFormatos
                      where x.IdPeliculaFormato != tblPeliculaFormato.IdPeliculaFormato
                      && x.IdPelicula == tblPeliculaFormato.IdPelicula
                      && x.IdFormato == tblPeliculaFormato.IdFormato
                      select x).Any();

        if (existe)
        {
            return -1;
        }

        peliculaFormato.IdPelicula = tblPeliculaFormato.IdPelicula;
        peliculaFormato.IdFormato = tblPeliculaFormato.IdFormato;
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }
}
