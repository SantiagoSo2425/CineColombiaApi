using System.Collections.Generic;
using System.Linq;
using CineColombiaApi.Models;

namespace apiCine.Clases;

public class clsOpeDireccionCliente
{
    private readonly CineColombiaContext oCine;
    public DireccionCliente tblDireccionCliente { get; set; }

    public clsOpeDireccionCliente(CineColombiaContext oCine)
    {
        this.oCine = oCine;
        tblDireccionCliente = new DireccionCliente();
    }

    public List<DireccionCliente> ListarDireccionesCliente()
    {
        return oCine.DireccionClientes.ToList();
    }

    public IQueryable ConsultarDireccionCliente(int idDireccionCli)
    {
        return from x in oCine.DireccionClientes
               where x.IdDireccionCli == idDireccionCli
               select x;
    }

    public int Agregar()
    {
        oCine.DireccionClientes.Add(tblDireccionCliente);
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }

    public int Modificar()
    {
        var direccion = (from x in oCine.DireccionClientes
                         where x.IdDireccionCli == tblDireccionCliente.IdDireccionCli
                         select x).FirstOrDefault();

        if (direccion == null)
        {
            return -2;
        }

        direccion.IdCliente = tblDireccionCliente.IdCliente;
        direccion.IdCiudad = tblDireccionCliente.IdCiudad;
        direccion.Direccion = tblDireccionCliente.Direccion;
        direccion.Activo = tblDireccionCliente.Activo;
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }

    public int Inactivar(int idDireccionCli)
    {
        var direccion = (from x in oCine.DireccionClientes
                         where x.IdDireccionCli == idDireccionCli
                         select x).FirstOrDefault();

        if (direccion == null)
        {
            return -2;
        }

        direccion.Activo = false;
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }
}
