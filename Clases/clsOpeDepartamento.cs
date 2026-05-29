using System.Collections.Generic;
using System.Linq;
using CineColombiaApi.Models;

namespace apiCine.Clases;

public class clsOpeDepartamento
{
    private readonly CineColombiaContext oCine;
    public Departamento tblDepartamento { get; set; }

    public clsOpeDepartamento(CineColombiaContext oCine)
    {
        this.oCine = oCine;
        tblDepartamento = new Departamento();
    }

    public List<Departamento> ListarDepartamentos()
    {
        return oCine.Departamentos.ToList();
    }

    public IQueryable ConsultarDepartamento(int idDepartamento)
    {
        return from x in oCine.Departamentos
               where x.IdDepartamento == idDepartamento
               select x;
    }

    public int Agregar()
    {
        var existe = (from x in oCine.Departamentos
                      where x.IdDepartamento == tblDepartamento.IdDepartamento
                      select x).Any();

        if (existe)
        {
            return -1;
        }

        oCine.Departamentos.Add(tblDepartamento);
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }

    public int Modificar()
    {
        var departamento = (from x in oCine.Departamentos
                            where x.IdDepartamento == tblDepartamento.IdDepartamento
                            select x).FirstOrDefault();

        if (departamento == null)
        {
            return -2;
        }

        departamento.IdPais = tblDepartamento.IdPais;
        departamento.Nombre = tblDepartamento.Nombre;
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }
}
