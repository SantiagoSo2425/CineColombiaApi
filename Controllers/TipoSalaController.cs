using System.Collections.Generic;
using System.Linq;
using apiCine.Clases;
using CineColombiaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiCine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TipoSalaController : ControllerBase
{
    private readonly CineColombiaContext oCine;

    public TipoSalaController(CineColombiaContext oCine)
    {
        this.oCine = oCine;
    }

    [HttpGet]
    public List<TipoSala> ListarTipoSalas()
    {
        clsOpeTipoSala oTipoSala = new clsOpeTipoSala(oCine);
        return oTipoSala.ListarTipoSalas();
    }

    [HttpGet("{idTipoSala}")]
    public IQueryable ConsultarTipoSala(int idTipoSala)
    {
        clsOpeTipoSala oTipoSala = new clsOpeTipoSala(oCine);
        return oTipoSala.ConsultarTipoSala(idTipoSala);
    }

    [HttpPost]
    public int Agregar([FromBody] TipoSala tipoSala)
    {
        clsOpeTipoSala oTipoSala = new clsOpeTipoSala(oCine);
        oTipoSala.tblTipoSala = tipoSala;
        return oTipoSala.Agregar();
    }

    [HttpPut]
    public int Modificar([FromBody] TipoSala tipoSala)
    {
        clsOpeTipoSala oTipoSala = new clsOpeTipoSala(oCine);
        oTipoSala.tblTipoSala = tipoSala;
        return oTipoSala.Modificar();
    }
}
