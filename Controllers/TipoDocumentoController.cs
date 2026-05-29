using System.Collections.Generic;
using System.Linq;
using apiCine.Clases;
using apiSIA.Models;
using CineColombiaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiCine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TipoDocumentoController : ControllerBase
{
    private readonly CineColombiaContext oCine;

    public TipoDocumentoController(CineColombiaContext oCine)
    {
        this.oCine = oCine;
    }

    [HttpGet]
    public List<TipoDocumento> ListarTipoDocumentos()
    {
        clsOpeTipoDocumento oTipoDocumento = new clsOpeTipoDocumento(oCine);
        return oTipoDocumento.ListarTipoDocumentos();
    }

    [HttpGet("{idTipoDocumento}")]
    public IQueryable ConsultarTipoDocumento(int idTipoDocumento)
    {
        clsOpeTipoDocumento oTipoDocumento = new clsOpeTipoDocumento(oCine);
        return oTipoDocumento.ConsultarTipoDocumento(idTipoDocumento);
    }

    [HttpPost]
    public int Agregar([FromBody] TipoDocumento tipoDocumento)
    {
        clsOpeTipoDocumento oTipoDocumento = new clsOpeTipoDocumento(oCine);
        oTipoDocumento.tblTipoDocumento = tipoDocumento;
        return oTipoDocumento.Agregar();
    }

    [HttpPut]
    public int Modificar([FromBody] TipoDocumento tipoDocumento)
    {
        clsOpeTipoDocumento oTipoDocumento = new clsOpeTipoDocumento(oCine);
        oTipoDocumento.tblTipoDocumento = tipoDocumento;
        return oTipoDocumento.Modificar();
    }
}
