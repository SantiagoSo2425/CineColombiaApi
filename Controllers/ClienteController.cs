using System.Collections.Generic;
using System.Linq;
using apiCine.Clases;
using apiSIA.Models;
using CineColombiaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiCine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClienteController : ControllerBase
{
    private readonly CineColombiaContext oCine;

    public ClienteController(CineColombiaContext oCine)
    {
        this.oCine = oCine;
    }

    [HttpGet]
    public List<Cliente> ListarClientes()
    {
        clsOpeCliente oCliente = new clsOpeCliente(oCine);
        return oCliente.ListarClientes();
    }

    [HttpGet("{idCliente}")]
    public IQueryable ConsultarCliente(int idCliente)
    {
        clsOpeCliente oCliente = new clsOpeCliente(oCine);
        return oCliente.ConsultarCliente(idCliente);
    }

    [HttpPost]
    public int Agregar([FromBody] Cliente cliente)
    {
        clsOpeCliente oCliente = new clsOpeCliente(oCine);
        oCliente.tblCliente = cliente;
        return oCliente.Agregar();
    }

    [HttpPut]
    public int Modificar([FromBody] Cliente cliente)
    {
        clsOpeCliente oCliente = new clsOpeCliente(oCine);
        oCliente.tblCliente = cliente;
        return oCliente.Modificar();
    }
}
