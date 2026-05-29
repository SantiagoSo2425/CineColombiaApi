using System.Collections.Generic;
using System.Linq;
using CineColombiaApi.Models;

namespace apiCine.Clases;

public class clsOpeTipoDocumento
{
    private readonly CineColombiaContext oCine;
    public TipoDocumento tblTipoDocumento { get; set; }

    public clsOpeTipoDocumento(CineColombiaContext oCine)
    {
        this.oCine = oCine;
        tblTipoDocumento = new TipoDocumento();
    }

    public List<TipoDocumento> ListarTipoDocumentos()
    {
        return oCine.TipoDocumentos.ToList();
    }

    public IQueryable ConsultarTipoDocumento(int idTipoDocumento)
    {
        return from x in oCine.TipoDocumentos
               where x.IdTipoDoc == idTipoDocumento
               select x;
    }

    public int Agregar()
    {
        var existe = (from x in oCine.TipoDocumentos
                      where x.IdTipoDoc == tblTipoDocumento.IdTipoDoc
                      select x).Any();

        if (existe)
        {
            return -1;
        }

        oCine.TipoDocumentos.Add(tblTipoDocumento);
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }

    public int Modificar()
    {
        var tipoDocumento = (from x in oCine.TipoDocumentos
                             where x.IdTipoDoc == tblTipoDocumento.IdTipoDoc
                             select x).FirstOrDefault();

        if (tipoDocumento == null)
        {
            return -2;
        }

        tipoDocumento.Codigo = tblTipoDocumento.Codigo;
        tipoDocumento.Descripcion = tblTipoDocumento.Descripcion;
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }
}
