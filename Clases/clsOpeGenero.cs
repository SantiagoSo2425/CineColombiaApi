using System.Collections.Generic;
using System.Linq;
using apiSIA.Models;
using CineColombiaApi.Models;

namespace apiCine.Clases;

public class clsOpeGenero
{
    private readonly CineColombiaContext oCine;
    public Genero tblGenero { get; set; }

    public clsOpeGenero(CineColombiaContext oCine)
    {
        this.oCine = oCine;
        tblGenero = new Genero();
    }

    public List<Genero> ListarGeneros()
    {
        return oCine.Generos.ToList();
    }

    public IQueryable ConsultarGenero(int idGenero)
    {
        return from x in oCine.Generos
               where x.IdGenero == idGenero
               select x;
    }

    public int Agregar()
    {
        var existe = (from x in oCine.Generos
                      where x.IdGenero == tblGenero.IdGenero
                      select x).Any();

        if (existe)
        {
            return -1;
        }

        oCine.Generos.Add(tblGenero);
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }

    public int Modificar()
    {
        var genero = (from x in oCine.Generos
                      where x.IdGenero == tblGenero.IdGenero
                      select x).FirstOrDefault();

        if (genero == null)
        {
            return -2;
        }

        genero.Nombre = tblGenero.Nombre;

        return oCine.SaveChanges() > 0 ? 1 : 0;
    }
}
