using System.Collections.Generic;
using System.Linq;
using apiCine.Clases;
using CineColombiaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiCine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PeliculaDistribuidoraController : ControllerBase
{
    private readonly CineColombiaContext oCine;

    public PeliculaDistribuidoraController(CineColombiaContext oCine)
    {
        this.oCine = oCine;
    }

    [HttpGet]
    public List<PeliculaDistribuidora> ListarPeliculasDistribuidora()
    {
        clsOpePeliculaDistribuidora oPeliculaDistribuidora = new clsOpePeliculaDistribuidora(oCine);
        return oPeliculaDistribuidora.ListarPeliculasDistribuidora();
    }

    [HttpGet("{idPeliculaDistribuidora}")]
    public IQueryable ConsultarPeliculaDistribuidora(int idPeliculaDistribuidora)
    {
        clsOpePeliculaDistribuidora oPeliculaDistribuidora = new clsOpePeliculaDistribuidora(oCine);
        return oPeliculaDistribuidora.ConsultarPeliculaDistribuidora(idPeliculaDistribuidora);
    }

    [HttpPost]
    public int Agregar([FromBody] PeliculaDistribuidora peliculaDistribuidora)
    {
        clsOpePeliculaDistribuidora oPeliculaDistribuidora = new clsOpePeliculaDistribuidora(oCine);
        oPeliculaDistribuidora.tblPeliculaDistribuidora = peliculaDistribuidora;
        return oPeliculaDistribuidora.Agregar();
    }

    [HttpPut]
    public int Modificar([FromBody] PeliculaDistribuidora peliculaDistribuidora)
    {
        clsOpePeliculaDistribuidora oPeliculaDistribuidora = new clsOpePeliculaDistribuidora(oCine);
        oPeliculaDistribuidora.tblPeliculaDistribuidora = peliculaDistribuidora;
        return oPeliculaDistribuidora.Modificar();
    }
}
