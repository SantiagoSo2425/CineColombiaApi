using System.Collections.Generic;
using System.Linq;
using apiCine.Clases;
using CineColombiaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiCine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DireccionClienteController : ControllerBase
{
    private readonly CineColombiaContext oCine;

    public DireccionClienteController(CineColombiaContext oCine)
    {
        this.oCine = oCine;
    }

    [HttpGet]
    public List<DireccionCliente> ListarDireccionesCliente()
    {
        clsOpeDireccionCliente oDireccion = new clsOpeDireccionCliente(oCine);
        return oDireccion.ListarDireccionesCliente();
    }

    [HttpGet("{idDireccionCli}")]
    public IQueryable ConsultarDireccionCliente(int idDireccionCli)
    {
        clsOpeDireccionCliente oDireccion = new clsOpeDireccionCliente(oCine);
        return oDireccion.ConsultarDireccionCliente(idDireccionCli);
    }

    [HttpPost]
    public int Agregar([FromBody] DireccionCliente direccionCliente)
    {
        clsOpeDireccionCliente oDireccion = new clsOpeDireccionCliente(oCine);
        oDireccion.tblDireccionCliente = direccionCliente;
        return oDireccion.Agregar();
    }

    [HttpPut]
    public int Modificar([FromBody] DireccionCliente direccionCliente)
    {
        clsOpeDireccionCliente oDireccion = new clsOpeDireccionCliente(oCine);
        oDireccion.tblDireccionCliente = direccionCliente;
        return oDireccion.Modificar();
    }

    [HttpPut("inactivar/{idDireccionCli}")]
    public int Inactivar(int idDireccionCli)
    {
        clsOpeDireccionCliente oDireccion = new clsOpeDireccionCliente(oCine);
        return oDireccion.Inactivar(idDireccionCli);
    }
}
