using System.Collections.Generic;
using System.Linq;
using apiCine.Clases;
using CineColombiaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiCine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IdiomaController : ControllerBase
{
    private readonly CineColombiaContext oCine;

    public IdiomaController(CineColombiaContext oCine)
    {
        this.oCine = oCine;
    }

    [HttpGet]
    public List<Idioma> ListarIdiomas()
    {
        clsOpeIdioma oIdioma = new clsOpeIdioma(oCine);
        return oIdioma.ListarIdiomas();
    }

    [HttpGet("{idIdioma}")]
    public IQueryable ConsultarIdioma(int idIdioma)
    {
        clsOpeIdioma oIdioma = new clsOpeIdioma(oCine);
        return oIdioma.ConsultarIdioma(idIdioma);
    }

    [HttpPost]
    public int Agregar([FromBody] Idioma idioma)
    {
        clsOpeIdioma oIdioma = new clsOpeIdioma(oCine);
        oIdioma.tblIdioma = idioma;
        return oIdioma.Agregar();
    }

    [HttpPut]
    public int Modificar([FromBody] Idioma idioma)
    {
        clsOpeIdioma oIdioma = new clsOpeIdioma(oCine);
        oIdioma.tblIdioma = idioma;
        return oIdioma.Modificar();
    }
}
