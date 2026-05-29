using System.Collections.Generic;
using System.Linq;
using apiCine.Clases;
using CineColombiaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiCine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TipoSillaController : ControllerBase
{
    private readonly CineColombiaContext oCine;

    public TipoSillaController(CineColombiaContext oCine)
    {
        this.oCine = oCine;
    }

    [HttpGet]
    public List<TipoSilla> ListarTipoSillas()
    {
        clsOpeTipoSilla oTipoSilla = new clsOpeTipoSilla(oCine);
        return oTipoSilla.ListarTipoSillas();
    }

    [HttpGet("{idTipoSilla}")]
    public IQueryable ConsultarTipoSilla(int idTipoSilla)
    {
        clsOpeTipoSilla oTipoSilla = new clsOpeTipoSilla(oCine);
        return oTipoSilla.ConsultarTipoSilla(idTipoSilla);
    }

    [HttpPost]
    public int Agregar([FromBody] TipoSilla tipoSilla)
    {
        clsOpeTipoSilla oTipoSilla = new clsOpeTipoSilla(oCine);
        oTipoSilla.tblTipoSilla = tipoSilla;
        return oTipoSilla.Agregar();
    }

    [HttpPut]
    public int Modificar([FromBody] TipoSilla tipoSilla)
    {
        clsOpeTipoSilla oTipoSilla = new clsOpeTipoSilla(oCine);
        oTipoSilla.tblTipoSilla = tipoSilla;
        return oTipoSilla.Modificar();
    }
}
