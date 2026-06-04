using System.Collections.Generic;
using System.Linq;
using CineColombiaApi.Models;

namespace apiCine.Clases;

public class clsOpeTipoTelefono
{
    private readonly CineColombiaContext oCine;
    public TipoTelefono tblTipoTelefono { get; set; }

    public clsOpeTipoTelefono(CineColombiaContext oCine)
    {
        this.oCine = oCine;
        tblTipoTelefono = new TipoTelefono();
    }

    public List<TipoTelefono> ListarTipoTelefonos()
    {
        return oCine.TipoTelefonos.ToList();
    }

    public IQueryable ConsultarTipoTelefono(int idTipoTelefono)
    {
        return from x in oCine.TipoTelefonos
               where x.IdTipoTelefono == idTipoTelefono
               select x;
    }

    public int Agregar()
    {
        if (tblTipoTelefono.IdTipoTelefono == 0)
        {
            var maxId = oCine.TipoTelefonos.Max(e => (int?)e.IdTipoTelefono) ?? 0;
            tblTipoTelefono.IdTipoTelefono = maxId + 1;
        }
        var existe = (from x in oCine.TipoTelefonos
                      where x.IdTipoTelefono == tblTipoTelefono.IdTipoTelefono
                      select x).Any();

        if (existe)
        {
            return -1;
        }

        oCine.TipoTelefonos.Add(tblTipoTelefono);
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }

    public int Modificar()
    {
        var tipoTelefono = (from x in oCine.TipoTelefonos
                            where x.IdTipoTelefono == tblTipoTelefono.IdTipoTelefono
                            select x).FirstOrDefault();

        if (tipoTelefono == null)
        {
            return -2;
        }

        tipoTelefono.Nombre = tblTipoTelefono.Nombre;
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }
}
