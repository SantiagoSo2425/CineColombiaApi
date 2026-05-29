using System.Collections.Generic;
using System.Linq;
using apiCine.Clases;
using CineColombiaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiCine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepartamentoController : ControllerBase
{
    private readonly CineColombiaContext oCine;

    public DepartamentoController(CineColombiaContext oCine)
    {
        this.oCine = oCine;
    }

    [HttpGet]
    public List<Departamento> ListarDepartamentos()
    {
        clsOpeDepartamento oDepartamento = new clsOpeDepartamento(oCine);
        return oDepartamento.ListarDepartamentos();
    }

    [HttpGet("{idDepartamento}")]
    public IQueryable ConsultarDepartamento(int idDepartamento)
    {
        clsOpeDepartamento oDepartamento = new clsOpeDepartamento(oCine);
        return oDepartamento.ConsultarDepartamento(idDepartamento);
    }

    [HttpPost]
    public int Agregar([FromBody] Departamento departamento)
    {
        clsOpeDepartamento oDepartamento = new clsOpeDepartamento(oCine);
        oDepartamento.tblDepartamento = departamento;
        return oDepartamento.Agregar();
    }

    [HttpPut]
    public int Modificar([FromBody] Departamento departamento)
    {
        clsOpeDepartamento oDepartamento = new clsOpeDepartamento(oCine);
        oDepartamento.tblDepartamento = departamento;
        return oDepartamento.Modificar();
    }
}
