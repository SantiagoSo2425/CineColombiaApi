using System.Collections.Generic;
using System.Linq;
using CineColombiaApi.Models;

namespace apiCine.Clases;

public class clsOpeEmpleadoProfesion
{
    private readonly CineColombiaContext oCine;
    public EmpleadoProfesion tblEmpleadoProfesion { get; set; }

    public clsOpeEmpleadoProfesion(CineColombiaContext oCine)
    {
        this.oCine = oCine;
        tblEmpleadoProfesion = new EmpleadoProfesion();
    }

    public List<EmpleadoProfesion> ListarEmpleadosProfesiones()
    {
        return oCine.EmpleadoProfesions.ToList();
    }

    public IQueryable ConsultarEmpleadoProfesion(int idEmpProfesion)
    {
        return from x in oCine.EmpleadoProfesions
               where x.IdEmpProfesion == idEmpProfesion
               select x;
    }

    public int Agregar()
    {
        var existe = (from x in oCine.EmpleadoProfesions
                      where x.IdEmpleado == tblEmpleadoProfesion.IdEmpleado
                      && x.IdProfesion == tblEmpleadoProfesion.IdProfesion
                      select x).Any();

        if (existe)
        {
            return -1;
        }

        oCine.EmpleadoProfesions.Add(tblEmpleadoProfesion);
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }

    public int Modificar()
    {
        var empleadoProfesion = (from x in oCine.EmpleadoProfesions
                                 where x.IdEmpProfesion == tblEmpleadoProfesion.IdEmpProfesion
                                 select x).FirstOrDefault();

        if (empleadoProfesion == null)
        {
            return -2;
        }

        var existe = (from x in oCine.EmpleadoProfesions
                      where x.IdEmpProfesion != tblEmpleadoProfesion.IdEmpProfesion
                      && x.IdEmpleado == tblEmpleadoProfesion.IdEmpleado
                      && x.IdProfesion == tblEmpleadoProfesion.IdProfesion
                      select x).Any();

        if (existe)
        {
            return -1;
        }

        empleadoProfesion.IdEmpleado = tblEmpleadoProfesion.IdEmpleado;
        empleadoProfesion.IdProfesion = tblEmpleadoProfesion.IdProfesion;
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }
}
