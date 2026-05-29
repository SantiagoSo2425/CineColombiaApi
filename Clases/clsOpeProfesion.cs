using System.Collections.Generic;
using System.Linq;
using CineColombiaApi.Models;

namespace apiCine.Clases;

public class clsOpeProfesion
{
    private readonly CineColombiaContext oCine;
    public Profesion tblProfesion { get; set; }

    public clsOpeProfesion(CineColombiaContext oCine)
    {
        this.oCine = oCine;
        tblProfesion = new Profesion();
    }

    public List<Profesion> ListarProfesiones()
    {
        return oCine.Profesions.ToList();
    }

    public IQueryable ConsultarProfesion(int idProfesion)
    {
        return from x in oCine.Profesions
               where x.IdProfesion == idProfesion
               select x;
    }

    public int Agregar()
    {
        var existe = (from x in oCine.Profesions
                      where x.IdProfesion == tblProfesion.IdProfesion
                      select x).Any();

        if (existe)
        {
            return -1;
        }

        oCine.Profesions.Add(tblProfesion);
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }

    public int Modificar()
    {
        var profesion = (from x in oCine.Profesions
                         where x.IdProfesion == tblProfesion.IdProfesion
                         select x).FirstOrDefault();

        if (profesion == null)
        {
            return -2;
        }

        profesion.Nombre = tblProfesion.Nombre;
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }
}
