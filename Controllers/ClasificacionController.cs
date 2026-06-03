using System.Collections.Generic;
using System.Linq;
using apiCine.Clases;
using CineColombiaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiCine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClasificacionController : ControllerBase
{
    private readonly CineColombiaContext oCine;

    public ClasificacionController(CineColombiaContext oCine)
    {
        this.oCine = oCine;
    }

    [HttpGet]
    public IActionResult ListarClasificaciones([FromQuery] int page = 1, [FromQuery] int pageSize = 50)
    {
        clsOpeClasificacion oClasificacion = new clsOpeClasificacion(oCine);
        return Ok(oClasificacion.ListarClasificaciones(page, pageSize));
    }

    [HttpGet("{idClasificacion}")]
    public IActionResult ConsultarClasificacion(int idClasificacion)
    {
        clsOpeClasificacion oClasificacion = new clsOpeClasificacion(oCine);
        return Ok(oClasificacion.ConsultarClasificacion(idClasificacion));
    }

    [HttpPost]
    public IActionResult Agregar([FromBody] Clasificacion clasificacion)
    {
        clsOpeClasificacion oClasificacion = new clsOpeClasificacion(oCine);
        oClasificacion.tblClasificacion = clasificacion;
        var resultado = oClasificacion.Agregar();

        if (resultado == 1)
        {
            return CreatedAtAction(nameof(ConsultarClasificacion), new { idClasificacion = clasificacion.IdClasificacion }, clasificacion);
        }

        if (resultado == -1)
        {
            return Conflict();
        }

        return BadRequest();
    }

    [HttpPut]
    public IActionResult Modificar([FromBody] Clasificacion clasificacion)
    {
        clsOpeClasificacion oClasificacion = new clsOpeClasificacion(oCine);
        oClasificacion.tblClasificacion = clasificacion;
        var resultado = oClasificacion.Modificar();

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
