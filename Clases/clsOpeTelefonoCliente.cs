using System.Collections.Generic;
using System.Linq;
using CineColombiaApi.Models;

namespace apiCine.Clases;

public class clsOpeTelefonoCliente
{
    private readonly CineColombiaContext oCine;
    public TelefonoCliente tblTelefonoCliente { get; set; }

    public clsOpeTelefonoCliente(CineColombiaContext oCine)
    {
        this.oCine = oCine;
        tblTelefonoCliente = new TelefonoCliente();
    }

    public List<TelefonoCliente> ListarTelefonosCliente()
    {
        return oCine.TelefonoClientes.ToList();
    }

    public IQueryable ConsultarTelefonoCliente(int idTelefono)
    {
        return from x in oCine.TelefonoClientes
               where x.IdTelefono == idTelefono
               select x;
    }

    public int Agregar()
    {
        oCine.TelefonoClientes.Add(tblTelefonoCliente);
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }

    public int Modificar()
    {
        var telefono = (from x in oCine.TelefonoClientes
                        where x.IdTelefono == tblTelefonoCliente.IdTelefono
                        select x).FirstOrDefault();

        if (telefono == null)
        {
            return -2;
        }

        telefono.IdCliente = tblTelefonoCliente.IdCliente;
        telefono.IdTipoTelefono = tblTelefonoCliente.IdTipoTelefono;
        telefono.Numero = tblTelefonoCliente.Numero;
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }
}
