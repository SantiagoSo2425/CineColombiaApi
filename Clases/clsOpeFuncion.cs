using System.Collections.Generic;
using System.Linq;
using CineColombiaApi.Models;

namespace apiCine.Clases;

public class clsOpeFuncion
{
    private readonly CineColombiaContext oCine;
    public Funcion tblFuncion { get; set; }

    public clsOpeFuncion(CineColombiaContext oCine)
    {
        this.oCine = oCine;
        tblFuncion = new Funcion();
    }

    public List<Funcion> ListarFunciones()
    {
        return oCine.Funcions.ToList();
    }

    public IQueryable ConsultarFuncion(int idFuncion)
    {
        return from x in oCine.Funcions
               where x.IdFuncion == idFuncion
               select x;
    }

    public int Agregar()
    {
        var existe = (from x in oCine.Funcions
                      where x.IdSala == tblFuncion.IdSala
                      && x.FechaFuncion == tblFuncion.FechaFuncion
                      && x.HoraInicio == tblFuncion.HoraInicio
                      select x).Any();

        if (existe)
        {
            return -1;
        }

        oCine.Funcions.Add(tblFuncion);
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }

    public int Modificar()
    {
        var funcion = (from x in oCine.Funcions
                       where x.IdFuncion == tblFuncion.IdFuncion
                       select x).FirstOrDefault();

        if (funcion == null)
        {
            return -2;
        }

        var existe = (from x in oCine.Funcions
                      where x.IdFuncion != tblFuncion.IdFuncion
                      && x.IdSala == tblFuncion.IdSala
                      && x.FechaFuncion == tblFuncion.FechaFuncion
                      && x.HoraInicio == tblFuncion.HoraInicio
                      select x).Any();

        if (existe)
        {
            return -1;
        }

        funcion.IdSala = tblFuncion.IdSala;
        funcion.IdPelicula = tblFuncion.IdPelicula;
        funcion.IdIdioma = tblFuncion.IdIdioma;
        funcion.IdFormato = tblFuncion.IdFormato;
        funcion.FechaFuncion = tblFuncion.FechaFuncion;
        funcion.HoraInicio = tblFuncion.HoraInicio;
        funcion.HoraFin = tblFuncion.HoraFin;
        funcion.PrecioBase = tblFuncion.PrecioBase;
        funcion.Estado = tblFuncion.Estado;
        funcion.RegistradoPor = tblFuncion.RegistradoPor;
        funcion.FechaRegistro = tblFuncion.FechaRegistro;

        return oCine.SaveChanges() > 0 ? 1 : 0;
    }

    public int Inactivar(int idFuncion)
    {
        var funcion = (from x in oCine.Funcions
                       where x.IdFuncion == idFuncion
                       select x).FirstOrDefault();

        if (funcion == null)
        {
            return -2;
        }

        funcion.Estado = false;
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }
}
