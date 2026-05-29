using System.Collections.Generic;
using System.Linq;
using apiCine.Clases;
using CineColombiaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiCine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DireccionEmpleadoController : ControllerBase
{
    private readonly CineColombiaContext oCine;

    public DireccionEmpleadoController(CineColombiaContext oCine)
    {
        this.oCine = oCine;
    }

    [HttpGet]
    public List<DireccionEmpleado> ListarDireccionesEmpleado()
    {
        clsOpeDireccionEmpleado oDireccion = new clsOpeDireccionEmpleado(oCine);
        return oDireccion.ListarDireccionesEmpleado();
    }

    [HttpGet("{idDireccionEmp}")]
    public IQueryable ConsultarDireccionEmpleado(int idDireccionEmp)
    {
        clsOpeDireccionEmpleado oDireccion = new clsOpeDireccionEmpleado(oCine);
        return oDireccion.ConsultarDireccionEmpleado(idDireccionEmp);
    }

    [HttpPost]
    public int Agregar([FromBody] DireccionEmpleado direccionEmpleado)
    {
        clsOpeDireccionEmpleado oDireccion = new clsOpeDireccionEmpleado(oCine);
        oDireccion.tblDireccionEmpleado = direccionEmpleado;
        return oDireccion.Agregar();
    }

    [HttpPut]
    public int Modificar([FromBody] DireccionEmpleado direccionEmpleado)
    {
        clsOpeDireccionEmpleado oDireccion = new clsOpeDireccionEmpleado(oCine);
        oDireccion.tblDireccionEmpleado = direccionEmpleado;
        return oDireccion.Modificar();
    }

    [HttpPut("inactivar/{idDireccionEmp}")]
    public int Inactivar(int idDireccionEmp)
    {
        clsOpeDireccionEmpleado oDireccion = new clsOpeDireccionEmpleado(oCine);
        return oDireccion.Inactivar(idDireccionEmp);
    }
}
