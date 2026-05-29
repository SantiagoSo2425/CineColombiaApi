using System.Collections.Generic;
using System.Linq;
using apiSIA.Models;
using CineColombiaApi.Models;

namespace apiCine.Clases;

public class clsOpeSala
{
    private readonly CineColombiaContext oCine;
    public Sala tblSala { get; set; }

    public clsOpeSala(CineColombiaContext oCine)
    {
        this.oCine = oCine;
        tblSala = new Sala();
    }

    public List<Sala> ListarSalas()
    {
        return oCine.Salas.ToList();
    }

    public IQueryable ConsultarSala(int idSala)
    {
        return from x in oCine.Salas
               where x.IdSala == idSala
               select x;
    }

    public int Agregar()
    {
        oCine.Salas.Add(tblSala);
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }

    public int Modificar()
    {
        var sala = (from x in oCine.Salas
                    where x.IdSala == tblSala.IdSala
                    select x).FirstOrDefault();

        if (sala == null)
        {
            return -2;
        }

        sala.IdTeatro = tblSala.IdTeatro;
        sala.IdTipoSala = tblSala.IdTipoSala;
        sala.NombreSala = tblSala.NombreSala;
        sala.CapacidadTotal = tblSala.CapacidadTotal;
        sala.Activo = tblSala.Activo;

        return oCine.SaveChanges() > 0 ? 1 : 0;
    }
}
