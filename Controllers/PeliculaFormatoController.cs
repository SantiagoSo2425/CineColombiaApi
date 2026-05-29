using System.Collections.Generic;
using System.Linq;
using apiCine.Clases;
using CineColombiaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiCine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PeliculaFormatoController : ControllerBase
{
    private readonly CineColombiaContext oCine;

    public PeliculaFormatoController(CineColombiaContext oCine)
    {
        this.oCine = oCine;
    }

    [HttpGet]
    public List<PeliculaFormato> ListarPeliculasFormato()
    {
        clsOpePeliculaFormato oPeliculaFormato = new clsOpePeliculaFormato(oCine);
        return oPeliculaFormato.ListarPeliculasFormato();
    }

    [HttpGet("{idPeliculaFormato}")]
    public IQueryable ConsultarPeliculaFormato(int idPeliculaFormato)
    {
        clsOpePeliculaFormato oPeliculaFormato = new clsOpePeliculaFormato(oCine);
        return oPeliculaFormato.ConsultarPeliculaFormato(idPeliculaFormato);
    }

    [HttpPost]
    public int Agregar([FromBody] PeliculaFormato peliculaFormato)
    {
        clsOpePeliculaFormato oPeliculaFormato = new clsOpePeliculaFormato(oCine);
        oPeliculaFormato.tblPeliculaFormato = peliculaFormato;
        return oPeliculaFormato.Agregar();
    }

    [HttpPut]
    public int Modificar([FromBody] PeliculaFormato peliculaFormato)
    {
        clsOpePeliculaFormato oPeliculaFormato = new clsOpePeliculaFormato(oCine);
        oPeliculaFormato.tblPeliculaFormato = peliculaFormato;
        return oPeliculaFormato.Modificar();
    }
}
