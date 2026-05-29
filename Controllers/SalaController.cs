using System.Collections.Generic;
using System.Linq;
using apiCine.Clases;
using apiSIA.Models;
using CineColombiaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiCine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SalaController : ControllerBase
{
    private readonly CineColombiaContext oCine;

    public SalaController(CineColombiaContext oCine)
    {
        this.oCine = oCine;
    }

    [HttpGet]
    public List<Sala> ListarSalas()
    {
        clsOpeSala oSala = new clsOpeSala(oCine);
        return oSala.ListarSalas();
    }

    [HttpGet("{idSala}")]
    public IQueryable ConsultarSala(int idSala)
    {
        clsOpeSala oSala = new clsOpeSala(oCine);
        return oSala.ConsultarSala(idSala);
    }

    [HttpPost]
    public int Agregar([FromBody] Sala sala)
    {
        clsOpeSala oSala = new clsOpeSala(oCine);
        oSala.tblSala = sala;
        return oSala.Agregar();
    }

    [HttpPut]
    public int Modificar([FromBody] Sala sala)
    {
        clsOpeSala oSala = new clsOpeSala(oCine);
        oSala.tblSala = sala;
        return oSala.Modificar();
    }
}
