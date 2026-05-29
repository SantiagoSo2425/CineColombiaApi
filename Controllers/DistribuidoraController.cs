using System.Collections.Generic;
using System.Linq;
using apiCine.Clases;
using CineColombiaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiCine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DistribuidoraController : ControllerBase
{
    private readonly CineColombiaContext oCine;

    public DistribuidoraController(CineColombiaContext oCine)
    {
        this.oCine = oCine;
    }

    [HttpGet]
    public List<Distribuidora> ListarDistribuidoras()
    {
        clsOpeDistribuidora oDistribuidora = new clsOpeDistribuidora(oCine);
        return oDistribuidora.ListarDistribuidoras();
    }

    [HttpGet("{idDistribuidora}")]
    public IQueryable ConsultarDistribuidora(int idDistribuidora)
    {
        clsOpeDistribuidora oDistribuidora = new clsOpeDistribuidora(oCine);
        return oDistribuidora.ConsultarDistribuidora(idDistribuidora);
    }

    [HttpPost]
    public int Agregar([FromBody] Distribuidora distribuidora)
    {
        clsOpeDistribuidora oDistribuidora = new clsOpeDistribuidora(oCine);
        oDistribuidora.tblDistribuidora = distribuidora;
        return oDistribuidora.Agregar();
    }

    [HttpPut]
    public int Modificar([FromBody] Distribuidora distribuidora)
    {
        clsOpeDistribuidora oDistribuidora = new clsOpeDistribuidora(oCine);
        oDistribuidora.tblDistribuidora = distribuidora;
        return oDistribuidora.Modificar();
    }
}
