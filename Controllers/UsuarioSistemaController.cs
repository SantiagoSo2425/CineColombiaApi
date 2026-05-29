using System.Collections.Generic;
using System.Linq;
using apiCine.Clases;
using apiSIA.Models;
using CineColombiaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiCine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuarioSistemaController : ControllerBase
{
    private readonly CineColombiaContext oCine;

    public UsuarioSistemaController(CineColombiaContext oCine)
    {
        this.oCine = oCine;
    }

    [HttpGet]
    public List<UsuarioSistema> ListarUsuariosSistema()
    {
        clsOpeUsuarioSistema oUsuario = new clsOpeUsuarioSistema(oCine);
        return oUsuario.ListarUsuariosSistema();
    }

    [HttpGet("{idUsuario}")]
    public IQueryable ConsultarUsuarioSistema(int idUsuario)
    {
        clsOpeUsuarioSistema oUsuario = new clsOpeUsuarioSistema(oCine);
        return oUsuario.ConsultarUsuarioSistema(idUsuario);
    }

    [HttpGet("login")]
    public IQueryable ConsultarLogin([FromQuery] string username, [FromQuery] string passwordHash)
    {
        clsOpeUsuarioSistema oUsuario = new clsOpeUsuarioSistema(oCine);
        return oUsuario.ConsultarLogin(username, passwordHash);
    }

    [HttpPost]
    public int Agregar([FromBody] UsuarioSistema usuario)
    {
        clsOpeUsuarioSistema oUsuario = new clsOpeUsuarioSistema(oCine);
        oUsuario.tblUsuarioSistema = usuario;
        return oUsuario.Agregar();
    }

    [HttpPut]
    public int Modificar([FromBody] UsuarioSistema usuario)
    {
        clsOpeUsuarioSistema oUsuario = new clsOpeUsuarioSistema(oCine);
        oUsuario.tblUsuarioSistema = usuario;
        return oUsuario.Modificar();
    }
}
