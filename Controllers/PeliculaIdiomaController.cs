using System.Collections.Generic;
using System.Linq;
using apiCine.Clases;
using CineColombiaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiCine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PeliculaIdiomaController : ControllerBase
{
    private readonly CineColombiaContext oCine;

    public PeliculaIdiomaController(CineColombiaContext oCine)
    {
        this.oCine = oCine;
    }

    [HttpGet]
    public List<PeliculaIdioma> ListarPeliculasIdioma()
    {
        clsOpePeliculaIdioma oPeliculaIdioma = new clsOpePeliculaIdioma(oCine);
        return oPeliculaIdioma.ListarPeliculasIdioma();
    }

    [HttpGet("{idPeliculaIdioma}")]
    public IQueryable ConsultarPeliculaIdioma(int idPeliculaIdioma)
    {
        clsOpePeliculaIdioma oPeliculaIdioma = new clsOpePeliculaIdioma(oCine);
        return oPeliculaIdioma.ConsultarPeliculaIdioma(idPeliculaIdioma);
    }

    [HttpPost]
    public int Agregar([FromBody] PeliculaIdioma peliculaIdioma)
    {
        clsOpePeliculaIdioma oPeliculaIdioma = new clsOpePeliculaIdioma(oCine);
        oPeliculaIdioma.tblPeliculaIdioma = peliculaIdioma;
        return oPeliculaIdioma.Agregar();
    }

    [HttpPut]
    public int Modificar([FromBody] PeliculaIdioma peliculaIdioma)
    {
        clsOpePeliculaIdioma oPeliculaIdioma = new clsOpePeliculaIdioma(oCine);
        oPeliculaIdioma.tblPeliculaIdioma = peliculaIdioma;
        return oPeliculaIdioma.Modificar();
    }
}
