using System.Collections.Generic;
using System.Linq;
using apiSIA.Models;
using CineColombiaApi.Models;

namespace apiCine.Clases;

public class clsOpeFormato
{
    private readonly CineColombiaContext oCine;
    public Formato tblFormato { get; set; }

    public clsOpeFormato(CineColombiaContext oCine)
    {
        this.oCine = oCine;
        tblFormato = new Formato();
    }

    public List<Formato> ListarFormatos()
    {
        return oCine.Formatos.ToList();
    }

    public IQueryable ConsultarFormato(int idFormato)
    {
        return from x in oCine.Formatos
               where x.IdFormato == idFormato
               select x;
    }

    public int Agregar()
    {
        var existe = (from x in oCine.Formatos
                      where x.IdFormato == tblFormato.IdFormato
                      select x).Any();

        if (existe)
        {
            return -1;
        }

        oCine.Formatos.Add(tblFormato);
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }

    public int Modificar()
    {
        var formato = (from x in oCine.Formatos
                       where x.IdFormato == tblFormato.IdFormato
                       select x).FirstOrDefault();

        if (formato == null)
        {
            return -2;
        }

        formato.Nombre = tblFormato.Nombre;

        return oCine.SaveChanges() > 0 ? 1 : 0;
    }
}
