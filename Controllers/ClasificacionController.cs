using System.Collections.Generic;
using System.Linq;
using apiCine.Clases;
using apiSIA.Models;
using CineColombiaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiCine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClasificacionController : ControllerBase
{
    private readonly CineColombiaContext oCine;

    public ClasificacionController(CineColombiaContext oCine)
    {
        this.oCine = oCine;
    }

    [HttpGet]
    public List<Clasificacion> ListarClasificaciones()
    {
        clsOpeClasificacion oClasificacion = new clsOpeClasificacion(oCine);
        return oClasificacion.ListarClasificaciones();
    }

    [HttpGet("{idClasificacion}")]
    public IQueryable ConsultarClasificacion(int idClasificacion)
    {
        clsOpeClasificacion oClasificacion = new clsOpeClasificacion(oCine);
        return oClasificacion.ConsultarClasificacion(idClasificacion);
    }

    [HttpPost]
    public int Agregar([FromBody] Clasificacion clasificacion)
    {
        clsOpeClasificacion oClasificacion = new clsOpeClasificacion(oCine);
        oClasificacion.tblClasificacion = clasificacion;
        return oClasificacion.Agregar();
    }

    [HttpPut]
    public int Modificar([FromBody] Clasificacion clasificacion)
    {
        clsOpeClasificacion oClasificacion = new clsOpeClasificacion(oCine);
        oClasificacion.tblClasificacion = clasificacion;
        return oClasificacion.Modificar();
    }
}
