using System.Collections.Generic;
using System.Linq;
using apiCine.Clases;
using CineColombiaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiCine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PeliculaProductoraController : ControllerBase
{
    private readonly CineColombiaContext oCine;

    public PeliculaProductoraController(CineColombiaContext oCine)
    {
        this.oCine = oCine;
    }

    [HttpGet]
    public List<PeliculaProductora> ListarPeliculasProductora()
    {
        clsOpePeliculaProductora oPeliculaProductora = new clsOpePeliculaProductora(oCine);
        return oPeliculaProductora.ListarPeliculasProductora();
    }

    [HttpGet("{idPeliculaProductora}")]
    public IQueryable ConsultarPeliculaProductora(int idPeliculaProductora)
    {
        clsOpePeliculaProductora oPeliculaProductora = new clsOpePeliculaProductora(oCine);
        return oPeliculaProductora.ConsultarPeliculaProductora(idPeliculaProductora);
    }

    [HttpPost]
    public int Agregar([FromBody] PeliculaProductora peliculaProductora)
    {
        clsOpePeliculaProductora oPeliculaProductora = new clsOpePeliculaProductora(oCine);
        oPeliculaProductora.tblPeliculaProductora = peliculaProductora;
        return oPeliculaProductora.Agregar();
    }

    [HttpPut]
    public int Modificar([FromBody] PeliculaProductora peliculaProductora)
    {
        clsOpePeliculaProductora oPeliculaProductora = new clsOpePeliculaProductora(oCine);
        oPeliculaProductora.tblPeliculaProductora = peliculaProductora;
        return oPeliculaProductora.Modificar();
    }
}
