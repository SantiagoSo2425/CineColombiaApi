using System.Collections.Generic;
using System.Linq;
using apiSIA.Models;
using CineColombiaApi.Models;

namespace apiCine.Clases;

public class clsOpeBoletica
{
    private readonly CineColombiaContext oCine;
    public Boletica tblBoletica { get; set; }

    public clsOpeBoletica(CineColombiaContext oCine)
    {
        this.oCine = oCine;
        tblBoletica = new Boletica();
    }

    public List<Boletica> ListarBoleticas()
    {
        return oCine.Boleticas.ToList();
    }

    public IQueryable ConsultarBoletica(int idBoletica)
    {
        return from x in oCine.Boleticas
               where x.IdBoletica == idBoletica
               select x;
    }

    public int Agregar()
    {
        oCine.Boleticas.Add(tblBoletica);
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }

    public int Modificar()
    {
        var boletica = (from x in oCine.Boleticas
                        where x.IdBoletica == tblBoletica.IdBoletica
                        select x).FirstOrDefault();

        if (boletica == null)
        {
            return -2;
        }

        boletica.IdVenta = tblBoletica.IdVenta;
        boletica.IdFuncion = tblBoletica.IdFuncion;
        boletica.Estado = tblBoletica.Estado;

        return oCine.SaveChanges() > 0 ? 1 : 0;
    }
}
