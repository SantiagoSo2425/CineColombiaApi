using System.Collections.Generic;
using System.Linq;
using CineColombiaApi.Models;

namespace apiCine.Clases;

public class clsOpeRol
{
    private readonly CineColombiaContext oCine;
    public Rol tblRol { get; set; }

    public clsOpeRol(CineColombiaContext oCine)
    {
        this.oCine = oCine;
        tblRol = new Rol();
    }

    public List<Rol> ListarRoles()
    {
        return oCine.Rols.ToList();
    }

    public IQueryable ConsultarRol(int idRol)
    {
        return from x in oCine.Rols
               where x.IdRol == idRol
               select x;
    }

    public int Agregar()
    {
        var existe = (from x in oCine.Rols
                      where x.IdRol == tblRol.IdRol
                      select x).Any();

        if (existe)
        {
            return -1;
        }

        oCine.Rols.Add(tblRol);
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }

    public int Modificar()
    {
        var rol = (from x in oCine.Rols
                   where x.IdRol == tblRol.IdRol
                   select x).FirstOrDefault();

        if (rol == null)
        {
            return -2;
        }

        rol.Nombre = tblRol.Nombre;
        rol.Descripcion = tblRol.Descripcion;
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }
}
