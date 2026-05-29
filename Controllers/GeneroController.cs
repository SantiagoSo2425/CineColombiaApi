using System.Collections.Generic;
using System.Linq;
using apiCine.Clases;
using CineColombiaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiCine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GeneroController : ControllerBase
{
    private readonly CineColombiaContext oCine;

    public GeneroController(CineColombiaContext oCine)
    {
        this.oCine = oCine;
    }

    [HttpGet]
    public List<Genero> ListarGeneros()
    {
        clsOpeGenero oGenero = new clsOpeGenero(oCine);
        return oGenero.ListarGeneros();
    }

    [HttpGet("{idGenero}")]
    public IQueryable ConsultarGenero(int idGenero)
    {
        clsOpeGenero oGenero = new clsOpeGenero(oCine);
        return oGenero.ConsultarGenero(idGenero);
    }

    [HttpPost]
    public int Agregar([FromBody] Genero genero)
    {
        clsOpeGenero oGenero = new clsOpeGenero(oCine);
        oGenero.tblGenero = genero;
        return oGenero.Agregar();
    }

    [HttpPut]
    public int Modificar([FromBody] Genero genero)
    {
        clsOpeGenero oGenero = new clsOpeGenero(oCine);
        oGenero.tblGenero = genero;
        return oGenero.Modificar();
    }
}
