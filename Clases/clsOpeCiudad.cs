using System.Collections.Generic;
using System.Linq;
using CineColombiaApi.Models;

namespace apiCine.Clases;

public class clsOpeCiudad
{
    private readonly CineColombiaContext oCine;
    public Ciudad tblCiudad { get; set; }

    public clsOpeCiudad(CineColombiaContext oCine)
    {
        this.oCine = oCine;
        tblCiudad = new Ciudad();
    }

    public List<Ciudad> ListarCiudades()
    {
        return oCine.Ciudads.ToList();
    }

    public IQueryable ConsultarCiudad(int idCiudad)
    {
        return from x in oCine.Ciudads
               where x.IdCiudad == idCiudad
               select x;
    }

    public int Agregar()
    {
        var existe = (from x in oCine.Ciudads
                      where x.IdCiudad == tblCiudad.IdCiudad
                      select x).Any();

        if (existe)
        {
            return -1;
        }

        oCine.Ciudads.Add(tblCiudad);
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }

    public int Modificar()
    {
        var ciudad = (from x in oCine.Ciudads
                      where x.IdCiudad == tblCiudad.IdCiudad
                      select x).FirstOrDefault();

        if (ciudad == null)
        {
            return -2;
        }

        ciudad.IdDepartamento = tblCiudad.IdDepartamento;
        ciudad.Nombre = tblCiudad.Nombre;
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }
}
