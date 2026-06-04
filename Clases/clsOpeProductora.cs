using System.Collections.Generic;
using System.Linq;
using CineColombiaApi.Models;

namespace apiCine.Clases;

public class clsOpeProductora
{
    private readonly CineColombiaContext oCine;
    public Productora tblProductora { get; set; }

    public clsOpeProductora(CineColombiaContext oCine)
    {
        this.oCine = oCine;
        tblProductora = new Productora();
    }

    public List<Productora> ListarProductoras()
    {
        return oCine.Productoras.ToList();
    }

    public IQueryable ConsultarProductora(int idProductora)
    {
        return from x in oCine.Productoras
               where x.IdProductora == idProductora
               select x;
    }

    public int Agregar()
    {
        if (tblProductora.IdProductora == 0)
        {
            var maxId = oCine.Productoras.Max(e => (int?)e.IdProductora) ?? 0;
            tblProductora.IdProductora = maxId + 1;
        }
        var existe = (from x in oCine.Productoras
                      where x.IdProductora == tblProductora.IdProductora
                      select x).Any();

        if (existe)
        {
            return -1;
        }

        oCine.Productoras.Add(tblProductora);
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }

    public int Modificar()
    {
        var productora = (from x in oCine.Productoras
                          where x.IdProductora == tblProductora.IdProductora
                          select x).FirstOrDefault();

        if (productora == null)
        {
            return -2;
        }

        productora.Nombre = tblProductora.Nombre;
        productora.IdPais = tblProductora.IdPais;
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }
}
