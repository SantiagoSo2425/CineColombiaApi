using System.Collections.Generic;
using System.Linq;
using apiCine.Clases;
using CineColombiaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiCine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmpleadoProfesionController : ControllerBase
{
    private readonly CineColombiaContext oCine;

    public EmpleadoProfesionController(CineColombiaContext oCine)
    {
        this.oCine = oCine;
    }

    [HttpGet]
    public List<EmpleadoProfesion> ListarEmpleadosProfesiones()
    {
        clsOpeEmpleadoProfesion oEmpleadoProfesion = new clsOpeEmpleadoProfesion(oCine);
        return oEmpleadoProfesion.ListarEmpleadosProfesiones();
    }

    [HttpGet("{idEmpProfesion}")]
    public IQueryable ConsultarEmpleadoProfesion(int idEmpProfesion)
    {
        clsOpeEmpleadoProfesion oEmpleadoProfesion = new clsOpeEmpleadoProfesion(oCine);
        return oEmpleadoProfesion.ConsultarEmpleadoProfesion(idEmpProfesion);
    }

    [HttpPost]
    public int Agregar([FromBody] EmpleadoProfesion empleadoProfesion)
    {
        clsOpeEmpleadoProfesion oEmpleadoProfesion = new clsOpeEmpleadoProfesion(oCine);
        oEmpleadoProfesion.tblEmpleadoProfesion = empleadoProfesion;
        return oEmpleadoProfesion.Agregar();
    }

    [HttpPut]
    public int Modificar([FromBody] EmpleadoProfesion empleadoProfesion)
    {
        clsOpeEmpleadoProfesion oEmpleadoProfesion = new clsOpeEmpleadoProfesion(oCine);
        oEmpleadoProfesion.tblEmpleadoProfesion = empleadoProfesion;
        return oEmpleadoProfesion.Modificar();
    }
}
