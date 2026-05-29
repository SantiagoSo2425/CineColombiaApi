using System.Collections.Generic;
using System.Linq;
using apiSIA.Models;
using CineColombiaApi.Models;

namespace apiCine.Clases;

public class clsOpeCliente
{
    private readonly CineColombiaContext oCine;
    public Cliente tblCliente { get; set; }

    public clsOpeCliente(CineColombiaContext oCine)
    {
        this.oCine = oCine;
        tblCliente = new Cliente();
    }

    public List<Cliente> ListarClientes()
    {
        return oCine.Clientes.ToList();
    }

    public IQueryable ConsultarCliente(int idCliente)
    {
        return from x in oCine.Clientes
               where x.IdCliente == idCliente
               select x;
    }

    public int Agregar()
    {
        var existe = (from x in oCine.Clientes
                      where x.IdTipoDoc == tblCliente.IdTipoDoc
                      && x.NumDocumento == tblCliente.NumDocumento
                      select x).Any();

        if (existe)
        {
            return -1;
        }

        oCine.Clientes.Add(tblCliente);
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }

    public int Modificar()
    {
        var cliente = (from x in oCine.Clientes
                       where x.IdCliente == tblCliente.IdCliente
                       select x).FirstOrDefault();

        if (cliente == null)
        {
            return -2;
        }

        cliente.IdTipoCliente = tblCliente.IdTipoCliente;
        cliente.IdTipoDoc = tblCliente.IdTipoDoc;
        cliente.NumDocumento = tblCliente.NumDocumento;
        cliente.Nombres = tblCliente.Nombres;
        cliente.Apellidos = tblCliente.Apellidos;
        cliente.Email = tblCliente.Email;
        cliente.Activo = tblCliente.Activo;

        return oCine.SaveChanges() > 0 ? 1 : 0;
    }
}
