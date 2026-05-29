using System.Collections.Generic;
using System.Linq;
using CineColombiaApi.Models;

namespace apiCine.Clases;

public class clsOpeTelefonoEmpleado
{
    private readonly CineColombiaContext oCine;
    public TelefonoEmpleado tblTelefonoEmpleado { get; set; }

    public clsOpeTelefonoEmpleado(CineColombiaContext oCine)
    {
        this.oCine = oCine;
        tblTelefonoEmpleado = new TelefonoEmpleado();
    }

    public List<TelefonoEmpleado> ListarTelefonosEmpleado()
    {
        return oCine.TelefonoEmpleados.ToList();
    }

    public IQueryable ConsultarTelefonoEmpleado(int idTelefonoEmp)
    {
        return from x in oCine.TelefonoEmpleados
               where x.IdTelefonoEmp == idTelefonoEmp
               select x;
    }

    public int Agregar()
    {
        oCine.TelefonoEmpleados.Add(tblTelefonoEmpleado);
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }

    public int Modificar()
    {
        var telefono = (from x in oCine.TelefonoEmpleados
                        where x.IdTelefonoEmp == tblTelefonoEmpleado.IdTelefonoEmp
                        select x).FirstOrDefault();

        if (telefono == null)
        {
            return -2;
        }

        telefono.IdEmpleado = tblTelefonoEmpleado.IdEmpleado;
        telefono.IdTipoTelefono = tblTelefonoEmpleado.IdTipoTelefono;
        telefono.Numero = tblTelefonoEmpleado.Numero;
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }
}
