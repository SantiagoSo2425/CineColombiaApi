using System.Collections.Generic;
using System.Linq;
using apiCine.Clases;
using apiSIA.Models;
using CineColombiaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiCine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VentaController : ControllerBase
{
    private readonly CineColombiaContext oCine;

    public VentaController(CineColombiaContext oCine)
    {
        this.oCine = oCine;
    }

    [HttpGet]
    public List<Ventum> ListarVentas()
    {
        clsOpeVenta oVenta = new clsOpeVenta(oCine);
        return oVenta.ListarVentas();
    }

    [HttpGet("{idVenta}")]
    public IQueryable ConsultarVenta(int idVenta)
    {
        clsOpeVenta oVenta = new clsOpeVenta(oCine);
        return oVenta.ConsultarVenta(idVenta);
    }

    [HttpPost]
    public int Agregar([FromBody] Ventum venta)
    {
        clsOpeVenta oVenta = new clsOpeVenta(oCine);
        oVenta.tblVenta = venta;
        return oVenta.Agregar();
    }

    [HttpPut]
    public int Modificar([FromBody] Ventum venta)
    {
        clsOpeVenta oVenta = new clsOpeVenta(oCine);
        oVenta.tblVenta = venta;
        return oVenta.Modificar();
    }
}
