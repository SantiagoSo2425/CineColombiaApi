using System.Collections.Generic;
using System.Linq;
using apiCine.Clases;
using apiSIA.Models;
using CineColombiaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiCine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BoleticaController : ControllerBase
{
    private readonly CineColombiaContext oCine;

    public BoleticaController(CineColombiaContext oCine)
    {
        this.oCine = oCine;
    }

    [HttpGet]
    public List<Boletica> ListarBoleticas()
    {
        clsOpeBoletica oBoletica = new clsOpeBoletica(oCine);
        return oBoletica.ListarBoleticas();
    }

    [HttpGet("{idBoletica}")]
    public IQueryable ConsultarBoletica(int idBoletica)
    {
        clsOpeBoletica oBoletica = new clsOpeBoletica(oCine);
        return oBoletica.ConsultarBoletica(idBoletica);
    }

    [HttpPost]
    public int Agregar([FromBody] Boletica boletica)
    {
        clsOpeBoletica oBoletica = new clsOpeBoletica(oCine);
        oBoletica.tblBoletica = boletica;
        return oBoletica.Agregar();
    }

    [HttpPut]
    public int Modificar([FromBody] Boletica boletica)
    {
        clsOpeBoletica oBoletica = new clsOpeBoletica(oCine);
        oBoletica.tblBoletica = boletica;
        return oBoletica.Modificar();
    }
}
