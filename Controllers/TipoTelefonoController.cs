using System.Collections.Generic;
using System.Linq;
using apiCine.Clases;
using CineColombiaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiCine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TipoTelefonoController : ControllerBase
{
    private readonly CineColombiaContext oCine;

    public TipoTelefonoController(CineColombiaContext oCine)
    {
        this.oCine = oCine;
    }

    [HttpGet]
    public List<TipoTelefono> ListarTipoTelefonos()
    {
        clsOpeTipoTelefono oTipoTelefono = new clsOpeTipoTelefono(oCine);
        return oTipoTelefono.ListarTipoTelefonos();
    }

    [HttpGet("{idTipoTelefono}")]
    public IQueryable ConsultarTipoTelefono(int idTipoTelefono)
    {
        clsOpeTipoTelefono oTipoTelefono = new clsOpeTipoTelefono(oCine);
        return oTipoTelefono.ConsultarTipoTelefono(idTipoTelefono);
    }

    [HttpPost]
    public int Agregar([FromBody] TipoTelefono tipoTelefono)
    {
        clsOpeTipoTelefono oTipoTelefono = new clsOpeTipoTelefono(oCine);
        oTipoTelefono.tblTipoTelefono = tipoTelefono;
        return oTipoTelefono.Agregar();
    }

    [HttpPut]
    public int Modificar([FromBody] TipoTelefono tipoTelefono)
    {
        clsOpeTipoTelefono oTipoTelefono = new clsOpeTipoTelefono(oCine);
        oTipoTelefono.tblTipoTelefono = tipoTelefono;
        return oTipoTelefono.Modificar();
    }
}
