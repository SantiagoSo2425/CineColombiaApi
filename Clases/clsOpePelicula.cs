using System.Collections.Generic;
using System.Linq;
using CineColombiaApi.Models;

namespace apiCine.Clases;

public class clsOpePelicula
{
    private readonly CineColombiaContext oCine;
    public Pelicula tblPelicula { get; set; }

    public clsOpePelicula(CineColombiaContext oCine)
    {
        this.oCine = oCine;
        tblPelicula = new Pelicula();
    }

    public List<Pelicula> ListarPeliculas()
    {
        return oCine.Peliculas.ToList();
    }

    public IQueryable ConsultarPelicula(int idPelicula)
    {
        return from x in oCine.Peliculas
               where x.IdPelicula == idPelicula
               select x;
    }

    public int Agregar()
    {
        oCine.Peliculas.Add(tblPelicula);
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }

    public int Modificar()
    {
        var pelicula = (from x in oCine.Peliculas
                        where x.IdPelicula == tblPelicula.IdPelicula
                        select x).FirstOrDefault();

        if (pelicula == null)
        {
            return -2;
        }

        pelicula.IdGenero = tblPelicula.IdGenero;
        pelicula.IdClasificacion = tblPelicula.IdClasificacion;
        pelicula.TituloOriginal = tblPelicula.TituloOriginal;
        pelicula.NombreOferta = tblPelicula.NombreOferta;
        pelicula.Resumen = tblPelicula.Resumen;
        pelicula.AnioEstreno = tblPelicula.AnioEstreno;
        pelicula.TrailerLink = tblPelicula.TrailerLink;
        pelicula.DuracionMin = tblPelicula.DuracionMin;
        pelicula.RegistradoPor = tblPelicula.RegistradoPor;
        pelicula.FechaRegistro = tblPelicula.FechaRegistro;

        return oCine.SaveChanges() > 0 ? 1 : 0;
    }

    public int Inactivar(int idPelicula)
    {
        var pelicula = (from x in oCine.Peliculas
                        where x.IdPelicula == idPelicula
                        select x).FirstOrDefault();

        if (pelicula == null)
        {
            return -2;
        }

        return 1;
    }
}
