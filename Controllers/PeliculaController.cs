using System.Collections.Generic;
using System.Linq;
using apiCine.Clases;
using apiSIA.Models;
using CineColombiaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiCine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PeliculaController : ControllerBase
{
    private readonly CineColombiaContext oCine;

    public PeliculaController(CineColombiaContext oCine)
    {
        this.oCine = oCine;
    }

    [HttpGet]
    public List<Pelicula> ListarPeliculas()
    {
        clsOpePelicula oPelicula = new clsOpePelicula(oCine);
        return oPelicula.ListarPeliculas();
    }

    [HttpGet("{idPelicula}")]
    public IQueryable ConsultarPelicula(int idPelicula)
    {
        clsOpePelicula oPelicula = new clsOpePelicula(oCine);
        return oPelicula.ConsultarPelicula(idPelicula);
    }

    [HttpPost]
    public int Agregar([FromBody] Pelicula pelicula)
    {
        clsOpePelicula oPelicula = new clsOpePelicula(oCine);
        oPelicula.tblPelicula = pelicula;
        return oPelicula.Agregar();
    }

    [HttpPut]
    public int Modificar([FromBody] Pelicula pelicula)
    {
        clsOpePelicula oPelicula = new clsOpePelicula(oCine);
        oPelicula.tblPelicula = pelicula;
        return oPelicula.Modificar();
    }
}
