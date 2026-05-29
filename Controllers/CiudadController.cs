using System.Collections.Generic;
using System.Linq;
using apiCine.Clases;
using CineColombiaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiCine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CiudadController : ControllerBase
{
    private readonly CineColombiaContext oCine;

    public CiudadController(CineColombiaContext oCine)
    {
        this.oCine = oCine;
    }

    [HttpGet]
    public List<Ciudad> ListarCiudades()
    {
        clsOpeCiudad oCiudad = new clsOpeCiudad(oCine);
        return oCiudad.ListarCiudades();
    }

    [HttpGet("{idCiudad}")]
    public IQueryable ConsultarCiudad(int idCiudad)
    {
        clsOpeCiudad oCiudad = new clsOpeCiudad(oCine);
        return oCiudad.ConsultarCiudad(idCiudad);
    }

    [HttpPost]
    public int Agregar([FromBody] Ciudad ciudad)
    {
        clsOpeCiudad oCiudad = new clsOpeCiudad(oCine);
        oCiudad.tblCiudad = ciudad;
        return oCiudad.Agregar();
    }

    [HttpPut]
    public int Modificar([FromBody] Ciudad ciudad)
    {
        clsOpeCiudad oCiudad = new clsOpeCiudad(oCine);
        oCiudad.tblCiudad = ciudad;
        return oCiudad.Modificar();
    }
}
