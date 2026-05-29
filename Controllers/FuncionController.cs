using System.Collections.Generic;
using System.Linq;
using apiCine.Clases;
using CineColombiaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiCine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FuncionController : ControllerBase
{
    private readonly CineColombiaContext oCine;

    public FuncionController(CineColombiaContext oCine)
    {
        this.oCine = oCine;
    }

    [HttpGet]
    public List<Funcion> ListarFunciones()
    {
        clsOpeFuncion oFuncion = new clsOpeFuncion(oCine);
        return oFuncion.ListarFunciones();
    }

    [HttpGet("{idFuncion}")]
    public IQueryable ConsultarFuncion(int idFuncion)
    {
        clsOpeFuncion oFuncion = new clsOpeFuncion(oCine);
        return oFuncion.ConsultarFuncion(idFuncion);
    }

    [HttpPost]
    public int Agregar([FromBody] Funcion funcion)
    {
        clsOpeFuncion oFuncion = new clsOpeFuncion(oCine);
        oFuncion.tblFuncion = funcion;
        return oFuncion.Agregar();
    }

    [HttpPut]
    public int Modificar([FromBody] Funcion funcion)
    {
        clsOpeFuncion oFuncion = new clsOpeFuncion(oCine);
        oFuncion.tblFuncion = funcion;
        return oFuncion.Modificar();
    }

    [HttpPut("inactivar/{idFuncion}")]
    public int Inactivar(int idFuncion)
    {
        clsOpeFuncion oFuncion = new clsOpeFuncion(oCine);
        return oFuncion.Inactivar(idFuncion);
    }
}
