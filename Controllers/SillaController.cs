using System.Collections.Generic;
using System.Linq;
using apiCine.Clases;
using apiSIA.Models;
using CineColombiaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiCine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SillaController : ControllerBase
{
    private readonly CineColombiaContext oCine;

    public SillaController(CineColombiaContext oCine)
    {
        this.oCine = oCine;
    }

    [HttpGet]
    public List<Silla> ListarSillas()
    {
        clsOpeSilla oSilla = new clsOpeSilla(oCine);
        return oSilla.ListarSillas();
    }

    [HttpGet("{idSilla}")]
    public IQueryable ConsultarSilla(int idSilla)
    {
        clsOpeSilla oSilla = new clsOpeSilla(oCine);
        return oSilla.ConsultarSilla(idSilla);
    }

    [HttpPost]
    public int Agregar([FromBody] Silla silla)
    {
        clsOpeSilla oSilla = new clsOpeSilla(oCine);
        oSilla.tblSilla = silla;
        return oSilla.Agregar();
    }

    [HttpPut]
    public int Modificar([FromBody] Silla silla)
    {
        clsOpeSilla oSilla = new clsOpeSilla(oCine);
        oSilla.tblSilla = silla;
        return oSilla.Modificar();
    }
}
