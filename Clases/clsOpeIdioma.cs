using System.Collections.Generic;
using System.Linq;
using CineColombiaApi.Models;

namespace apiCine.Clases;

public class clsOpeIdioma
{
    private readonly CineColombiaContext oCine;
    public Idioma tblIdioma { get; set; }

    public clsOpeIdioma(CineColombiaContext oCine)
    {
        this.oCine = oCine;
        tblIdioma = new Idioma();
    }

    public List<Idioma> ListarIdiomas()
    {
        return oCine.Idiomas.ToList();
    }

    public IQueryable ConsultarIdioma(int idIdioma)
    {
        return from x in oCine.Idiomas
               where x.IdIdioma == idIdioma
               select x;
    }

    public int Agregar()
    {
        var existe = (from x in oCine.Idiomas
                      where x.IdIdioma == tblIdioma.IdIdioma
                      select x).Any();

        if (existe)
        {
            return -1;
        }

        oCine.Idiomas.Add(tblIdioma);
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }

    public int Modificar()
    {
        var idioma = (from x in oCine.Idiomas
                      where x.IdIdioma == tblIdioma.IdIdioma
                      select x).FirstOrDefault();

        if (idioma == null)
        {
            return -2;
        }

        idioma.Nombre = tblIdioma.Nombre;
        idioma.Codigo = tblIdioma.Codigo;
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }
}
