using System.Collections.Generic;
using System.Linq;
using apiCine.Clases;
using CineColombiaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiCine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TelefonoClienteController : ControllerBase
{
    private readonly CineColombiaContext oCine;

    public TelefonoClienteController(CineColombiaContext oCine)
    {
        this.oCine = oCine;
    }

    [HttpGet]
    public List<TelefonoCliente> ListarTelefonosCliente()
    {
        clsOpeTelefonoCliente oTelefono = new clsOpeTelefonoCliente(oCine);
        return oTelefono.ListarTelefonosCliente();
    }

    [HttpGet("{idTelefono}")]
    public IQueryable ConsultarTelefonoCliente(int idTelefono)
    {
        clsOpeTelefonoCliente oTelefono = new clsOpeTelefonoCliente(oCine);
        return oTelefono.ConsultarTelefonoCliente(idTelefono);
    }

    [HttpPost]
    public int Agregar([FromBody] TelefonoCliente telefonoCliente)
    {
        clsOpeTelefonoCliente oTelefono = new clsOpeTelefonoCliente(oCine);
        oTelefono.tblTelefonoCliente = telefonoCliente;
        return oTelefono.Agregar();
    }

    [HttpPut]
    public int Modificar([FromBody] TelefonoCliente telefonoCliente)
    {
        clsOpeTelefonoCliente oTelefono = new clsOpeTelefonoCliente(oCine);
        oTelefono.tblTelefonoCliente = telefonoCliente;
        return oTelefono.Modificar();
    }
}
