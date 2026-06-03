using System.Collections.Generic;
using System.Linq;
using apiCine.Clases;
using CineColombiaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiCine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CiudadController : ControllerBase
{
    private readonly CineColombiaContext oCine;

    public CiudadController(CineColombiaContext oCine)
    {
        this.oCine = oCine;
    }

    [HttpGet]
    public IActionResult ListarCiudades([FromQuery] int page = 1, [FromQuery] int pageSize = 50)
    {
        clsOpeCiudad oCiudad = new clsOpeCiudad(oCine);
        return Ok(oCiudad.ListarCiudades(page, pageSize));
    }

    [HttpGet("{idCiudad}")]
    public IActionResult ConsultarCiudad(int idCiudad)
    {
        clsOpeCiudad oCiudad = new clsOpeCiudad(oCine);
        return Ok(oCiudad.ConsultarCiudad(idCiudad));
    }

    [HttpPost]
    public IActionResult Agregar([FromBody] Ciudad ciudad)
    {
        clsOpeCiudad oCiudad = new clsOpeCiudad(oCine);
        oCiudad.tblCiudad = ciudad;
        var resultado = oCiudad.Agregar();

        if (resultado == 1)
        {
            return CreatedAtAction(nameof(ConsultarCiudad), new { idCiudad = ciudad.IdCiudad }, ciudad);
        }

        if (resultado == -1)
        {
            return Conflict();
        }

        return BadRequest();
    }

    [HttpPut]
    public IActionResult Modificar([FromBody] Ciudad ciudad)
    {
        clsOpeCiudad oCiudad = new clsOpeCiudad(oCine);
        oCiudad.tblCiudad = ciudad;
        var resultado = oCiudad.Modificar();

        if (resultado == 1)
        {
            return Ok();
        }

        if (resultado == -2)
        {
            return NotFound();
        }

        return BadRequest();
    }
}
