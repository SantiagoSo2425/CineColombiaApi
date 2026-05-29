using System.Collections.Generic;
using System.Linq;
using apiCine.Clases;
using CineColombiaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiCine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MetodoPagoController : ControllerBase
{
    private readonly CineColombiaContext oCine;

    public MetodoPagoController(CineColombiaContext oCine)
    {
        this.oCine = oCine;
    }

    [HttpGet]
    public List<MetodoPago> ListarMetodosPago()
    {
        clsOpeMetodoPago oMetodoPago = new clsOpeMetodoPago(oCine);
        return oMetodoPago.ListarMetodosPago();
    }

    [HttpGet("{idMetodoPago}")]
    public IQueryable ConsultarMetodoPago(int idMetodoPago)
    {
        clsOpeMetodoPago oMetodoPago = new clsOpeMetodoPago(oCine);
        return oMetodoPago.ConsultarMetodoPago(idMetodoPago);
    }

    [HttpPost]
    public int Agregar([FromBody] MetodoPago metodoPago)
    {
        clsOpeMetodoPago oMetodoPago = new clsOpeMetodoPago(oCine);
        oMetodoPago.tblMetodoPago = metodoPago;
        return oMetodoPago.Agregar();
    }

    [HttpPut]
    public int Modificar([FromBody] MetodoPago metodoPago)
    {
        clsOpeMetodoPago oMetodoPago = new clsOpeMetodoPago(oCine);
        oMetodoPago.tblMetodoPago = metodoPago;
        return oMetodoPago.Modificar();
    }
}
