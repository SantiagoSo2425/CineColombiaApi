using System.Collections.Generic;
using System.Linq;
using apiCine.Clases;
using CineColombiaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiCine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolController : ControllerBase
{
    private readonly CineColombiaContext oCine;

    public RolController(CineColombiaContext oCine)
    {
        this.oCine = oCine;
    }

    [HttpGet]
    public List<Rol> ListarRoles()
    {
        clsOpeRol oRol = new clsOpeRol(oCine);
        return oRol.ListarRoles();
    }

    [HttpGet("{idRol}")]
    public IQueryable ConsultarRol(int idRol)
    {
        clsOpeRol oRol = new clsOpeRol(oCine);
        return oRol.ConsultarRol(idRol);
    }

    [HttpPost]
    public int Agregar([FromBody] Rol rol)
    {
        clsOpeRol oRol = new clsOpeRol(oCine);
        oRol.tblRol = rol;
        return oRol.Agregar();
    }

    [HttpPut]
    public int Modificar([FromBody] Rol rol)
    {
        clsOpeRol oRol = new clsOpeRol(oCine);
        oRol.tblRol = rol;
        return oRol.Modificar();
    }
}
