using System.Collections.Generic;
using System.Linq;
using apiSIA.Models;
using CineColombiaApi.Models;

namespace apiCine.Clases;

public class clsOpeTipoCliente
{
    private readonly CineColombiaContext oCine;
    public TipoCliente tblTipoCliente { get; set; }

    public clsOpeTipoCliente(CineColombiaContext oCine)
    {
        this.oCine = oCine;
        tblTipoCliente = new TipoCliente();
    }

    public List<TipoCliente> ListarTipoClientes()
    {
        return oCine.TipoClientes.ToList();
    }

    public IQueryable ConsultarTipoCliente(int idTipoCliente)
    {
        return from x in oCine.TipoClientes
               where x.IdTipoCliente == idTipoCliente
               select x;
    }

    public int Agregar()
    {
        var existe = (from x in oCine.TipoClientes
                      where x.IdTipoCliente == tblTipoCliente.IdTipoCliente
                      select x).Any();

        if (existe)
        {
            return -1;
        }

        oCine.TipoClientes.Add(tblTipoCliente);
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }

    public int Modificar()
    {
        var tipoCliente = (from x in oCine.TipoClientes
                           where x.IdTipoCliente == tblTipoCliente.IdTipoCliente
                           select x).FirstOrDefault();

        if (tipoCliente == null)
        {
            return -2;
        }

        tipoCliente.Nombre = tblTipoCliente.Nombre;
        tipoCliente.Descripcion = tblTipoCliente.Descripcion;

        return oCine.SaveChanges() > 0 ? 1 : 0;
    }
}
