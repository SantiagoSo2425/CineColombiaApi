using System.Collections.Generic;
using System.Linq;
using CineColombiaApi.Models;

namespace apiCine.Clases;

public class clsOpeDireccionEmpleado
{
    private readonly CineColombiaContext oCine;
    public DireccionEmpleado tblDireccionEmpleado { get; set; }

    public clsOpeDireccionEmpleado(CineColombiaContext oCine)
    {
        this.oCine = oCine;
        tblDireccionEmpleado = new DireccionEmpleado();
    }

    public List<DireccionEmpleado> ListarDireccionesEmpleado()
    {
        return oCine.DireccionEmpleados.ToList();
    }

    public IQueryable ConsultarDireccionEmpleado(int idDireccionEmp)
    {
        return from x in oCine.DireccionEmpleados
               where x.IdDireccionEmp == idDireccionEmp
               select x;
    }

    public int Agregar()
    {
        oCine.DireccionEmpleados.Add(tblDireccionEmpleado);
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }

    public int Modificar()
    {
        var direccion = (from x in oCine.DireccionEmpleados
                         where x.IdDireccionEmp == tblDireccionEmpleado.IdDireccionEmp
                         select x).FirstOrDefault();

        if (direccion == null)
        {
            return -2;
        }

        direccion.IdEmpleado = tblDireccionEmpleado.IdEmpleado;
        direccion.IdCiudad = tblDireccionEmpleado.IdCiudad;
        direccion.Direccion = tblDireccionEmpleado.Direccion;
        direccion.Activo = tblDireccionEmpleado.Activo;
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }

    public int Inactivar(int idDireccionEmp)
    {
        var direccion = (from x in oCine.DireccionEmpleados
                         where x.IdDireccionEmp == idDireccionEmp
                         select x).FirstOrDefault();

        if (direccion == null)
        {
            return -2;
        }

        direccion.Activo = false;
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }
}
