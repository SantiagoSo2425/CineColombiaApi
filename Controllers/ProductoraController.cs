using System.Collections.Generic;
using System.Linq;
using apiCine.Clases;
using CineColombiaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiCine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductoraController : ControllerBase
{
    private readonly CineColombiaContext oCine;

    public ProductoraController(CineColombiaContext oCine)
    {
        this.oCine = oCine;
    }

    [HttpGet]
    public List<Productora> ListarProductoras()
    {
        clsOpeProductora oProductora = new clsOpeProductora(oCine);
        return oProductora.ListarProductoras();
    }

    [HttpGet("{idProductora}")]
    public IQueryable ConsultarProductora(int idProductora)
    {
        clsOpeProductora oProductora = new clsOpeProductora(oCine);
        return oProductora.ConsultarProductora(idProductora);
    }

    [HttpPost]
    public int Agregar([FromBody] Productora productora)
    {
        clsOpeProductora oProductora = new clsOpeProductora(oCine);
        oProductora.tblProductora = productora;
        return oProductora.Agregar();
    }

    [HttpPut]
    public int Modificar([FromBody] Productora productora)
    {
        clsOpeProductora oProductora = new clsOpeProductora(oCine);
        oProductora.tblProductora = productora;
        return oProductora.Modificar();
    }
}
