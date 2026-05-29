using System.Collections.Generic;
using System.Linq;
using apiCine.Clases;
using CineColombiaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiCine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmpleadoController : ControllerBase
{
    private readonly CineColombiaContext oCine;

    public EmpleadoController(CineColombiaContext oCine)
    {
        this.oCine = oCine;
    }

    [HttpGet]
    public List<Empleado> ListarEmpleados()
    {
        clsOpeEmpleado oEmpleado = new clsOpeEmpleado(oCine);
        return oEmpleado.ListarEmpleados();
    }

    [HttpGet("{idEmpleado}")]
    public IQueryable ConsultarEmpleado(int idEmpleado)
    {
        clsOpeEmpleado oEmpleado = new clsOpeEmpleado(oCine);
        return oEmpleado.ConsultarEmpleado(idEmpleado);
    }

    [HttpPost]
    public int Agregar([FromBody] Empleado empleado)
    {
        clsOpeEmpleado oEmpleado = new clsOpeEmpleado(oCine);
        oEmpleado.tblEmpleado = empleado;
        return oEmpleado.Agregar();
    }

    [HttpPut]
    public int Modificar([FromBody] Empleado empleado)
    {
        clsOpeEmpleado oEmpleado = new clsOpeEmpleado(oCine);
        oEmpleado.tblEmpleado = empleado;
        return oEmpleado.Modificar();
    }

    [HttpPut("inactivar/{idEmpleado}")]
    public int Inactivar(int idEmpleado)
    {
        clsOpeEmpleado oEmpleado = new clsOpeEmpleado(oCine);
        return oEmpleado.Inactivar(idEmpleado);
    }
}
