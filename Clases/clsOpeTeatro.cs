using System.Collections.Generic;
using System.Linq;
using CineColombiaApi.Models;

namespace apiCine.Clases;

public class clsOpeTeatro
{
    private readonly CineColombiaContext oCine;
    public Teatro tblTeatro { get; set; }

    public clsOpeTeatro(CineColombiaContext oCine)
    {
        this.oCine = oCine;
        tblTeatro = new Teatro();
    }

    public List<Teatro> ListarTeatros()
    {
        return oCine.Teatros.ToList();
    }

    public IQueryable ConsultarTeatro(int idTeatro)
    {
        return from x in oCine.Teatros
               where x.IdTeatro == idTeatro
               select x;
    }

    public int Agregar()
    {
        oCine.Teatros.Add(tblTeatro);
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }

    public int Modificar()
    {
        var teatro = (from x in oCine.Teatros
                      where x.IdTeatro == tblTeatro.IdTeatro
                      select x).FirstOrDefault();

        if (teatro == null)
        {
            return -2;
        }

        teatro.IdCiudad = tblTeatro.IdCiudad;
        teatro.Nombre = tblTeatro.Nombre;
        teatro.Direccion = tblTeatro.Direccion;
        teatro.Telefono = tblTeatro.Telefono;
        teatro.Email = tblTeatro.Email;
        teatro.Activo = tblTeatro.Activo;
        teatro.RegistradoPor = tblTeatro.RegistradoPor;
        teatro.FechaRegistro = tblTeatro.FechaRegistro;

        return oCine.SaveChanges() > 0 ? 1 : 0;
    }

    public int Inactivar(int idTeatro)
    {
        var teatro = (from x in oCine.Teatros
                      where x.IdTeatro == idTeatro
                      select x).FirstOrDefault();

        if (teatro == null)
        {
            return -2;
        }

        teatro.Activo = false;
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }
}
