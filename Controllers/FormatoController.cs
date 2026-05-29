using System.Collections.Generic;
using System.Linq;
using apiCine.Clases;
using apiSIA.Models;
using CineColombiaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiCine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FormatoController : ControllerBase
{
    private readonly CineColombiaContext oCine;

    public FormatoController(CineColombiaContext oCine)
    {
        this.oCine = oCine;
    }

    [HttpGet]
    public List<Formato> ListarFormatos()
    {
        clsOpeFormato oFormato = new clsOpeFormato(oCine);
        return oFormato.ListarFormatos();
    }

    [HttpGet("{idFormato}")]
    public IQueryable ConsultarFormato(int idFormato)
    {
        clsOpeFormato oFormato = new clsOpeFormato(oCine);
        return oFormato.ConsultarFormato(idFormato);
    }

    [HttpPost]
    public int Agregar([FromBody] Formato formato)
    {
        clsOpeFormato oFormato = new clsOpeFormato(oCine);
        oFormato.tblFormato = formato;
        return oFormato.Agregar();
    }

    [HttpPut]
    public int Modificar([FromBody] Formato formato)
    {
        clsOpeFormato oFormato = new clsOpeFormato(oCine);
        oFormato.tblFormato = formato;
        return oFormato.Modificar();
    }
}
