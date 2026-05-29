using System.Collections.Generic;
using System.Linq;
using apiCine.Clases;
using apiSIA.Models;
using CineColombiaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiCine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TipoClienteController : ControllerBase
{
    private readonly CineColombiaContext oCine;

    public TipoClienteController(CineColombiaContext oCine)
    {
        this.oCine = oCine;
    }

    [HttpGet]
    public List<TipoCliente> ListarTipoClientes()
    {
        clsOpeTipoCliente oTipoCliente = new clsOpeTipoCliente(oCine);
        return oTipoCliente.ListarTipoClientes();
    }

    [HttpGet("{idTipoCliente}")]
    public IQueryable ConsultarTipoCliente(int idTipoCliente)
    {
        clsOpeTipoCliente oTipoCliente = new clsOpeTipoCliente(oCine);
        return oTipoCliente.ConsultarTipoCliente(idTipoCliente);
    }

    [HttpPost]
    public int Agregar([FromBody] TipoCliente tipoCliente)
    {
        clsOpeTipoCliente oTipoCliente = new clsOpeTipoCliente(oCine);
        oTipoCliente.tblTipoCliente = tipoCliente;
        return oTipoCliente.Agregar();
    }

    [HttpPut]
    public int Modificar([FromBody] TipoCliente tipoCliente)
    {
        clsOpeTipoCliente oTipoCliente = new clsOpeTipoCliente(oCine);
        oTipoCliente.tblTipoCliente = tipoCliente;
        return oTipoCliente.Modificar();
    }
}
