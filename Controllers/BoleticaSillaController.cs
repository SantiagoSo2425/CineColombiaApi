using System.Collections.Generic;
using System.Linq;
using apiCine.Clases;
using apiSIA.Models;
using CineColombiaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiCine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BoleticaSillaController : ControllerBase
{
    private readonly CineColombiaContext oCine;

    public BoleticaSillaController(CineColombiaContext oCine)
    {
        this.oCine = oCine;
    }

    [HttpGet]
    public List<BoleticaSilla> ListarBoleticaSillas()
    {
        clsOpeBoleticaSilla oBoleticaSilla = new clsOpeBoleticaSilla(oCine);
        return oBoleticaSilla.ListarBoleticaSillas();
    }

    [HttpGet("{idBoleticaSilla}")]
    public IQueryable ConsultarBoleticaSilla(int idBoleticaSilla)
    {
        clsOpeBoleticaSilla oBoleticaSilla = new clsOpeBoleticaSilla(oCine);
        return oBoleticaSilla.ConsultarBoleticaSilla(idBoleticaSilla);
    }

    [HttpPost]
    public int Agregar([FromBody] BoleticaSilla boleticaSilla)
    {
        clsOpeBoleticaSilla oBoleticaSilla = new clsOpeBoleticaSilla(oCine);
        oBoleticaSilla.tblBoleticaSilla = boleticaSilla;
        return oBoleticaSilla.Agregar();
    }

    [HttpPut]
    public int Modificar([FromBody] BoleticaSilla boleticaSilla)
    {
        clsOpeBoleticaSilla oBoleticaSilla = new clsOpeBoleticaSilla(oCine);
        oBoleticaSilla.tblBoleticaSilla = boleticaSilla;
        return oBoleticaSilla.Modificar();
    }
}
