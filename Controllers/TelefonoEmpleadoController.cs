using System.Collections.Generic;
using System.Linq;
using apiCine.Clases;
using CineColombiaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiCine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TelefonoEmpleadoController : ControllerBase
{
    private readonly CineColombiaContext oCine;

    public TelefonoEmpleadoController(CineColombiaContext oCine)
    {
        this.oCine = oCine;
    }

    [HttpGet]
    public List<TelefonoEmpleado> ListarTelefonosEmpleado()
    {
        clsOpeTelefonoEmpleado oTelefono = new clsOpeTelefonoEmpleado(oCine);
        return oTelefono.ListarTelefonosEmpleado();
    }

    [HttpGet("{idTelefonoEmp}")]
    public IQueryable ConsultarTelefonoEmpleado(int idTelefonoEmp)
    {
        clsOpeTelefonoEmpleado oTelefono = new clsOpeTelefonoEmpleado(oCine);
        return oTelefono.ConsultarTelefonoEmpleado(idTelefonoEmp);
    }

    [HttpPost]
    public int Agregar([FromBody] TelefonoEmpleado telefonoEmpleado)
    {
        clsOpeTelefonoEmpleado oTelefono = new clsOpeTelefonoEmpleado(oCine);
        oTelefono.tblTelefonoEmpleado = telefonoEmpleado;
        return oTelefono.Agregar();
    }

    [HttpPut]
    public int Modificar([FromBody] TelefonoEmpleado telefonoEmpleado)
    {
        clsOpeTelefonoEmpleado oTelefono = new clsOpeTelefonoEmpleado(oCine);
        oTelefono.tblTelefonoEmpleado = telefonoEmpleado;
        return oTelefono.Modificar();
    }
}
