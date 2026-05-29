using System.Collections.Generic;
using System.Linq;
using apiSIA.Models;
using CineColombiaApi.Models;

namespace apiCine.Clases;

public class clsOpeEmpleado
{
    private readonly CineColombiaContext oCine;
    public Empleado tblEmpleado { get; set; }

    public clsOpeEmpleado(CineColombiaContext oCine)
    {
        this.oCine = oCine;
        tblEmpleado = new Empleado();
    }

    public List<Empleado> ListarEmpleados()
    {
        return oCine.Empleados.ToList();
    }

    public IQueryable ConsultarEmpleado(int idEmpleado)
    {
        return from x in oCine.Empleados
               where x.IdEmpleado == idEmpleado
               select x;
    }

    public int Agregar()
    {
        var existe = (from x in oCine.Empleados
                      where x.IdTipoDoc == tblEmpleado.IdTipoDoc
                      && x.NumDocumento == tblEmpleado.NumDocumento
                      select x).Any();

        if (existe)
        {
            return -1;
        }

        oCine.Empleados.Add(tblEmpleado);
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }

    public int Modificar()
    {
        var empleado = (from x in oCine.Empleados
                        where x.IdEmpleado == tblEmpleado.IdEmpleado
                        select x).FirstOrDefault();

        if (empleado == null)
        {
            return -2;
        }

        empleado.IdTeatro = tblEmpleado.IdTeatro;
        empleado.IdTipoDoc = tblEmpleado.IdTipoDoc;
        empleado.NumDocumento = tblEmpleado.NumDocumento;
        empleado.Nombres = tblEmpleado.Nombres;
        empleado.Apellidos = tblEmpleado.Apellidos;
        empleado.FechaIngreso = tblEmpleado.FechaIngreso;
        empleado.Activo = tblEmpleado.Activo;

        return oCine.SaveChanges() > 0 ? 1 : 0;
    }
}
