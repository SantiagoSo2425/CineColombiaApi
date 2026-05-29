using System.Collections.Generic;
using System.Linq;
using apiCine.Clases;
using CineColombiaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiCine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeatroController : ControllerBase
{
    private readonly CineColombiaContext oCine;

    public TeatroController(CineColombiaContext oCine)
    {
        this.oCine = oCine;
    }

    [HttpGet]
    public List<Teatro> ListarTeatros()
    {
        clsOpeTeatro oTeatro = new clsOpeTeatro(oCine);
        return oTeatro.ListarTeatros();
    }

    [HttpGet("{idTeatro}")]
    public IQueryable ConsultarTeatro(int idTeatro)
    {
        clsOpeTeatro oTeatro = new clsOpeTeatro(oCine);
        return oTeatro.ConsultarTeatro(idTeatro);
    }

    [HttpPost]
    public int Agregar([FromBody] Teatro teatro)
    {
        clsOpeTeatro oTeatro = new clsOpeTeatro(oCine);
        oTeatro.tblTeatro = teatro;
        return oTeatro.Agregar();
    }

    [HttpPut]
    public int Modificar([FromBody] Teatro teatro)
    {
        clsOpeTeatro oTeatro = new clsOpeTeatro(oCine);
        oTeatro.tblTeatro = teatro;
        return oTeatro.Modificar();
    }

    [HttpPut("inactivar/{idTeatro}")]
    public int Inactivar(int idTeatro)
    {
        clsOpeTeatro oTeatro = new clsOpeTeatro(oCine);
        return oTeatro.Inactivar(idTeatro);
    }
}
