using System.Collections.Generic;
using System.Linq;
using CineColombiaApi.Models;

namespace apiCine.Clases;

public class clsOpeClasificacion
{
    private readonly CineColombiaContext oCine;
    public Clasificacion tblClasificacion { get; set; }

    public clsOpeClasificacion(CineColombiaContext oCine)
    {
        this.oCine = oCine;
        tblClasificacion = new Clasificacion();
    }

    public List<Clasificacion> ListarClasificaciones(int page = 1, int pageSize = 50)
    {
        return oCine.Clasificacions
            .OrderBy(c => c.IdClasificacion)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }

    public IQueryable ConsultarClasificacion(int idClasificacion)
    {
        return from x in oCine.Clasificacions
               where x.IdClasificacion == idClasificacion
               select x;
    }

    public int Agregar()
    {
        if (tblClasificacion.IdClasificacion == 0)
        {
            var maxId = oCine.Clasificacions.Max(e => (int?)e.IdClasificacion) ?? 0;
            tblClasificacion.IdClasificacion = maxId + 1;
        }
        var existe = (from x in oCine.Clasificacions
                      where x.IdClasificacion == tblClasificacion.IdClasificacion
                      select x).Any();

        if (existe)
        {
            return -1;
        }

        oCine.Clasificacions.Add(tblClasificacion);
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }

    public int Modificar()
    {
        var clasificacion = (from x in oCine.Clasificacions
                             where x.IdClasificacion == tblClasificacion.IdClasificacion
                             select x).FirstOrDefault();

        if (clasificacion == null)
        {
            return -2;
        }

        clasificacion.Codigo = tblClasificacion.Codigo;
        clasificacion.Descripcion = tblClasificacion.Descripcion;
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }
}
