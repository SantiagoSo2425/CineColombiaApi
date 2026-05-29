using System.Collections.Generic;
using System.Linq;
using apiCine.Clases;
using CineColombiaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiCine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaisController : ControllerBase
{
    private readonly CineColombiaContext oCine;

    public PaisController(CineColombiaContext oCine)
    {
        this.oCine = oCine;
    }

    [HttpGet]
    public List<Pai> ListarPaises()
    {
        clsOpePais oPais = new clsOpePais(oCine);
        return oPais.ListarPaises();
    }

    [HttpGet("{idPais}")]
    public IQueryable ConsultarPais(int idPais)
    {
        clsOpePais oPais = new clsOpePais(oCine);
        return oPais.ConsultarPais(idPais);
    }

    [HttpPost]
    public int Agregar([FromBody] Pai pais)
    {
        clsOpePais oPais = new clsOpePais(oCine);
        oPais.tblPais = pais;
        return oPais.Agregar();
    }

    [HttpPut]
    public int Modificar([FromBody] Pai pais)
    {
        clsOpePais oPais = new clsOpePais(oCine);
        oPais.tblPais = pais;
        return oPais.Modificar();
    }
}
