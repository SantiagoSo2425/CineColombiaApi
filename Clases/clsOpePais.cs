using System.Collections.Generic;
using System.Linq;
using CineColombiaApi.Models;

namespace apiCine.Clases;

public class clsOpePais
{
    private readonly CineColombiaContext oCine;
    public Pai tblPais { get; set; }

    public clsOpePais(CineColombiaContext oCine)
    {
        this.oCine = oCine;
        tblPais = new Pai();
    }

    public List<Pai> ListarPaises()
    {
        return oCine.Pais.ToList();
    }

    public IQueryable ConsultarPais(int idPais)
    {
        return from x in oCine.Pais
               where x.IdPais == idPais
               select x;
    }

    public int Agregar()
    {
        var existe = (from x in oCine.Pais
                      where x.IdPais == tblPais.IdPais
                      select x).Any();

        if (existe)
        {
            return -1;
        }

        oCine.Pais.Add(tblPais);
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }

    public int Modificar()
    {
        var pais = (from x in oCine.Pais
                    where x.IdPais == tblPais.IdPais
                    select x).FirstOrDefault();

        if (pais == null)
        {
            return -2;
        }

        pais.Nombre = tblPais.Nombre;
        pais.Codigo = tblPais.Codigo;
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }
}
