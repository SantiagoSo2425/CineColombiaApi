using System.Collections.Generic;
using System.Linq;
using CineColombiaApi.Models;

namespace apiCine.Clases;

public class clsOpeSilla
{
    private readonly CineColombiaContext oCine;
    public Silla tblSilla { get; set; }

    public clsOpeSilla(CineColombiaContext oCine)
    {
        this.oCine = oCine;
        tblSilla = new Silla();
    }

    public List<Silla> ListarSillas()
    {
        return oCine.Sillas.ToList();
    }

    public IQueryable ConsultarSilla(int idSilla)
    {
        return from x in oCine.Sillas
               where x.IdSilla == idSilla
               select x;
    }

    public int Agregar()
    {
        var existe = (from x in oCine.Sillas
                      where x.IdSala == tblSilla.IdSala
                      && x.Fila == tblSilla.Fila
                      && x.Numero == tblSilla.Numero
                      select x).Any();

        if (existe)
        {
            return -1;
        }

        oCine.Sillas.Add(tblSilla);
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }

    public int Modificar()
    {
        var silla = (from x in oCine.Sillas
                     where x.IdSilla == tblSilla.IdSilla
                     select x).FirstOrDefault();

        if (silla == null)
        {
            return -2;
        }

        var existe = (from x in oCine.Sillas
                      where x.IdSilla != tblSilla.IdSilla
                      && x.IdSala == tblSilla.IdSala
                      && x.Fila == tblSilla.Fila
                      && x.Numero == tblSilla.Numero
                      select x).Any();

        if (existe)
        {
            return -1;
        }

        silla.IdSala = tblSilla.IdSala;
        silla.IdTipoSilla = tblSilla.IdTipoSilla;
        silla.Fila = tblSilla.Fila;
        silla.Numero = tblSilla.Numero;
        silla.Estado = tblSilla.Estado;
        silla.RegistradoPor = tblSilla.RegistradoPor;
        silla.FechaRegistro = tblSilla.FechaRegistro;

        return oCine.SaveChanges() > 0 ? 1 : 0;
    }

    public int Inactivar(int idSilla)
    {
        var silla = (from x in oCine.Sillas
                     where x.IdSilla == idSilla
                     select x).FirstOrDefault();

        if (silla == null)
        {
            return -2;
        }

        silla.Estado = 0;
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }
}
