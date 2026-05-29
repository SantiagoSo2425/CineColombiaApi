using System.Collections.Generic;
using System.Linq;
using apiCine.Clases;
using CineColombiaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiCine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProfesionController : ControllerBase
{
    private readonly CineColombiaContext oCine;

    public ProfesionController(CineColombiaContext oCine)
    {
        this.oCine = oCine;
    }

    [HttpGet]
    public List<Profesion> ListarProfesiones()
    {
        clsOpeProfesion oProfesion = new clsOpeProfesion(oCine);
        return oProfesion.ListarProfesiones();
    }

    [HttpGet("{idProfesion}")]
    public IQueryable ConsultarProfesion(int idProfesion)
    {
        clsOpeProfesion oProfesion = new clsOpeProfesion(oCine);
        return oProfesion.ConsultarProfesion(idProfesion);
    }

    [HttpPost]
    public int Agregar([FromBody] Profesion profesion)
    {
        clsOpeProfesion oProfesion = new clsOpeProfesion(oCine);
        oProfesion.tblProfesion = profesion;
        return oProfesion.Agregar();
    }

    [HttpPut]
    public int Modificar([FromBody] Profesion profesion)
    {
        clsOpeProfesion oProfesion = new clsOpeProfesion(oCine);
        oProfesion.tblProfesion = profesion;
        return oProfesion.Modificar();
    }
}
