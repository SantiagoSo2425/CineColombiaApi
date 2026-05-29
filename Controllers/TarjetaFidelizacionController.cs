using System.Collections.Generic;
using System.Linq;
using apiCine.Clases;
using CineColombiaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiCine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TarjetaFidelizacionController : ControllerBase
{
    private readonly CineColombiaContext oCine;

    public TarjetaFidelizacionController(CineColombiaContext oCine)
    {
        this.oCine = oCine;
    }

    [HttpGet]
    public List<TarjetaFidelizacion> ListarTarjetasFidelizacion()
    {
        clsOpeTarjetaFidelizacion oTarjeta = new clsOpeTarjetaFidelizacion(oCine);
        return oTarjeta.ListarTarjetasFidelizacion();
    }

    [HttpGet("{idTarjeta}")]
    public IQueryable ConsultarTarjetaFidelizacion(int idTarjeta)
    {
        clsOpeTarjetaFidelizacion oTarjeta = new clsOpeTarjetaFidelizacion(oCine);
        return oTarjeta.ConsultarTarjetaFidelizacion(idTarjeta);
    }

    [HttpPost]
    public int Agregar([FromBody] TarjetaFidelizacion tarjeta)
    {
        clsOpeTarjetaFidelizacion oTarjeta = new clsOpeTarjetaFidelizacion(oCine);
        oTarjeta.tblTarjetaFidelizacion = tarjeta;
        return oTarjeta.Agregar();
    }

    [HttpPut]
    public int Modificar([FromBody] TarjetaFidelizacion tarjeta)
    {
        clsOpeTarjetaFidelizacion oTarjeta = new clsOpeTarjetaFidelizacion(oCine);
        oTarjeta.tblTarjetaFidelizacion = tarjeta;
        return oTarjeta.Modificar();
    }

    [HttpPut("inactivar/{idTarjeta}")]
    public int Inactivar(int idTarjeta)
    {
        clsOpeTarjetaFidelizacion oTarjeta = new clsOpeTarjetaFidelizacion(oCine);
        return oTarjeta.Inactivar(idTarjeta);
    }
}
