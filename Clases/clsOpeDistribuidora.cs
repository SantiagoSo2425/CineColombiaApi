using System.Collections.Generic;
using System.Linq;
using CineColombiaApi.Models;

namespace apiCine.Clases;

public class clsOpeDistribuidora
{
    private readonly CineColombiaContext oCine;
    public Distribuidora tblDistribuidora { get; set; }

    public clsOpeDistribuidora(CineColombiaContext oCine)
    {
        this.oCine = oCine;
        tblDistribuidora = new Distribuidora();
    }

    public List<Distribuidora> ListarDistribuidoras()
    {
        return oCine.Distribuidoras.ToList();
    }

    public IQueryable ConsultarDistribuidora(int idDistribuidora)
    {
        return from x in oCine.Distribuidoras
               where x.IdDistribuidora == idDistribuidora
               select x;
    }

    public int Agregar()
    {
        var existe = (from x in oCine.Distribuidoras
                      where x.IdDistribuidora == tblDistribuidora.IdDistribuidora
                      select x).Any();

        if (existe)
        {
            return -1;
        }

        oCine.Distribuidoras.Add(tblDistribuidora);
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }

    public int Modificar()
    {
        var distribuidora = (from x in oCine.Distribuidoras
                             where x.IdDistribuidora == tblDistribuidora.IdDistribuidora
                             select x).FirstOrDefault();

        if (distribuidora == null)
        {
            return -2;
        }

        distribuidora.Nombre = tblDistribuidora.Nombre;
        distribuidora.IdPais = tblDistribuidora.IdPais;
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }
}
