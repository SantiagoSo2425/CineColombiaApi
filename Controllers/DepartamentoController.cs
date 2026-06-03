using System.Collections.Generic;
using System.Linq;
using apiCine.Clases;
using CineColombiaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiCine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepartamentoController : ControllerBase
{
    private readonly CineColombiaContext oCine;

    public DepartamentoController(CineColombiaContext oCine)
    {
        this.oCine = oCine;
    }

    [HttpGet]
    public IActionResult ListarDepartamentos([FromQuery] int page = 1, [FromQuery] int pageSize = 50)
    {
        clsOpeDepartamento oDepartamento = new clsOpeDepartamento(oCine);
        return Ok(oDepartamento.ListarDepartamentos(page, pageSize));
    }

    [HttpGet("{idDepartamento}")]
    public IActionResult ConsultarDepartamento(int idDepartamento)
    {
        clsOpeDepartamento oDepartamento = new clsOpeDepartamento(oCine);
        return Ok(oDepartamento.ConsultarDepartamento(idDepartamento));
    }

    [HttpPost]
    public IActionResult Agregar([FromBody] Departamento departamento)
    {
        clsOpeDepartamento oDepartamento = new clsOpeDepartamento(oCine);
        oDepartamento.tblDepartamento = departamento;
        var resultado = oDepartamento.Agregar();

        if (resultado == 1)
        {
            return CreatedAtAction(nameof(ConsultarDepartamento), new { idDepartamento = departamento.IdDepartamento }, departamento);
        }

        if (resultado == -1)
        {
            return Conflict();
        }

        return BadRequest();
    }

    [HttpPut]
    public IActionResult Modificar([FromBody] Departamento departamento)
    {
        clsOpeDepartamento oDepartamento = new clsOpeDepartamento(oCine);
        oDepartamento.tblDepartamento = departamento;
        var resultado = oDepartamento.Modificar();

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
